using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ListManager : MonoBehaviour
{

    public string lastInput = "";

    public GameObject inputField;
    public GameObject listContainer;
    public GameObject buttonContent;
    public List<GameObject> contentObjects;
    public NodeBlockManager nodeBlockManager;

    public void Start()
    {
        nodeBlockManager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
        lastInput = "";
        AddContent();
    }
    public void Update()
    {
        string readInput = inputField.GetComponent<TMP_InputField>().text;

        if(readInput != lastInput)
        {
            lastInput = readInput;
            UpdateContent();
        }
    }
    protected virtual List<NodeBlock> GetNodeBlocks()
    {
        return nodeBlockManager.SearchNodeBlocks(this, lastInput);
    }

    protected GameObject CreateButton(NodeBlock node)
    {
        GameObject newContent = Instantiate(buttonContent);
        newContent.transform.SetParent(listContainer.transform);
        newContent.GetComponent<ButtonScript>().SetNodeBlock(node);
        newContent.transform.localScale = Vector3.one;
        return newContent;
    }
    protected void AddContent()
    {
        List<NodeBlock> containsList = GetNodeBlocks();

        foreach(NodeBlock node in containsList)
        {
            contentObjects.Add(CreateButton(node));
        }

    }

    protected void DestroyContent()
    {
        foreach(GameObject content in contentObjects)
        {
            Destroy(content);
        }

        contentObjects.Clear();
    }

    protected void UpdateContent()
    {
        DestroyContent();
        AddContent();
    }
}
