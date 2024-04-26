using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Backend.API;
using Backend.API.DTO;
using Backend;

public class FunctionFake : IFunctionManage

{
    public string Name { get; set; }

    public IMyType OutputType { get; set; }

    public IVariableList InputList { get; set; }

    public INode StartNode { get; set; }

    private List<INode> MyInstance = new List<INode>();
    public void Change(FunctionManageDto functionManageDto)
    {
        Name = functionManageDto.FunctionName;
        OutputType = functionManageDto.OutputType;
        Debug.Log("FunctionFake:Change -> ");
    }

    public INode CreateFunction()
    {
        Debug.Log("FunctionFake:CreateFunction");
        NodeFake newNode = new NodeFake();
        newNode.NodeName = Name;
        newNode.OutputsList = new List<IConnection>();
        newNode.InputsList = new List<IConnection>();
        MyInstance.Add(newNode);
        return newNode;
    }

    public void DeleteFunction()
    {
        Debug.Log("FunctionFake:DeleteFunction");
        foreach(INode node in MyInstance)
        {
            node.Delete();
        }
        MyInstance.Clear();
    }

}
