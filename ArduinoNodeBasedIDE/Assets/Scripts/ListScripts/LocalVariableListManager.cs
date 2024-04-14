using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalVariableListManager : ListManager
{
    public GameObject nameField;
    protected override List<NodeBlock> GetNodeBlocks()
    {
        return nodeBlockManager.SearchNodeBlocks(this, lastInput);
    }
    public void CreateNewVariable()
    {
        nodeBlockManager.AddNodeBlock(this, nameField.GetComponent<TMP_InputField>().text);
        UpdateContent();
    }
}
