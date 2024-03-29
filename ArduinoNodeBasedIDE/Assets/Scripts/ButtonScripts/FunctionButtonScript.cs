using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FunctionButtonScript : ButtonScript
{
    public GameObject spawnButton;
    public GameObject deleteButton;
    public GameObject editButton;
    public GameObject changeButton;

    public override void SetNodeBlock(NodeBlock node)
    {
        this.node = node;
        text.GetComponent<TMP_Text>().text = node.GetName();
        if(node.GetName() == "setup" || node.GetName() == "loop")
        {
            spawnButton.GetComponent<Button>().interactable = false;
            deleteButton.GetComponent<Button>().interactable = false;
            editButton.GetComponent<Button>().interactable = false;
        }
    }
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
    public void ChangeView()
    {
        nodeBlockManager.ChangeView(this, node);
    }
}
