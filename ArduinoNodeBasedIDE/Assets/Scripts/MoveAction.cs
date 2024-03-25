using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class MoveAction : IAction
{
    private NodeBlockController controller;
    private Vector2 oldPosition;
    private Vector2 newPosition;

    public MoveAction(NodeBlockController controller, Vector2 oldPosition, Vector2 newPosition)
    {
        Debug.Log("Push MoveAction");
        this.controller = controller;
        this.oldPosition = oldPosition;
        this.newPosition = newPosition;
    }

    public void RedoAction()
    {
        Debug.Log("Redo MoveAction");
        controller.transform.position = newPosition;
    }

    public void UndoAction()
    {
        Debug.Log("Undo MoveAction");
        controller.transform.position = oldPosition;
    }
}
