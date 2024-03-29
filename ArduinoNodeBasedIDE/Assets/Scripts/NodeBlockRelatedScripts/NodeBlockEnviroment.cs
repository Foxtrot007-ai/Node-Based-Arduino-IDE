using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NodeBlockEnviroment
{
    private NodeBlockManager manager;
    private List<NodeBlock> globalVariables = new List<NodeBlock>();
    private List<NodeBlock> globalFunctions = new List<NodeBlock>();
   
    private void GenerateGlobalVariables()
    {
       foreach (NodeBlock block in manager.variableList)
       {
            globalVariables.Add((NodeBlock) block.Clone());
       }
    }

    private void fillFunction(ref NodeBlock function)
    {
        List<GameObject> blocks = manager.viewsManager.views[function];
        List<NodeBlock> nodeblocks = new List<NodeBlock>();
        foreach(GameObject block in blocks)
        {
            NodeBlock node = block.GetComponent<NodeBlockController>().nodeBlock;
            NodeBlock temp = new NodeBlock(node.GetName(),
                                           node.GetNodeBlockType(),
                                           node.GetNumberOfInputs(),
                                           node.GetOutputBlock() != null ? 1 : 0);
            nodeblocks.Add(temp);
        }
        
        //todo

    }

    private void GenerateGlobalFunctions()
    {
        foreach (NodeBlock function in manager.myFunctionList)
        {
            NodeBlock temp = (NodeBlock)function.Clone();
            fillFunction(ref temp);
            globalFunctions.Add(temp);
        }
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
