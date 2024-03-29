using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FunctionListManager : ListManager
{
    public GameObject nameField;
    public GameObject numberOfInputField;
    public GameObject outputField;

    protected override List<NodeBlock> GetNodeBlocks()
    {
        return nodeBlockManager.SearchNodeBlocks(this, lastInput);
    }

    public void CreateNewFunction()
    {
        int numberOfInput = 0;
        int numberOfOutput = 0;

        try
        {
            numberOfInput = Convert.ToInt32(numberOfInputField.GetComponent<TMP_InputField>().text);
            numberOfOutput = Convert.ToInt32(outputField.GetComponent<TMP_InputField>().text);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
            return;
        }
       
        nodeBlockManager.AddNodeBlock(nameField.GetComponent<TMP_InputField>().text, numberOfInput, numberOfOutput);

        UpdateContent();
    }
}

