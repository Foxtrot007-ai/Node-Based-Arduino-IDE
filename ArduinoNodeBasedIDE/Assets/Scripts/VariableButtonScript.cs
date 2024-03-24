using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableButtonScript : ButtonScript
{
    public override void SpawnNodeBlock()
    {
        nodeBlockManager.SpawnNodeBlock(this, node);
    }
    public override void DeleteNodeBlock()
    {
        nodeBlockManager.DeleteNodeBlock(this, node);
    }

    public override void EditMyNodeBlock()
    {
        nodeBlockManager.SetNodeBlockToEdit(this, node);
    }
}
