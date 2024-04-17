using Backend;
using Backend.API;
using Backend.API.DTO;
using Backend.Node;
using Backend.Type;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LanguageReferenceParser
{
    public string languageReferenceFile = "Assets/Resources/languageReference.txt";
    public List<IFunctionManage> functions;
    public List<IVariableManage> variables;

    public void CreateNode()
    {
        //to do
    }
    public void lineReader(string line)
    {
        //to do
    }
    public void loadReferences()
    {
        functions = new List<IFunctionManage>();
        variables = new List<IVariableManage>();
    }
}
