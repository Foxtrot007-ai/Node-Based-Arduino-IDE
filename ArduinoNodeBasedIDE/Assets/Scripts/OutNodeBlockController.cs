using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutNodeBlockController : NodeBlockController
{
    public override void addOutPoint(string type)
    {
        if (outPoint != null)
        {
            return;
        }

        GameObject newPoint = Instantiate(inPointPrefab, inPointStartPoint.transform.position, Quaternion.identity);
        newPoint.transform.SetParent(this.transform);
        ConnectionPoint connection = newPoint.GetComponent<ConnectionPoint>();
        connection.nodeBlockName = nodeBlockName;
        connection.SetType(type);
        connection.interactiveDefinition = true;
        connection.connectionIndex = 0;
        outPoint = newPoint;
    }
    public override void DestroyMe()
    {
        Debug.Log("Can't Destroy me :)");
    }
}
