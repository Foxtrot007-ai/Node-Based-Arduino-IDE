using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FunctionListManager : MonoBehaviour
{
    public GameObject nameField;
    public GameObject inputField;
    public GameObject outputField;
    public GameObject listContainer;
    public GameObject buttonContent;
    public List<GameObject> contentObjects;
    void Start()
    {
        AddContent();
    }

    public void CreateNewFunction()
    {
        int numberOfInput = 0;
        int numberOfOutput = 0;

        try
        {
            numberOfInput = Convert.ToInt32(inputField.GetComponent<TMP_InputField>().text);
            numberOfOutput = Convert.ToInt32(outputField.GetComponent<TMP_InputField>().text);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
            return;
        }
       
        GameObject.FindGameObjectWithTag("NodeBlocksManager")
                    .GetComponent<NodeBlockManager>()
                        .AddNewFunction(nameField.GetComponent<TMP_InputField>().text, numberOfInput, numberOfOutput);

        UpdateContent();
    }


    private void AddContent()
    {
        List<string> namesList = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>().getFunctionsNames();

        foreach (string s in namesList)
        {
            GameObject newContent = Instantiate(buttonContent);
            newContent.transform.SetParent(listContainer.transform);
            newContent.GetComponent<ButtonScript>().SetName(s);
            newContent.GetComponent<ButtonScript>().SetMode("view");
            contentObjects.Add(newContent);
            newContent.transform.localScale = Vector3.one;
        }
    }

    private void DestroyContent()
    {
        foreach (var content in contentObjects)
        {
            Destroy(content);
        }

        contentObjects.Clear();
    }

    public void UpdateContent()
    {
        DestroyContent();
        AddContent();
    }
}

