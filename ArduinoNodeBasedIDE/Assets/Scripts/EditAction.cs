using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditAction : IAction
{
    private NodeBlock nodeBlock;
    private string oldType;
    private string newType;
    private int index;
    public EditAction(NodeBlock nodeBlock, string oldType, string newType, int index)
    {
        Debug.Log("Push EditAction");
        this.nodeBlock = nodeBlock;
        this.oldType = oldType;
        this.newType = newType;
        this.index = index;
    }

    private void SetType(string setType)
    {
        if (index == -1)
        {
            nodeBlock.SetOutputType(setType);
        }
        else
        {
            nodeBlock.SetInputType(setType, index);
        }
    }

    public void RedoAction()
    {
        Debug.Log("REdo EditAction");
        SetType(newType);
    }

    public void UndoAction()
    {
        Debug.Log("Undo EditAction");
        SetType(oldType);
    }
}
