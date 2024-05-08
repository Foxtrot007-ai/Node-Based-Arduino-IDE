using Backend.API.DTO;
using Backend.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Backend.Type;

public static class SimpleNodeBlockMaker
{
    public static IFunctionManage MakeFunction(string name, EType type)
    {
        IFunctionManage node = new FunctionFake()
        { 
            InputList = new VariableListFake() 
            { 
                VariableManages = new List<IVariableManage>()
            }
        };

        node.Change(new FunctionManageDto
        {
            FunctionName = name,
            OutputType = new MyTypeFake
            {
                EType = type,
                TypeName = type.ToString()
            }
        });
        return node;
    }

    public static IVariableManage MakeVariable(string name, EType type)
    {
        IVariableManage node = new VariableFake();
        node.Change(new VariableManageDto
        {
            VariableName = name,
            Type = new MyTypeFake
            {
                EType = type,
                TypeName = type.ToString()
            }
        });
        return node;
    }
}
