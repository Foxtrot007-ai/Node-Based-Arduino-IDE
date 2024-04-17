using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceButtonScript : ButtonScript
{
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
