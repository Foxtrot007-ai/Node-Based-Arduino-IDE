using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using Backend;
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
        }
    }

    public void ResizeConnections()
    {
        //to do
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
        instantiated = true;

        SetNodeBlock(nodeBlock);
        AddInPoints();
        AddOutPoint();
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
        AddPoints(nodeBlock.InputsList, inPointsList, inPointPrefab, inPointStartPoint, inPointStartPointIncrease);
    }
    private void AddOutPoint()
    {
        AddPoints(nodeBlock.OutputsList, outPointsList, outPointPrefab, outPointStartPoint, outPointStartPointIncrease);
    }
    private void AddPoints(List<IConnection> list, List<GameObject> objectList, GameObject prefab, GameObject startPoint, Vector3 increase)
    {
        foreach (IConnection con in list)
        {
            GameObject newInPoint = CreatePoint(prefab, startPoint.transform.position, con, this.transform);
            objectList.Add(newInPoint);
            startPoint.transform.position += increase;
        }
    }

    private void SetNodeBlock(INode nodeBlock)
    {
        this.nodeBlock = nodeBlock;
        textField.GetComponent<TMP_Text>().text = nodeBlock.NodeName;
    }
}

