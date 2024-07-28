using Backend.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceButtonScript : ButtonScript
{
    public GameObject typeField;
    public override void SetNodeBlock(ITemplate template)
    {
        base.SetNodeBlock(template);
    }
    public override void SpawnNodeBlock()
    {
        nodeBlockManager.SpawnNodeBlock(this);
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
