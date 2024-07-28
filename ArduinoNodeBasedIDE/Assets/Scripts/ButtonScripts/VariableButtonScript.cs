using Backend.API;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VariableButtonScript : ButtonScript
{
    public GameObject typeField;
    public override void SetNodeBlock(IVariableManage variable)
    {
        base.SetNodeBlock(variable);
        typeField.GetComponent<TMP_Text>().text = variable.Type.TypeName;
    }
    public override void SpawnNodeBlock()
    {
        nodeBlockManager.SpawnNodeBlock(this);
    }
    public void SpawnSetNodeBlock()
    {
        nodeBlockManager.SpawnSetNodeBlock(this);
    }
    public override void DeleteNodeBlock()
    {
        nodeBlockManager.DeleteNodeBlock(this);
    }

    public override void EditMyNodeBlock()
    {
        nodeBlockManager.SetNodeBlockToEdit(this);
    }
}
