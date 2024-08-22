using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectAction : IAction
{
    private ConnectionPoint originalPoint;
    private GameObject partnerPoint;
    public ConnectAction(ConnectionPoint originalPoint, GameObject partnerPoint)
    {
        Debug.Log("Pushing ConnectAction");
        this.originalPoint = originalPoint;
        this.partnerPoint = partnerPoint;
    }
    public void RedoAction()
    {
        Debug.Log("Redo ConnectAction");
        //originalPoint.connectedPoint = partnerPoint;
       // partnerPoint.GetComponent<ConnectionPoint>().connectedPoint = originalPoint.gameObject;
    }

    public void UndoAction()
    {
        Debug.Log("Undo ConnectAction");
        //originalPoint.connectedPoint = null;
       // partnerPoint.GetComponent<ConnectionPoint>().connectedPoint = null;
    }
}
