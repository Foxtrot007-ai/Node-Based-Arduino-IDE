using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewsManager
{
    public NodeBlock actualView = null;
    public Dictionary<NodeBlock, List<GameObject>> views = new Dictionary<NodeBlock, List<GameObject>>();

    public void DeleteNodeBlock(NodeBlock node)
    {
        foreach (var view in views)
        {
            List<GameObject> toDelete = view.Value.FindAll(x => x.GetComponent<NodeBlockController>().nodeBlock.Equals(node));

            view.Value.RemoveAll(x => x.GetComponent<NodeBlockController>().nodeBlock.Equals(node));

            foreach (var obj in toDelete)
            {
                GameObject.Destroy(obj);
            }
        }
    }
    public void AddNewView(NodeBlock node)
    {
        views.Add(node, new List<GameObject>());
    }
    public void AddToView(GameObject ToAdd)
    {
        views[actualView].Add(ToAdd);
    }
    public void DeleteView(NodeBlock node, List<NodeBlock> languageReferenceList, List<NodeBlock> functionList)
    {
        languageReferenceList.RemoveAll(x => x.Equals(node));
        
        DeleteNodeBlock(node);

        foreach (var obj in views[node])
        {
            GameObject.Destroy(obj);
        }

        functionList.RemoveAll(x => x.Equals(node));
    }
    public void ChangeView(NodeBlock node)
    {
        if (actualView != null)
        {
            foreach (GameObject block in views[actualView])
            {
                block.SetActive(false);
            }
        }
        actualView = node;
        if (actualView != null)
        {
            foreach (GameObject block in views[actualView])
            {
                block.SetActive(true);
            }
        }
    }
}
