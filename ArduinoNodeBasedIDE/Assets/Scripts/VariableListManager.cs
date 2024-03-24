using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VariableListManager : ListManager
{
    public GameObject nameField;
    protected override List<NodeBlock> GetNodeBlocks()
    {
        return nodeBlockManager.SearchNodeBlocks(this, lastInput);
    }
    public void CreateNewVariable()
    {
        nodeBlockManager.AddNodeBlock(nameField.GetComponent<TMP_InputField>().text);
        UpdateContent();
    }
}
