using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class NodeBlockController : MonoBehaviour
{
    public string nodeBlockName;
    public NodeBlockTypes type;

    protected Vector2 originPoint;
    protected Vector2 directionPoint;
    public bool colliding = false;
    public bool holding = false;
    public GameObject textField;

    public GameObject field;

    public GameObject inPointStartPoint;
    public GameObject outPointStartPoint;
    public GameObject nextBlockStartPoint;
    public GameObject previousBlockStartPoint;

    public Vector3 inPointStartPointIncrease;
    public Vector3 nextBlockStartPointIncrease;
      
    public GameObject inPointPrefab;
    public GameObject outPointPrefab;
    public GameObject nextBlockPrefab;
    public GameObject previousBlockPrefab;

    public List<GameObject> inPointsList = new List<GameObject>();
    public GameObject outPoint = null;
    public List<GameObject> nextBlockList = new List<GameObject>();
    public GameObject previousBlock = null;

    public virtual void addInPoint(int index, string type)
    {
        GameObject newPoint = Instantiate(inPointPrefab, inPointStartPoint.transform.position, Quaternion.identity);
        newPoint.transform.SetParent(this.transform);
        ConnectionPoint connection = newPoint.GetComponent<ConnectionPoint>();
        connection.nodeBlockName = nodeBlockName;
        connection.SetType(type);
        connection.connectionIndex = index;
        inPointStartPoint.transform.position += inPointStartPointIncrease;
        inPointsList.Add(newPoint);
    }

    public virtual void addOutPoint(string type)
    {
        if(outPoint != null)
        {
            return;
        }

        GameObject newPoint = Instantiate(outPointPrefab, outPointStartPoint.transform.position, Quaternion.identity);
        newPoint.transform.SetParent(this.transform);
        ConnectionPoint connection = newPoint.GetComponent<ConnectionPoint>();
        connection.nodeBlockName = nodeBlockName;
        connection.SetType(type);
        connection.connectionIndex = 0;
        outPoint = newPoint;
    }

    public void addNextBlock()
    {
        GameObject newPoint = Instantiate(nextBlockPrefab, nextBlockStartPoint.transform.position, Quaternion.identity);
        newPoint.transform.SetParent(this.transform);
        ConnectionPoint connection = newPoint.GetComponent<ConnectionPoint>();
        connection.nodeBlockName = nodeBlockName;
        connection.SetType("");
        connection.connectionIndex = 0;
        nextBlockStartPoint.transform.position += nextBlockStartPointIncrease;
        nextBlockList.Add(newPoint);
    }
    public void addPreviousBlock()
    {
        if(previousBlock != null)
        {
            return;
        }

        GameObject newPoint = Instantiate(previousBlockPrefab, previousBlockStartPoint.transform.position, Quaternion.identity);
        newPoint.transform.SetParent(this.transform);
        ConnectionPoint connection = newPoint.GetComponent<ConnectionPoint>();
        connection.nodeBlockName = nodeBlockName;
        connection.SetType("");
        connection.connectionIndex = 0;
        previousBlock = newPoint;
    }

    public void SetName(string name)
    {
        textField.GetComponent<TMP_Text>().text = name;
        this.nodeBlockName = name;
    }

    public virtual void DestroyMe()
    {
        Destroy(gameObject);
    }
    protected void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            originPoint = transform.position;
            directionPoint = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
            holding = true;
        }

        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            DestroyMe();
        }
    }
    protected void OnMouseDrag()
    {
        if (holding)
        {
            Vector2 moveVector = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - directionPoint;
            transform.position = moveVector;
        }
        
    }

    protected void OnMouseUp()
    {
        if (colliding)
        {
            transform.position = originPoint;
        }

        holding = false;

    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "NodeBlock")
        {
            colliding = true;
        }  
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NodeBlock")
        {
            colliding = false;
        }
    }
}

