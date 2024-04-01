using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class NodeBlockController : MonoBehaviour
{
    public NodeBlock nodeBlock;
  

    public GameObject textField;

    public GameObject field;

    //test variable
    public bool isStartNodeBlock;

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

    public bool instantiated = false;

    public NodeBlockManager nodeBlockManager;

    public DateTime lastTimeStamp;
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
            if (lastTimeStamp != nodeBlock.lastChange)
            {
                ReloadBlock();
            }
        }
        
    }
    public void ReloadBlock()
    {
        DestroyConnections();
        InstantiateNodeBlockController(this.nodeBlock);
    }
    public void DestroyConnections()
    {
        foreach(GameObject p in inPointsList)
        {
            Destroy(p);
            inPointStartPoint.transform.position -= inPointStartPointIncrease;
        }
        inPointsList.Clear();
        if(outPoint != null)
        {
            Destroy(outPoint);
        }
        foreach (GameObject p in nextBlockList)
        {
            Destroy(p);
            nextBlockStartPoint.transform.position -= nextBlockStartPointIncrease;
        }
        nextBlockList.Clear();
        if (previousBlock != null)
        {
            Destroy(previousBlock);
        }
        instantiated = false;
    }
    public void InstantiateNodeBlockController(NodeBlock node)
    {
        lastTimeStamp = node.lastChange;
     

        instantiated = true;

        SetNodeBlock(node);
        AddInPoints();
        AddOutPoint();
        AddNextBlocks();
        AddPreviousBlock();
    }
    /*
    private void Unconnect(GameObject point)
    {
        ConnectionPoint connection = point.GetComponent<ConnectionPoint>();
        if (connection.connectedPoint != null)
        {
            connection.Unconnect();
        }
        
    }
    */
    public void UnconnectAll()
    {
        /*
        foreach(var point in inPointsList)
        {
            Unconnect(point);
        }

        if(outPoint != null)
        {
            Unconnect(outPoint);
        }

        foreach (var point in nextBlockList)
        {
            Unconnect(point);
        }

        if (previousBlock != null)
        {
            Unconnect(previousBlock);
        }
        */
    }
    
    private GameObject CreatePoint(GameObject prefab, Vector3 spawnPoint, NodeBlock nodeBlock, string type, int connectionIndex, Transform parent)
    {
        GameObject newPoint = Instantiate(prefab, spawnPoint, Quaternion.identity);
        newPoint.transform.SetParent(parent);
        ConnectionPoint connection = newPoint.GetComponent<ConnectionPoint>();
        connection.InstantiateConnection(this, type, connectionIndex);
        return newPoint;
    }
    private void AddInPoints()
    {
        for(int i = 0; i < nodeBlock.GetNumberOfInputs(); i++)
        {
            GameObject newInPoint = CreatePoint(inPointPrefab, inPointStartPoint.transform.position, nodeBlock, nodeBlock.GetInputType(i), i, this.transform);
            inPointsList.Add(newInPoint);
            inPointStartPoint.transform.position += inPointStartPointIncrease;
        }
    }

    private void AddOutPoint()
    {
        if (nodeBlock.returnOutputBlock)
        {
            outPoint = CreatePoint(outPointPrefab, outPointStartPoint.transform.position, nodeBlock, nodeBlock.GetOutputType(), 0, this.transform);
        }
        
    }

    private void AddNextBlocks()
    {
        for (int i = 0; i < nodeBlock.nextBlockListSize; i++)
        {
            GameObject newNextPoint = CreatePoint(nextBlockPrefab, nextBlockStartPoint.transform.position, nodeBlock, "", 0, this.transform);
            nextBlockList.Add(newNextPoint);
            nextBlockStartPoint.transform.position += nextBlockStartPointIncrease;
        }  
    }
    private void AddPreviousBlock()
    {
        if (nodeBlock.hasPreviousBlock)
        {
            previousBlock = CreatePoint(previousBlockPrefab, previousBlockStartPoint.transform.position, nodeBlock, "", 0, this.transform);
        }
    }

    private void SetNodeBlock(NodeBlock node)
    {
        this.nodeBlock = node;
        textField.GetComponent<TMP_Text>().text = nodeBlock.GetName();
    }
}

