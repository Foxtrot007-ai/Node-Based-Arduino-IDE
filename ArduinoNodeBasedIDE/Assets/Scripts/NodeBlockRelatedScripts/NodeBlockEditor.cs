using Codice.CM.Client.Differences;
using PlasticGui;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeBlockEditor : MonoBehaviour
{
    public NodeBlock currentNodeBlock;

    public GameObject nodeBlockName;

    public GameObject inputButtonPrefab;

    public GameObject outputTypeField;


    public bool instantiated = false;
    public GameObject listContainer;
    public List<GameObject> inputObjects;
    public GameObject outputObject;

    public NodeBlockManager nodeBlockManager;
    public DateTime lastTimeStamp;
    public void Start()
    {
        nodeBlockManager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
        instantiated = false;
    }

    public void Update()
    {
        if (instantiated)
        {
            CheckForNewName();
            CheckForNewOutputType();
            CheckForInputChanges();
        }
    }
    public void SetNodeBlockToEdit(NodeBlock nodeBlock)
    {
        instantiated = true;
        currentNodeBlock = nodeBlock;
        nodeBlockName.GetComponentInChildren<TMP_InputField>().text = currentNodeBlock.GetName();
        outputTypeField.GetComponentInChildren<TMP_InputField>().text = currentNodeBlock.GetOutputType();
        UpdateContent();
    }


    public void CheckForNewName()
    {
        String inputName = nodeBlockName.GetComponentInChildren<TMP_InputField>().text;
        if (inputName != currentNodeBlock.GetName())
        {
            currentNodeBlock.SetName(inputName);
        }
    }

    public void CheckForNewOutputType()
    {
        String outputType = outputTypeField.GetComponentInChildren<TMP_InputField>().text;
        if (outputType != currentNodeBlock.GetOutputType())
        {
            if (!currentNodeBlock.returnOutputBlock && outputType != "void")
            {
                currentNodeBlock.AddOutput();
                currentNodeBlock.SetOutputType(outputType);
            }
            else if(currentNodeBlock.returnOutputBlock && outputType == "void")
            {
                currentNodeBlock.DeleteOutput();
                currentNodeBlock.SetOutputType(outputType);
            }    
        }
    }

    public void CheckForInputChanges()
    {
        if (lastTimeStamp != currentNodeBlock.lastChange)
        {
            UpdateContent();
        }
    }

    public void InstantiateInputs() 
    {
        int numberOfInputs = currentNodeBlock.GetNumberOfInputs();

        for(int i = 0; i < numberOfInputs; i++)
        {
            inputObjects.Add(CreateButton(i));
        }
    }


    public void CleanInputs()
    {
        foreach(var node in inputObjects)
        {
            GameObject.Destroy(node);
        }
        inputObjects.Clear();
    }
    protected GameObject CreateButton(int index)
    {
        GameObject newContent = Instantiate(inputButtonPrefab);
        newContent.transform.SetParent(listContainer.transform);
        newContent.GetComponentInChildren<TMP_InputField>().text = currentNodeBlock.GetInputType(index);
        newContent.transform.localScale = Vector3.one;
        return newContent;
    }

    protected void UpdateContent()
    {
        lastTimeStamp = currentNodeBlock.lastChange;
        CleanInputs();
        InstantiateInputs();
    }

    public void AddInput()
    {
        currentNodeBlock.AddInput();
    }
}
