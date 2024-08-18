using Backend.API;
using UnityEngine;

//button objets for reference List content
public class ReferenceButtonScript : ButtonScript
{
    //Additional Child Class Attributes
    public GameObject typeField;

    //Class Methods (just override)
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
