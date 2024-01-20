using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class NodeBlockManager : MonoBehaviour
{
    // language Reference objects
    public List<NodeBlock> languageReferenceList;
    public string languageReferenceFile = "Assets/Resources/languageReference.txt";

    // NodeBlock List objects
    public Vector3 nodeBlockSpawnPoint = Vector3.zero;
    public GameObject nodeBlockPrefab;
    public GameObject inNodeBlockPrefab;
    public GameObject outNodeBlockPrefab;
    //views
    public string actualView = "none";
    public Dictionary<string, List<GameObject>> views = new Dictionary<string, List<GameObject>>();

    //Variable List objects
    public List<NodeBlock> variableList = new List<NodeBlock>();

    //my FunctionsList objects
    public List<NodeBlock> myFunctionList = new List<NodeBlock>();


    //Documentation read helper
    public NodeBlock lineReader(string line)
    {
        var parts = line.Split(';');
        string name = parts[0];
        NodeBlockTypes type = (NodeBlockTypes)Enum.Parse(typeof(NodeBlockTypes), parts[1], true);
        //input parse
        var inputparts = parts[2].Split('/');
        int inputListSize = int.Parse(inputparts[0]);
        
        //output parse
        var outputparts = parts[3].Split('/');
        int outputListSize = int.Parse(outputparts[0]);

        NodeBlock block = new NodeBlock(name, type, inputListSize, outputListSize);

        Debug.Log(outputListSize + "," + outputparts[0]);
        for (int i = 0; i < inputListSize; i++)
        {
            block.SetInputType(inputparts[i + 1], i);
        }

        if(outputListSize > 0)
        {
            block.SetOutputType(outputparts[1]);
        }
        

        return block;
    }

    // Start is called before the first frame update
    void Start()
    {
        languageReferenceFile = "Assets/Resources/languageReference.txt";
        languageReferenceList = new List<NodeBlock>();
        var stream = new StreamReader(languageReferenceFile);
        while (!stream.EndOfStream)
            languageReferenceList.Add(lineReader(stream.ReadLine()));

        foreach(NodeBlock function in languageReferenceList)
            Debug.Log(function.GetName());

        instantiateBasicFunctions();
    }

    //NodeBlock functions
    public void SpawnNodeBlock(string name)
    {
        NodeBlock nodeBlock = null;
        foreach(NodeBlock function in languageReferenceList)
        {
            if(function.GetName() == name)
            {
                nodeBlock = function;
            }
        }

        if (nodeBlock == null)
        {
            return;
        }
            
        nodeBlockSpawnPoint.z = 0;
        GameObject temp = Instantiate(nodeBlockPrefab, nodeBlockSpawnPoint, Quaternion.identity);
        NodeBlockController controller = temp.GetComponent<NodeBlockController>();

        for(int i = 0; i < nodeBlock.nextBlockListSize; i++)
        {
            controller.addNextBlock();
        }

        for (int i = 0; i < nodeBlock.inputBlockListSize; i++)
        {
            controller.addInPoint(i, nodeBlock.GetInputType(i));
        }

        if (nodeBlock.hasPreviousBlock)
        {
            controller.addPreviousBlock();
        }

        if (nodeBlock.returnOutputBlock)
        {
            controller.addOutPoint(nodeBlock.GetOutputType());
        }

        controller.SetName(name);
        controller.type = nodeBlock.GetNodeBlockType();
        views[actualView].Add(temp);
    }

    public void SpawnInNodeBlock(NodeBlock nodeBlock)
    {
        nodeBlockSpawnPoint.z = 0;
        GameObject temp = Instantiate(inNodeBlockPrefab, nodeBlockSpawnPoint, Quaternion.identity);
        InNodeBlockController controller = temp.GetComponent<InNodeBlockController>();

        for (int i = 0; i < nodeBlock.nextBlockListSize; i++)
        {
            controller.addNextBlock();
        }

        for (int i = 0; i < nodeBlock.inputBlockListSize; i++)
        {
            controller.addInPoint(i, nodeBlock.GetInputType(i));
        }

        controller.SetName(nodeBlock.GetName() + "(Begin)");
        controller.type = nodeBlock.GetNodeBlockType();
        views[actualView].Add(temp);
    }

    public void SpawnOutNodeBlock(NodeBlock nodeBlock)
    {
        nodeBlockSpawnPoint.z = 0;
        GameObject temp = Instantiate(outNodeBlockPrefab, nodeBlockSpawnPoint, Quaternion.identity);
        OutNodeBlockController controller = temp.GetComponent<OutNodeBlockController>();

        for (int i = 0; i < nodeBlock.nextBlockListSize; i++)
        {
            controller.addNextBlock();
        }

        for (int i = 0; i < nodeBlock.inputBlockListSize; i++)
        {
            controller.addOutPoint(nodeBlock.GetOutputType());
        }

        controller.SetName(nodeBlock.GetName() + "(End)");
        controller.type = nodeBlock.GetNodeBlockType();
        views[actualView].Add(temp);
    }
    public List<string> getLanguageReferenceNames()
    {
        List<string> temp = new List<string>();
        foreach(NodeBlock node in languageReferenceList)
        {
            temp.Add(node.GetName());
        }
        return temp;
    }

    // Variable Functions
    public void AddVariableNodeBlock(string name)
    {
        foreach (NodeBlock node in variableList)
        {
            if(node.GetName() == name)
            {
                return;
            }
        }

        NodeBlock block = new NodeBlock(name, NodeBlockTypes.Variable, 0, 1);
        variableList.Add(block);
    }

    public List<string> getVariablesNames()
    {
        List<string> temp = new List<string>();
        foreach (NodeBlock node in variableList)
        {
            temp.Add(node.GetName());
        }
        return temp;
    }
    public void DeleteVariable(string name)
    {
        variableList.RemoveAll(x => x.GetName() == name);
        foreach (var view in views)
        {
            List<GameObject> toDelete = view.Value.FindAll(x => x.GetComponent<NodeBlockController>().nodeBlockName == name
                                   || x.GetComponent<NodeBlockController>().nodeBlockName == name + "(begin)");

            view.Value.RemoveAll(x => x.GetComponent<NodeBlockController>().nodeBlockName == name
                                    || x.GetComponent<NodeBlockController>().nodeBlockName == name + "(begin)");

            foreach (var node in toDelete)
            {
                Destroy(node);
            }
        }
    }
    public void SpawnVariableNodeBlock(string name)
    {
        NodeBlock nodeBlock = null;
        foreach (NodeBlock variable in variableList)
        {
            if (variable.GetName() == name)
            {
                nodeBlock = variable;
            }
        }

        if (nodeBlock == null)
        {
            return;
        }

        nodeBlockSpawnPoint.z = 0;
        GameObject temp = Instantiate(nodeBlockPrefab, nodeBlockSpawnPoint, Quaternion.identity);
        NodeBlockController controller = temp.GetComponent<NodeBlockController>();

        if (nodeBlock.returnOutputBlock)
        {
            controller.addOutPoint(nodeBlock.GetOutputType());
        }

        controller.SetName(name);
        views[actualView].Add(temp);
    }

    //functions and view
    public List<string> getFunctionsNames()
    {
        List<string> temp = new List<string>();
        foreach (NodeBlock node in myFunctionList)
        {
            temp.Add(node.GetName());
        }
        return temp;
    }
    public void AddNewFunction(string name, int numberOfInput, int numberOfOutput)
    {
        foreach (NodeBlock node in myFunctionList)
        {
            if (node.GetName() == name)
            {
                return;
            }
        }

        NodeBlock block = new NodeBlock(name, NodeBlockTypes.Function, numberOfInput, numberOfOutput);
        myFunctionList.Add(block);
        languageReferenceList.Add(block);
        views.Add(name, new List<GameObject>());
        ChangeView(name);

        SpawnInNodeBlock(block);
        SpawnOutNodeBlock(block);
    }

    public void instantiateBasicFunctions()
    {
        AddNewFunction("setup", 0, 0);
        AddNewFunction("loop", 0, 0);
    }

    public void DeleteView(string name)
    {
        myFunctionList.RemoveAll(x => x.GetName() == name);
        languageReferenceList.RemoveAll(x => x.GetName() == name);
        foreach (var view in views)
        {
            List<GameObject> toDelete = view.Value.FindAll(x => x.GetComponent<NodeBlockController>().nodeBlockName == name);

            view.Value.RemoveAll(x => x.GetComponent<NodeBlockController>().nodeBlockName == name);

            foreach (var node in toDelete)
            {
                Destroy(node);
            }
        }
    }
    public void ChangeView(string name)
    {
        if(actualView != "none")
        {
            foreach (GameObject block in views[actualView])
            {
                block.SetActive(false);
            }
        }
        actualView = name;
        if (actualView != "none")
        {
            foreach (GameObject block in views[actualView])
            {
                block.SetActive(true);
            }
        }
    }

    public void updateTypes(int index, string name, string newType, NodeBlockTypes nodeBlockType)
    {
        NodeBlock temp;
        if (nodeBlockType.Equals(NodeBlockTypes.Function))
        {
            temp = myFunctionList.Find(x => x.GetName() == name);
        }
        else if (nodeBlockType.Equals(NodeBlockTypes.Variable))
        {
            temp = variableList.Find(x => x.GetName() == name);
        }
        else
        {
            temp = null;
            Debug.Log("problem with change definition");
        }
        
        temp.SetInputType(newType, index);
        foreach (var view in views)
        {
           foreach(var node in view.Value)
           {
                NodeBlockController controller = node.GetComponent<NodeBlockController>();
                if (controller != null && controller.nodeBlockName == name)
                {
                    controller.inPointsList[index].GetComponent<ConnectionPoint>().changeType(newType);
                }
           }
        }
    }
}
