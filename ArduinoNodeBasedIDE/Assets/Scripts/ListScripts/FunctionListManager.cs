using Backend.API;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Script for Function list UI Control
public class FunctionListManager : ListManager
{
    //Additional attributes
    public GameObject nameField;

    //overrided class methods
    protected override List<IUserFunction> GetFunctions()
    {
        return nodeBlockManager.SearchNodeBlocks(this, lastInput);
    }
    public override void UpdateContent()
    {
        DestroyContent();
        AddContentFunctions();
    }
    //add button ui function
    public void CreateNewFunction()
    {
        nodeBlockManager.AddNodeBlock(nameField.GetComponent<TMP_InputField>().text, 0, 0);
        UpdateContent();
    }
}

