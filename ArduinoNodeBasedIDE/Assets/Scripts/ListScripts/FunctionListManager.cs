using Backend.API;
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

    protected override List<IFunctionManage> GetFunctions()
    {
        return nodeBlockManager.SearchNodeBlocks(this, lastInput);
    }
    protected override void UpdateContent()
    {
        DestroyContent();
        AddContentFunctions();
    }
    public void CreateNewFunction()
    {
        nodeBlockManager.AddNodeBlock(nameField.GetComponent<TMP_InputField>().text, 0, 0);
        UpdateContent();
    }
}

