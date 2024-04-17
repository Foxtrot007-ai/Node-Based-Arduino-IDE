using Backend.API;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalVariableListManager : ListManager
{
    public GameObject nameField;
    protected override List<IVariableManage> GetVariables()
    {
        return nodeBlockManager.SearchNodeBlocks(this, lastInput);
    }
    protected override void UpdateContent()
    {
        DestroyContent();
        AddContentVariables();
    }
    public void CreateNewVariable()
    {
        nodeBlockManager.AddNodeBlock(this, nameField.GetComponent<TMP_InputField>().text);
        UpdateContent();
    }
}
