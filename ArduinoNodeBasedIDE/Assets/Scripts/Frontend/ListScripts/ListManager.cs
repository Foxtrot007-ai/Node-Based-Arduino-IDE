using Backend.API;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Parent class for list UI controls objects, shouldn't be used directly
public class ListManager : MonoBehaviour
{

    public string lastInput = "";
    public long nodeBlockManagerTimeStamp = 0;
    public GameObject inputField;
    public GameObject listContainer;
    public GameObject buttonContent;
    public List<GameObject> contentObjects;
    public NodeBlockManager nodeBlockManager;

    //Simple start function to instantiate UI control appearance
    public void Start()
    {
        nodeBlockManager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
        lastInput = "";
        UpdateContent();
    }

    //Checking for search bar changes and update list content
    public void Update()
    {
        string readInput = inputField.GetComponent<TMP_InputField>().text;

        if(readInput != lastInput)
        {
            lastInput = readInput;
            UpdateContent();
        }

        if (nodeBlockManager.listTimeStamp != nodeBlockManagerTimeStamp)
        {
            nodeBlockManagerTimeStamp = nodeBlockManager.listTimeStamp;
            UpdateContent();
        }
        

    }

    //function for overriding

    protected virtual List<IUserFunction> GetFunctions()
    {
        return nodeBlockManager.SearchNodeBlocks(this, lastInput);
    }

    protected virtual List<IVariable> GetVariables()
    {
        return null;
    }

    protected virtual List<ITemplate> GetTemplates()
    {
        return null;
    }

    //Create button methods for different type of buttons
    protected GameObject CreateButton()
    {
        GameObject newContent = Instantiate(buttonContent);
        newContent.transform.SetParent(listContainer.transform);
        newContent.transform.localScale = Vector3.one;

        Vector3 defaultPosition = newContent.transform.localPosition;
        newContent.transform.localPosition = new Vector3(defaultPosition.x, defaultPosition.y, 0);
        return newContent;
    }
    protected GameObject CreateButton(IUserFunction node)
    {
        GameObject newContent = CreateButton();
        newContent.GetComponent<ButtonScript>().SetNodeBlock(node);
        return newContent;
    }
    protected GameObject CreateButton(IVariable node)
    {
        GameObject newContent = CreateButton();
        newContent.GetComponent<ButtonScript>().SetNodeBlock(node);
        return newContent;
    }
    protected GameObject CreateButton(ITemplate node)
    {
        GameObject newContent = CreateButton();
        newContent.GetComponent<ButtonScript>().SetNodeBlock(node);
        return newContent;
    }

    //Methods for filling lists
    protected void AddContentFunctions()
    {
        List<IUserFunction> containsList = GetFunctions();

        foreach(IUserFunction function in containsList)
        {
            contentObjects.Add(CreateButton(function));
        }

    }

    protected void AddContentVariables()
    {
        List<IVariable> containsList = GetVariables();

        foreach (IVariable function in containsList)
        {
            contentObjects.Add(CreateButton(function));
        }

    }

    protected void AddContentTemplates()
    {
        List<ITemplate> containsList = GetTemplates();

        foreach (ITemplate function in containsList)
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

    public virtual void UpdateContent()
    {
    }
}
