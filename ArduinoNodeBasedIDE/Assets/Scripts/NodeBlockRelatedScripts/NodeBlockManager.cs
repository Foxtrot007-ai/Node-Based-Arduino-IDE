using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeBlockManager : MonoBehaviour
{
    // language Reference objects
    public LanguageReferenceParser languageReferenceParser = new LanguageReferenceParser();
    public List<NodeBlock> languageReferenceList = new List<NodeBlock>();
    
    // NodeBlock List objects
    public Vector3 nodeBlockSpawnPoint = Vector3.zero;
    public GameObject nodeBlockPrefab;

    //views
    public ViewsManager viewsManager = new ViewsManager();

    //Variable List objects
    public List<NodeBlock> variableList = new List<NodeBlock>();

    //my FunctionsList objects
    public List<NodeBlock> myFunctionList = new List<NodeBlock>();

    //NodeBlock Editor
    public GameObject nodeBlockEditor;

    //Variable Editor
    public GameObject variableEditorPrefab;
    public GameObject currentVariableEditor;

    // Start is called before the first frame update
    void Start()
    {
        languageReferenceList = languageReferenceParser.loadReferenceList();
        instantiateBasicFunctions();
    }

    //Starting functions
    
    public void instantiateBasicFunctions()
    {
        AddNodeBlock("setup", 0, 0);
        AddNodeBlock("loop", 0, 0);
    }

    //SearchNodeBlock Section
    public List<NodeBlock> SearchNodeBlocks(List<NodeBlock> list, String nodeBlockName)
    {
        List<NodeBlock> temp = new List<NodeBlock>();
        foreach (NodeBlock node in list)
        {
            if (node.GetName().Contains(nodeBlockName)) {
                temp.Add(node);
            }
        }
        return temp;
    }

    public List<NodeBlock> SearchNodeBlocks(ListManager manager, String nodeBlockName)
    {
        //throw exception
        return null;
    }
    public List<NodeBlock> SearchNodeBlocks(GlobalVariableListManager manager, String nodeBlockName)
    {
        return SearchNodeBlocks(variableList, nodeBlockName);
    }

    public List<NodeBlock> SearchNodeBlocks(LocalVariableListManager manager, String nodeBlockName)
    {
        return SearchNodeBlocks(viewsManager.GetLocalVariables(), nodeBlockName);
    }

    public List<NodeBlock> SearchNodeBlocks(FunctionListManager manager, String nodeBlockName)
    {
        return SearchNodeBlocks(myFunctionList, nodeBlockName);
    }

    public List<NodeBlock> SearchNodeBlocks(ReferenceListManager manager, String nodeBlockName)
    {
        return SearchNodeBlocks(languageReferenceList, nodeBlockName);
    }

    //SpawnNodeBlock section
    public GameObject SpawnNodeBlockWithoutValidation(NodeBlock node)
    {
        nodeBlockSpawnPoint.z = 0;
        GameObject nodeBlockObject = Instantiate(nodeBlockPrefab, nodeBlockSpawnPoint, Quaternion.identity);
        nodeBlockObject.GetComponent<NodeBlockController>().InstantiateNodeBlockController(node);

        viewsManager.AddToView(nodeBlockObject);
        return nodeBlockObject;
    }
    public GameObject SpawnNodeBlock(List<NodeBlock> list, NodeBlock node)
    {
        if (!list.Contains(node))
        {
            return null;
        }

        return SpawnNodeBlockWithoutValidation(node);
    }
    public void SpawnNodeBlock(ButtonScript button, NodeBlock node)
    {
        //throw exception
        Debug.Log("what?");
    }
    public void SpawnNodeBlock(ReferenceButtonScript button, NodeBlock node)
    {
        SpawnNodeBlock(languageReferenceList, node);
    }
    public void SpawnNodeBlock(VariableButtonScript button, NodeBlock node)
    {
        SpawnNodeBlock(variableList, node);
    }
    public void SpawnNodeBlock(FunctionButtonScript button, NodeBlock node)
    {
        SpawnNodeBlock(myFunctionList, node);
    }

    //DeleteNodeBlock Section
    public void DeleteNodeBlock(ButtonScript button, NodeBlock node)
    {
        //throw exception
        Debug.Log("what?");
    }
    public void DeleteNodeBlock(VariableButtonScript button, NodeBlock node)
    {
        variableList.Remove(node);
        viewsManager.DeleteNodeBlock(node);
    }

    public void DeleteNodeBlock(FunctionButtonScript button, NodeBlock node)
    {
        viewsManager.DeleteView(node, languageReferenceList, myFunctionList);
    }

    public void DeleteNodeBlock(ReferenceButtonScript button, NodeBlock node)
    {
        //throw exception
        Debug.Log("what?");
    }

    //AddNodeBlock Section

    public void AddNodeBlock(string name, int numberOfInput, int numberOfOutput)
    {
        NodeBlock newNodeBlock = new NodeBlock(name, NodeBlockTypes.Function, numberOfInput, numberOfOutput);

        if (myFunctionList.Contains(newNodeBlock))
        {
            return;
        }

        myFunctionList.Add(newNodeBlock);
        languageReferenceList.Add(newNodeBlock);
        viewsManager.AddNewView(newNodeBlock);
        viewsManager.ChangeView(newNodeBlock);

        //making start node
        NodeBlock startNodeBlock = new NodeBlock(name + "(Start)", NodeBlockTypes.Function, 0, 0);
        startNodeBlock.hasPreviousBlock = false;

        SpawnNodeBlockWithoutValidation(startNodeBlock).GetComponent<NodeBlockController>().isStartNodeBlock = true;
    }
    public void AddNodeBlock(GlobalVariableListManager list, string name)
    {
        NodeBlock newNodeBlock = new NodeBlock(name, NodeBlockTypes.Variable, 0, 1);

        if (variableList.Contains(newNodeBlock))
        {
            return;
        }

        variableList.Add(newNodeBlock);
    }

    public void AddNodeBlock(LocalVariableListManager list, string name)
    {
        NodeBlock newNodeBlock = new NodeBlock(name, NodeBlockTypes.Variable, 0, 1);

        if (viewsManager.GetLocalVariables().Contains(newNodeBlock))
        {
            return;
        }

        viewsManager.AddVariableToView(newNodeBlock);
    }
    //Update types

    public void updateInputType(int index, string newType, NodeBlock node)
    {
        node.SetInputType(newType, index);
    }

    public void updateOutputType(string newType, NodeBlock node)
    {
        node.SetOutputType(newType);
    }


    //Set NodeBlock to Edit section
    public void SetNodeBlockToEdit(ButtonScript button, NodeBlock nodeBlock)
    {
        //throw exception
        Debug.Log("what?");
    }

    public void SetNodeBlockToEdit(ReferenceButtonScript button, NodeBlock nodeBlock)
    {
        //throw exception
        Debug.Log("what?");
    }

    public void SetNodeBlockToEdit(List<NodeBlock> list, NodeBlock nodeBlock)
    {
        nodeBlockEditor.GetComponent<NodeBlockEditor>().SetNodeBlockToEdit(list.Find(x => x.Equals(nodeBlock)));
        nodeBlockEditor.SetActive(true);
    }
    public void SetNodeBlockToEdit(VariableButtonScript button, NodeBlock nodeBlock)
    {
        if(currentVariableEditor == null)
        {
            currentVariableEditor = Instantiate(variableEditorPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            currentVariableEditor.GetComponent<VariableEditor>().InstantiateEditor(nodeBlock);
        }
    }

    public void SetNodeBlockToEdit(FunctionButtonScript button, NodeBlock nodeBlock)
    {
        SetNodeBlockToEdit(myFunctionList, nodeBlock);
    }

    public void ChangeView(FunctionButtonScript button, NodeBlock nodeBlock)
    {
        viewsManager.ChangeView(nodeBlock);
    }
}
