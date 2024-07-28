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
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        nodeBlockManager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
    }
    public void Update()
    {
        if (instantiated)
        {
            CheckForNameChange();
            //todo resizeConnections()
        }
    }

    public void ResizeConnections()
    {
        AddInPoints();
        AddOutPoint();
        field.transform.localScale = new Vector3(60, Math.Max(inPointsList.Count, outPointsList.Count), 0);
    }

    private void CheckForNameChange()
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

    private GameObject CreatePoint(GameObject prefab, Vector3 spawnPoint, IConnection con, Transform parent)
    {
        GameObject newPoint = Instantiate(prefab, spawnPoint, Quaternion.identity);
        newPoint.transform.SetParent(parent);
        ConnectionPoint connection = newPoint.GetComponent<ConnectionPoint>();
        connection.InstantiateConnection(con);
        return newPoint;
    }
    private void AddInPoints()
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
            startPoint += inPointStartPointIncrease;
        }
        inPointsList = newInPointsList;
    }
    private void AddOutPoint()
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
            startPoint += outPointStartPointIncrease;
        }
        outPointsList = newOutPointsList;
    }

    private void SetNodeBlock(INode nodeBlock)
    {
        this.nodeBlock = nodeBlock;
        textField.GetComponent<TMP_Text>().text = nodeBlock.NodeName;
    }
}

