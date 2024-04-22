using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Backend.API;
public class VariableListFake : IVariableList
{
    public List<IVariableManage> VariableManages { get; set; }

    public void AddVariable(IVariableManage variableManage)
    {
        VariableManages.Add(variableManage);
    }
}
