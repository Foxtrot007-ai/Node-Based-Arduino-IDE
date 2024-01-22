using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InNodeBlockController : NodeBlockController
{
    public override void addInPoint(int index, string type)
    {
        GameObject newPoint = Instantiate(outPointPrefab, outPointStartPoint.transform.position, Quaternion.identity);
        newPoint.transform.SetParent(this.transform);
        ConnectionPoint connection = newPoint.GetComponent<ConnectionPoint>();
        connection.nodeBlockName = nodeBlockName;
        connection.SetType(type);
        connection.connectionIndex = index;
        connection.interactiveDefinition = true;
        outPointStartPoint.transform.position += inPointStartPointIncrease;
        inPointsList.Add(newPoint);
    }
    public override void DestroyMe()
    {
        Debug.Log("Can't Destroy me :)");
    }
}
