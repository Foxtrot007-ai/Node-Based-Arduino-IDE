using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewsManager
{
    public NodeBlock actualView = null;
    public Dictionary<NodeBlock, Tuple<List<GameObject>, List<NodeBlock>>> views = new Dictionary<NodeBlock, Tuple<List<GameObject>, List<NodeBlock>>>();

    public void DeleteNodeBlock(NodeBlock node)
    {
        foreach (var view in views)
        {
            List<GameObject> toDelete = view.Value.Item1.FindAll(x => x.GetComponent<NodeBlockController>().nodeBlock.Equals(node));

            view.Value.Item1.RemoveAll(x => x.GetComponent<NodeBlockController>().nodeBlock.Equals(node));

            foreach (var obj in toDelete)
            {
                GameObject.Destroy(obj);
            }
        }
    }
    public void AddNewView(NodeBlock node)
    {
        views.Add(node, new Tuple<List<GameObject>, List<NodeBlock>>(new List<GameObject>(), new List<NodeBlock>()));
    }
    public void AddToView(GameObject ToAdd)
    {
        views[actualView].Item1.Add(ToAdd);
    }
    public void AddVariableToView(NodeBlock variable)
    {
        views[actualView].Item2.Add(variable);
    }
    public List<NodeBlock> GetLocalVariables()
    {
        return views[actualView].Item2;
    }
    public void DeleteView(NodeBlock node, List<NodeBlock> languageReferenceList, List<NodeBlock> functionList)
    {
        languageReferenceList.RemoveAll(x => x.Equals(node));
        
        DeleteNodeBlock(node);

        foreach (var obj in views[node].Item1)
        {
            GameObject.Destroy(obj);
        }

        functionList.RemoveAll(x => x.Equals(node));
    }
    public void ChangeView(NodeBlock node)
    {
        if (actualView != null)
        {
            foreach (GameObject block in views[actualView].Item1)
            {
                if(block != null)
                    block.SetActive(false);
            }
        }
        actualView = node;
        if (actualView != null)
        {
            foreach (GameObject block in views[actualView].Item1)
            {
                if (block != null)  
                    block.SetActive(true);
            }
        }
    }
}
