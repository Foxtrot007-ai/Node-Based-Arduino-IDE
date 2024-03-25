using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnconnectAction : IAction
{
    private ConnectionPoint originalPoint;
    private GameObject partnerPoint;
    public UnconnectAction(ConnectionPoint originalPoint, GameObject partnerPoint)
    {
        Debug.Log("Pushing UnconnectAction");
        this.originalPoint = originalPoint;
        this.partnerPoint = partnerPoint;
    }
    public void RedoAction()
    {
        Debug.Log("Redo UnconnectAction");
        originalPoint.connectedPoint = null;
        partnerPoint.GetComponent<ConnectionPoint>().connectedPoint = null;
    }

    public void UndoAction()
    {
        Debug.Log("Undo UnconnectAction");
        originalPoint.connectedPoint = partnerPoint;
        partnerPoint.GetComponent<ConnectionPoint>().connectedPoint = originalPoint.gameObject;
    }
}
