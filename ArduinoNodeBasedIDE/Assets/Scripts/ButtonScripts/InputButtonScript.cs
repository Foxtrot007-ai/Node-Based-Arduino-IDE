using Backend.API;
using TMPro;
using UnityEngine;

public class InputButtonScript : ButtonScript
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
    public override void DeleteNodeBlock()
    {
        nodeBlockManager.DeleteNodeBlock(this);
    }
    public void RemoveVariable()
    {
        GameObject.FindGameObjectWithTag("NodeBlockEditor").GetComponent<NodeBlockEditor>().DeleteInput(gameObject);
    }
    public override void EditMyNodeBlock()
    {
        nodeBlockManager.SetNodeBlockToEdit(this);
    }
}
