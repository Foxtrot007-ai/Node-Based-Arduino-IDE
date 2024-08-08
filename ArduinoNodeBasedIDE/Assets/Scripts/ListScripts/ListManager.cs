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
