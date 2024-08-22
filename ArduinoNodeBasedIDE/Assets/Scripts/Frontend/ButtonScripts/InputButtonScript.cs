using Backend.API;
using TMPro;
using UnityEngine;


//input button objets for Function attributes List content in Function editor
public class InputButtonScript : ButtonScript
{
    //Additional Child Class Attributes
    public GameObject typeField;

    //Class Methods
    public override void SetNodeBlock(IVariable variable)
    {
        base.SetNodeBlock(variable);
        typeField.GetComponent<TMP_Text>().text = variable.Type.TypeName;
    }
    //functions for UI button
    public override void SpawnNodeBlock()
    {
        IFunction function = GameObject.FindGameObjectWithTag("NodeBlockEditor").GetComponent<NodeBlockEditor>().currentNodeBlock;
        nodeBlockManager.SpawnNodeBlock(this, function);
    }
    public override void DeleteNodeBlock()
    {
        
        nodeBlockManager.DeleteNodeBlock(this);
    }

    //Additional method for UI button
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
