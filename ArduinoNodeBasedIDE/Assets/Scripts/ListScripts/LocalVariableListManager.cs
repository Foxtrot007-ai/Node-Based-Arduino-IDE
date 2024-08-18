using Backend.API;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Script for Local Variable List UI Control
public class LocalVariableListManager : ListManager
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

    //reload list content from editors
    public void ReloadVariables()
    {
        UpdateContent();
    }

    //add button ui function
    public void CreateNewVariable()
    {
        nodeBlockManager.AddNodeBlock(this, nameField.GetComponent<TMP_InputField>().text);
        UpdateContent();
    }
}
