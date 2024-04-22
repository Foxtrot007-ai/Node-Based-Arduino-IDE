using Backend;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NodeBlockEnviroment
{
    private NodeBlockManager manager;
    private List<INode> globalVariables = new List<INode>();
    private List<INode> globalFunctions = new List<INode>();
   
    private void GenerateGlobalVariables()
    {
       
    }

    private void fillFunction(ref NodeBlock function)
    {
     

    }

    private void GenerateGlobalFunctions()
    {
        
    }

    public void GenerateEnviroment()
    {
        manager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
        GenerateGlobalVariables();
        GenerateGlobalFunctions();
    }
    public void SaveToJsonFile(string file)
    {

    }

    public void LoadFromJsonFile(string file)
    {

    }
   
}
