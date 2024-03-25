using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeViewAction : IAction
{
    private NodeBlock oldView;
    private NodeBlock newView;
    private NodeBlockManager manager;

    public ChangeViewAction(NodeBlock oldView, NodeBlock newView)
    {
        Debug.Log("Pushing ChangeViewAction");
        manager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
        this.oldView = oldView;
        this.newView = newView;
    }
    public void RedoAction()
    {
        Debug.Log("Redo ChangeViewAction");
        manager.viewsManager.ChangeView(newView);
    }

    public void UndoAction()
    {
        Debug.Log("Undo ChangeViewAction");
        manager.viewsManager.ChangeView(oldView);
    }
}
