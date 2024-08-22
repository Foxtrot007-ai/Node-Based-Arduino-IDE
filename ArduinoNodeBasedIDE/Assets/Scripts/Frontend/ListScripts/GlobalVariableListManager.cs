using Backend.API;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Script for Global Variable List UI Control
public class GlobalVariableListManager : ListManager
{
    //Additional attributes
    public GameObject nameField;

    //overrided class methods 
    protected override List<IVariable> GetVariables()
    {
        return nodeBlockManager.SearchNodeBlocks(this, lastInput);
    }
    public override void UpdateContent()
    {
        DestroyContent();
        AddContentVariables();
    }

    //add button ui function
    public void CreateNewVariable()
    {
        nodeBlockManager.AddNodeBlock(this, nameField.GetComponent<TMP_InputField>().text);
        UpdateContent();
    }
}
