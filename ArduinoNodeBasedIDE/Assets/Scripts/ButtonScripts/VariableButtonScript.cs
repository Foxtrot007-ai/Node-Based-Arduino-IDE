using Backend.API;
using TMPro;
using UnityEngine;

//variable button objets for variable List content (global and local)
public class VariableButtonScript : ButtonScript
{
    //Additional Child Class Attributes
    public GameObject typeField;

    //Class Methods (just override)
    public override void SetNodeBlock(IVariable variable)
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
