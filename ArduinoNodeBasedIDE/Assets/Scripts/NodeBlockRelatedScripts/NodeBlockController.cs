using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Backend.API;

public class NodeBlockController : MonoBehaviour
{
    public INode nodeBlock;

    public GameObject textField;

    public GameObject field;

    //test variable
    public bool isStartNodeBlock;

    public GameObject inPointStartPoint;
    public GameObject outPointStartPoint;

    public Vector3 inPointStartPointIncrease;
    public Vector3 outPointStartPointIncrease;
    public Vector3 startFieldPoint;

    public GameObject inPointPrefab;
    public GameObject outPointPrefab;

    public List<GameObject> inPointsList = new List<GameObject>();
    public List<GameObject> outPointsList = new List<GameObject>();

    public bool instantiated = false;

    public NodeBlockManager nodeBlockManager;


    public void DestroyMe()
    {
        if (!isStartNodeBlock)
        {
            nodeBlock.Delete();
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        nodeBlockManager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
        startFieldPoint = field.transform.localPosition;
    }
    public void Update()
    {
        if (instantiated)
        {
            Validation();
        }
    }

    virtual public void Validation()
    {
        CheckForNameChange();

        if (nodeBlock.IsDeleted)
        {
            Destroy(gameObject);
        }

        if (inPointsList.Count != nodeBlock.InputsList.Count || outPointsList.Count != nodeBlock.OutputsList.Count)
        {
            ResizeConnections();
        }
    }

    public void ResizeConnections()
    {
        field.transform.localPosition = startFieldPoint;
        AddInPoints();
        AddOutPoint();
        field.transform.localScale = new Vector3(60, 21 + Math.Max(inPointsList.Count - 1, outPointsList.Count - 1) * 3.5f , 0);
        field.transform.localPosition -= new Vector3(0, Math.Max(inPointsList.Count - 1, outPointsList.Count - 1) * 1.5f, 0);
    }

    public void CheckForNameChange()
    {
        if(textField.GetComponent<TMP_Text>().text != nodeBlock.NodeName)
        {
            textField.GetComponent<TMP_Text>().text = nodeBlock.NodeName;
        }
    }

    public void InstantiateNodeBlockController(INode nodeBlock)
    {
        SetNodeBlock(nodeBlock);
        AddInPoints();
        AddOutPoint();
        instantiated = true;
    }

    public GameObject CreatePoint(GameObject prefab, Vector3 spawnPoint, IConnection con, Transform parent)
    {
        GameObject newPoint = Instantiate(prefab, spawnPoint, Quaternion.identity);
        newPoint.transform.SetParent(parent);
        ConnectionPoint connection = newPoint.GetComponent<ConnectionPoint>();
        connection.InstantiateConnection(con);
        return newPoint;
    }
    public void AddInPoints()
    {
        List<GameObject> newInPointsList = new List<GameObject>();
        Vector3 startPoint = inPointStartPoint.transform.position;
        foreach (IConnection con in nodeBlock.InputsList)
        {
            GameObject newInPoint = inPointsList.Find(point => point.GetComponent<ConnectionPoint>().connection == con);
            if(newInPoint == null)
            {
                newInPoint = CreatePoint(inPointPrefab, startPoint, con, this.transform);
            }
            else
            {
                newInPoint.transform.position = startPoint;
            }

            newInPointsList.Add(newInPoint);
            inPointsList.Remove(newInPoint);
            startPoint += inPointStartPointIncrease;
        }
        //clean up
        foreach(GameObject oldCon in inPointsList)
        {
            oldCon.GetComponent<ConnectionPoint>().connection.Disconnect();
            Destroy(oldCon);
        }
        inPointsList = newInPointsList;
    }
    public void AddOutPoint()
    {
        List<GameObject> newOutPointsList = new List<GameObject>();
        Vector3 startPoint = outPointStartPoint.transform.position;
        foreach (IConnection con in nodeBlock.OutputsList)
        {
            GameObject newOutPoint = outPointsList.Find(point => point.GetComponent<ConnectionPoint>().connection == con);
            if (newOutPoint == null)
            {
                newOutPoint = CreatePoint(outPointPrefab, startPoint, con, this.transform);
            }
            else
            {
                newOutPoint.transform.position = startPoint;
            }

            newOutPointsList.Add(newOutPoint);
            outPointsList.Remove(newOutPoint);
            startPoint += outPointStartPointIncrease;
        }
        //clean up
        foreach (GameObject oldCon in outPointsList)
        {
            oldCon.GetComponent<ConnectionPoint>().connection.Disconnect();
            Destroy(oldCon);
        }
        outPointsList = newOutPointsList;
    }

    public void SetNodeBlock(INode nodeBlock)
    {
        this.nodeBlock = nodeBlock;
        textField.GetComponent<TMP_Text>().text = nodeBlock.NodeName;
    }
}

