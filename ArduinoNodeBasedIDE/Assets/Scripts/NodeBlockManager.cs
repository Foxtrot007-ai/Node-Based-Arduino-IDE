using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        int inputListSize = int.Parse(parts[2]);
        int outputListSize = int.Parse(parts[3]);
        NodeBlock block = new NodeBlock(name, type, inputListSize, outputListSize);
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
            controller.addInPoint();
        }

        if (nodeBlock.hasPreviousBlock)
        {
            controller.addPreviousBlock();
        }

        if (nodeBlock.returnOutputBlock)
        {
            controller.addOutPoint();
        }

        controller.SetName(name);
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
            controller.addOutPoint();
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
    public void AddNewFunction(string name)
    {
        foreach (NodeBlock node in myFunctionList)
        {
            if (node.GetName() == name)
            {
                return;
            }
        }

        NodeBlock block = new NodeBlock(name, NodeBlockTypes.Function, 0, 0);
        myFunctionList.Add(block);
        languageReferenceList.Add(block);
        views.Add(name, new List<GameObject>());
        ChangeView(name);

        NodeBlock blockbegin = new NodeBlock(name + "(begin)", NodeBlockTypes.Function, 0, 0);
        languageReferenceList.Add(blockbegin);
        SpawnNodeBlock(name + "(begin)");
    }

    public void instantiateBasicFunctions()
    {
        AddNewFunction("setup");
        AddNewFunction("loop");
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
}
