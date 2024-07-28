using Backend.API;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        UpdateContent();
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
    protected virtual List<IFunctionManage> GetFunctions()
    {
        return nodeBlockManager.SearchNodeBlocks(this, lastInput);
    }

    protected virtual List<IVariableManage> GetVariables()
    {
        return null;
    }
    protected GameObject CreateButton()
    {
        GameObject newContent = Instantiate(buttonContent);
        newContent.transform.SetParent(listContainer.transform);
        newContent.transform.localScale = Vector3.one;
        return newContent;
    }
    protected GameObject CreateButton(IFunctionManage node)
    {
        GameObject newContent = CreateButton();
        newContent.GetComponent<ButtonScript>().SetNodeBlock(node);
        return newContent;
    }
    protected GameObject CreateButton(IVariableManage node)
    {
        GameObject newContent = CreateButton();
        newContent.GetComponent<ButtonScript>().SetNodeBlock(node);
        return newContent;
    }
    protected void AddContentFunctions()
    {
        List<IFunctionManage> containsList = GetFunctions();

        foreach(IFunctionManage function in containsList)
        {
            contentObjects.Add(CreateButton(function));
        }

    }

    protected void AddContentVariables()
    {
        List<IVariableManage> containsList = GetVariables();

        foreach (IVariableManage function in containsList)
        {
            contentObjects.Add(CreateButton(function));
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

    protected virtual void UpdateContent()
    {
    }
}
