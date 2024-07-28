using Backend.API;
using TMPro;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject text;
    public IFunctionManage function;
    public IVariableManage variable;
    public NodeBlockManager nodeBlockManager;

    public void Start()
    {
        nodeBlockManager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
    }

    public virtual void SetNodeBlock(IFunctionManage function)
    {
        this.function = function;
        text.GetComponent<TMP_Text>().text = function.Name;
    }
    public virtual void SetNodeBlock(IVariableManage variable)
    {
        this.variable = variable;
        text.GetComponent<TMP_Text>().text = variable.Name;
    }
    public virtual void SpawnNodeBlock()
    {
        nodeBlockManager.SpawnNodeBlock(this);
    }
    public virtual void DeleteNodeBlock()
    {
        nodeBlockManager.DeleteNodeBlock(this);
    }

    public virtual void EditMyNodeBlock()
    {
        nodeBlockManager.SetNodeBlockToEdit(this);
    }

}
