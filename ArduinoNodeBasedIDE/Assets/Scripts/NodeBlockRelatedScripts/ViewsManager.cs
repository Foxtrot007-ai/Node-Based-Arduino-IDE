using Backend.API;
using System.Collections.Generic;
using UnityEngine;


//Script which deciding with Nodeblock object should be currently show to user based on current edited function
public class ViewsManager
{
    public IFunction actualView = null;
    public Dictionary<IFunction, List<GameObject>> views = new Dictionary<IFunction, List<GameObject>>();

    public void AddNewView(IFunction userFunction)
    {
        views.Add(userFunction, new List<GameObject>());
    }
    public void AddToView(GameObject ToAdd)
    {
        views[actualView].Add(ToAdd);
    }

    public List<IVariable> GetLocalVariables()
    {
       return (actualView == null) ? null : actualView.Variables.Variables;
    }
    public void DeleteView(IFunction node, IUserFunctionsManager functionList)
    {
        foreach (var obj in views[node])
        {
            GameObject.Destroy(obj);
        }

        functionList.DeleteFunction((IUserFunction) node);
    }

    public void DeleteAllView()
    {
        foreach (var pair in views)
        {
            foreach (var obj in pair.Value)
            {
                GameObject.Destroy(obj);
            }
        }
    }

    public void ChangeView(IFunction node)
    {
        if (actualView != null)
        {
            foreach (GameObject block in views[actualView])
            {
                if(block != null)
                    block.SetActive(false);
            }
        }
        actualView = node;
        if (actualView != null)
        {
            foreach (GameObject block in views[actualView])
            {
                if (block != null)  
                    block.SetActive(true);
            }
        }
    }
}
