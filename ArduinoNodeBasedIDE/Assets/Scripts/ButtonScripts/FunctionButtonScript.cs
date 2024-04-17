using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Backend.API;

public class FunctionButtonScript : ButtonScript
{
    public GameObject spawnButton;
    public GameObject deleteButton;
    public GameObject editButton;
    public GameObject changeButton;

    public override void SetNodeBlock(IFunctionManage function)
    {
        this.function = function;
        text.GetComponent<TMP_Text>().text = function.Name;
        if(function.Name == "setup" || function.Name == "loop")
        {
            spawnButton.GetComponent<Button>().interactable = false;
            deleteButton.GetComponent<Button>().interactable = false;
            editButton.GetComponent<Button>().interactable = false;
        }
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
    public void ChangeView()
    {
        nodeBlockManager.ChangeView(this);
    }
}
