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

    public GameObject inputEditorPrefab;
    public GameObject outputEditorPrefab;

    public GameObject inputStartPoint;
    public GameObject outputStartPoint;

    public Vector3 inputPointIncrease;

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
            if(lastTimeStamp != currentNodeBlock.lastChange)
            {
                UpdateContent();
            }
        }
    }
    public void SetNodeBlockToEdit(NodeBlock nodeBlock)
    {
        instantiated = true;
        currentNodeBlock = nodeBlock;
        UpdateContent();
    }

    public void UpdateField()
    {
        nodeBlockName.GetComponent<TMP_InputField>().text = currentNodeBlock.GetName();
    }

    public void InstantiateInputs() 
    {
        int numberOfInputs = currentNodeBlock.GetNumberOfInputs();

        for(int i = 0; i < numberOfInputs; i++)
        {
            inputObjects.Add(CreateButton(currentNodeBlock.GetInputType(i)));
        }
    }

    public void InstantiateOutput()
    {
        if (currentNodeBlock.returnOutputBlock)
        {
            outputObject = CreateOutPoint(outputEditorPrefab, outputStartPoint.transform.position, currentNodeBlock.GetOutputType());
        }
    }

    public GameObject CreateOutPoint(GameObject prefab, Vector3 point, string type)
    { 
        GameObject temp = Instantiate(prefab, point, Quaternion.identity);
        temp.transform.SetParent(this.transform);
        temp.transform.localScale = Vector3.one;
        temp.GetComponentInChildren<TMP_InputField>().text = type;
        return temp;
    }

    public void CleanEditor()
    {
        foreach(var node in inputObjects)
        {
            GameObject.Destroy(node);
        }
        inputObjects.Clear();

        if (outputObject != null)
        {
            GameObject.Destroy(outputObject);
        }
    }
    protected GameObject CreateButton(string type)
    {
        GameObject newContent = Instantiate(inputEditorPrefab);
        newContent.transform.SetParent(listContainer.transform);
        newContent.GetComponentInChildren<TMP_InputField>().text = type;
        newContent.transform.localScale = Vector3.one;
        return newContent;
    }

    protected void UpdateContent()
    {
        lastTimeStamp = currentNodeBlock.lastChange;
        UpdateField();
        CleanEditor();
        InstantiateInputs();
        InstantiateOutput();
    }

    public void DeleteInput()
    {
        currentNodeBlock.DeleteInput();
    }

    public void AddInput()
    {
        currentNodeBlock.AddInput();
    }
    public void DeleteOutput()
    {
        currentNodeBlock.DeleteOutput();
    }

    public void AddOutput()
    {
        currentNodeBlock.AddOutput();
    }

    public void UpdateNodeBlockData()
    {
        string newName = nodeBlockName.GetComponentInChildren<TMP_InputField>().text;
        if(newName != currentNodeBlock.GetName())
        {
            currentNodeBlock.SetName(newName);
        }

        for (int i = 0; i < currentNodeBlock.GetNumberOfInputs(); i++)
        {
            string newType = inputObjects[i].GetComponentInChildren<TMP_InputField>().text;
            if (newType != currentNodeBlock.GetInputType(i)) {
                nodeBlockManager.updateInputType(i, newType, currentNodeBlock);
            } 
        }

        if (currentNodeBlock.returnOutputBlock)
        {
            string newType = outputObject.GetComponentInChildren<TMP_InputField>().text;
            if (newType != currentNodeBlock.GetOutputType())
            {
                nodeBlockManager.updateOutputType(newType, currentNodeBlock);
            }
        }
    }
}
