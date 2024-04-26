using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Backend.API;
using Backend.API.DTO;

public class VariableListFake : IVariableList
{
    public List<IVariableManage> VariableManages { get; set; }

    public void AddVariable(IVariableManage variableManage)
    {
        VariableManages.Add(variableManage);
    }

    public IVariableManage AddVariable(VariableManageDto variableManageDto)
    {
        VariableFake variableFake = new VariableFake();
        variableFake.Change(variableManageDto);
        VariableManages.Add(variableFake);
        return variableFake;
    }

    public void DeleteVariable(IVariableManage variableManage)
    {
        VariableManages.Remove(variableManage);
    }
}
