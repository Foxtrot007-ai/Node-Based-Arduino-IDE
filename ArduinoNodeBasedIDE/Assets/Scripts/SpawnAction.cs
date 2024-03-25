using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAction : IAction
{
    private GameObject nodeBlockObject;

    public SpawnAction(GameObject nodeBlockObject)
    {
        Debug.Log("Pushing SpawnAction");
        this.nodeBlockObject = nodeBlockObject;
    }

    public void RedoAction()
    {
        Debug.Log("Redo SpawnAction");
        nodeBlockObject.SetActive(true);
    }

    public void UndoAction()
    {
        Debug.Log("Undo SpawnAction");
        nodeBlockObject.GetComponent<NodeBlockController>().UnconnectAll();
        nodeBlockObject.SetActive(false);
    }
}
