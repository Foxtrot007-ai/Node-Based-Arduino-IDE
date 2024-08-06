using Backend.API;
using TMPro;
using UnityEngine;

public class InputButtonScript : ButtonScript
{
    public GameObject typeField;
    public override void SetNodeBlock(IVariable variable)
    {
        base.SetNodeBlock(variable);
        typeField.GetComponent<TMP_Text>().text = variable.Type.TypeName;
    }
    public override void SpawnNodeBlock()
    {
        IFunction function = GameObject.FindGameObjectWithTag("NodeBlockEditor").GetComponent<NodeBlockEditor>().currentNodeBlock;
        nodeBlockManager.SpawnNodeBlock(this, function);
    }
    public override void DeleteNodeBlock()
    {
        
        nodeBlockManager.DeleteNodeBlock(this);
    }
    public void SpawnSetNodeBlock()
    {
        IFunction function = GameObject.FindGameObjectWithTag("NodeBlockEditor").GetComponent<NodeBlockEditor>().currentNodeBlock;
        nodeBlockManager.SpawnSetNodeBlock(this, function);
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
