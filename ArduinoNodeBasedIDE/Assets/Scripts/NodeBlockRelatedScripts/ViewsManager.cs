using Backend.API;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ViewsManager
{
    public IFunctionManage actualView = null;
    public Dictionary<IFunctionManage, Tuple<List<GameObject>, List<IVariableManage>>> views = new Dictionary<IFunctionManage, Tuple<List<GameObject>, List<IVariableManage>>>();

    public void AddNewView(IFunctionManage node)
    {
        views.Add(node, new Tuple<List<GameObject>, List<IVariableManage>>(new List<GameObject>(), new List<IVariableManage>()));
    }
    public void AddToView(GameObject ToAdd)
    {
        views[actualView].Item1.Add(ToAdd);
    }
    public void AddVariableToView(IVariableManage variable)
    {
        views[actualView].Item2.Add(variable);
    }
    public List<IVariableManage> GetLocalVariables()
    {
        List<IVariableManage> empty = new List<IVariableManage>();

        if (views.ContainsKey(actualView))
        {
            return views[actualView].Item2 ?? empty;
        }

        return empty;
    }
    public void DeleteView(IFunctionManage node, List<IFunctionManage> functionList)
    {
        foreach (var obj in views[node].Item1)
        {
            GameObject.Destroy(obj);
        }

        functionList.RemoveAll(x => x.Equals(node));
    }
    public void ChangeView(IFunctionManage node)
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
