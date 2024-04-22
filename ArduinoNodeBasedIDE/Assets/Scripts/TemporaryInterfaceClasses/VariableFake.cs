using Backend;
using Backend.API;
using Backend.API.DTO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableFake : IVariableManage
{
    public string Name { get; set; }

    public IMyType Type { get; set; }

    private List<INode> MyInstance = new List<INode>();

    public void Change(VariableManageDto variableManageDto)
    {
        Name = variableManageDto.VariableName;
        Type = variableManageDto.Type;
        Debug.Log("VariableFake:Change");
    }

    public INode CreateVariable()
    {
        Debug.Log("VariableFake:CreateFunction");
        NodeFake newNode = new NodeFake();
        newNode.NodeName = Name;
        MyInstance.Add(newNode);
        return newNode;
    }

    public void DeleteVariable()
    {
        Debug.Log("FunctionFake:DeleteFunction");
        foreach (INode node in MyInstance)
        {
            node.Delete();
        }
        MyInstance.Clear();
    }
}
