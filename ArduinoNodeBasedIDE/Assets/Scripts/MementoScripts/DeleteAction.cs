using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAction : IAction
{
    private GameObject nodeBlockObject;

    public DeleteAction(GameObject nodeBlockObject)
    {
        Debug.Log("Pushing DeleteAction");
        this.nodeBlockObject = nodeBlockObject;
    }

    public void RedoAction()
    {
        Debug.Log("Redo DeleteAction");
        nodeBlockObject.GetComponent<NodeBlockController>().UnconnectAll();
        nodeBlockObject.SetActive(false);
    }

    public void UndoAction()
    {
        Debug.Log("Undo DeleteAction");
        nodeBlockObject.SetActive(true);
    }
}

