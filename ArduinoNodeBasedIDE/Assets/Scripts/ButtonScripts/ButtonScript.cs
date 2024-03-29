using TMPro;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject text;
    public NodeBlock node;
    public NodeBlockManager nodeBlockManager;

    public void Start()
    {
        nodeBlockManager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
    }

    public virtual void SetNodeBlock(NodeBlock node)
    {
        this.node = node;
        text.GetComponent<TMP_Text>().text = node.GetName();
    }
    public virtual void SpawnNodeBlock()
    {
        nodeBlockManager.SpawnNodeBlock(this, node);
    }
    public virtual void DeleteNodeBlock()
    {
        nodeBlockManager.DeleteNodeBlock(this, node);
    }

    public virtual void EditMyNodeBlock()
    {
        nodeBlockManager.SetNodeBlockToEdit(this, node);
    }

}
