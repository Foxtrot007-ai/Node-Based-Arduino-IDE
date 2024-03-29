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
        nodeBlockManager.AddNodeBlock(nameField.GetComponent<TMP_InputField>().text, 0, 0);
        UpdateContent();
    }
}

