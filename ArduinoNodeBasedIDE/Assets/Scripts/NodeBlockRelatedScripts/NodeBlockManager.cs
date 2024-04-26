using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using Backend.API;
using Backend;

public class NodeBlockManager : MonoBehaviour
{
    // language Reference objects
    public LanguageReferenceParser languageReferenceParser = new LanguageReferenceParser();

    // NodeBlock List objects
    public Vector3 nodeBlockSpawnPoint = Vector3.zero;
    public GameObject nodeBlockPrefab;

    //views
    public ViewsManager viewsManager = new ViewsManager();

    //Variable List objects
    public List<IVariableManage> variableList = new List<IVariableManage>();

    public GameObject localVariableList;

    //my FunctionsList objects
    public List<IFunctionManage> myFunctionList = new List<IFunctionManage>();

    //NodeBlock Editor
    public GameObject nodeBlockEditor;

    //Variable Editor
    public GameObject variableEditorPrefab;
    public GameObject currentVariableEditor;

    // Start is called before the first frame update
    void Start()
    {
        languageReferenceParser.loadReferences();
        instantiateBasicFunctions();
    }

    //Starting functions
    
    public void instantiateBasicFunctions()
    {
        AddNodeBlock("setup", 0, 0);
        AddNodeBlock("loop", 0, 0);
    }

    //SearchNodeBlock Section
    public List<IFunctionManage> SearchNodeBlocks(List<IFunctionManage> list, String nodeBlockName)
    {
        List<IFunctionManage> temp = new List<IFunctionManage>();
        foreach (IFunctionManage node in list)
        {
            if (node.Name.Contains(nodeBlockName)) {
                temp.Add(node);
            }
        }
        return temp;
    }

    public List<IVariableManage> SearchNodeBlocks(List<IVariableManage> list, String nodeBlockName)
    {
        List<IVariableManage> temp = new List<IVariableManage>();
        foreach (IVariableManage node in list)
        {
            if (node.Name.Contains(nodeBlockName))
            {
                temp.Add(node);
            }
        }
        return temp;
    }

    public List<IFunctionManage> SearchNodeBlocks(ListManager manager, String nodeBlockName)
    {
        //throw exception
        return null;
    }
   
    public List<IVariableManage> SearchNodeBlocks(GlobalVariableListManager manager, String nodeBlockName)
    {
        return SearchNodeBlocks(variableList, nodeBlockName);
    }

    public List<IVariableManage> SearchNodeBlocks(LocalVariableListManager manager, String nodeBlockName)
    {
        return SearchNodeBlocks(viewsManager.GetLocalVariables(), nodeBlockName);
    }

    public List<IFunctionManage> SearchNodeBlocks(FunctionListManager manager, String nodeBlockName)
    {
        return SearchNodeBlocks(myFunctionList, nodeBlockName);
    }

    public List<IFunctionManage> SearchNodeBlocks(ReferenceListManager manager, String nodeBlockName)
    {
        return SearchNodeBlocks(languageReferenceParser.functions, nodeBlockName);
    }

    //SpawnNodeBlock section
 
    public GameObject SpawnNodeBlockWithoutValidation()
    {
        nodeBlockSpawnPoint.z = 0;
        GameObject nodeBlockObject = Instantiate(nodeBlockPrefab, nodeBlockSpawnPoint, Quaternion.identity);
        viewsManager.AddToView(nodeBlockObject);
        return nodeBlockObject;
    }
    public GameObject SpawnNodeBlock(IFunctionManage function)
    {
        GameObject nodeBlockObject = SpawnNodeBlockWithoutValidation();
        nodeBlockObject.GetComponent<NodeBlockController>().InstantiateNodeBlockController(function.CreateFunction());
        return nodeBlockObject;
    }

    public GameObject SpawnNodeBlock(IVariableManage variable)
    {
        GameObject nodeBlockObject = SpawnNodeBlockWithoutValidation();
        nodeBlockObject.GetComponent<NodeBlockController>().InstantiateNodeBlockController(variable.CreateVariable());
        return nodeBlockObject;
    }
    public void SpawnNodeBlock(ButtonScript button)
    {
        //throw exception
        Debug.Log("what?");
    }
    public void SpawnNodeBlock(ReferenceButtonScript button)
    {
        SpawnNodeBlock(button.function);
    }
    public void SpawnNodeBlock(VariableButtonScript button)
    {
        SpawnNodeBlock(button.variable);
    }
    public void SpawnNodeBlock(FunctionButtonScript button)
    {
        SpawnNodeBlock(button.function);
    }
    public void SpawnNodeBlock(InputButtonScript button)
    {
        SpawnNodeBlock(button.variable);
    }

    //DeleteNodeBlock Section
    public void DeleteNodeBlock(ButtonScript button)
    {
        //throw exception
        Debug.Log("what?");
    }
    public void DeleteNodeBlock(VariableButtonScript button)
    {
        button.variable.DeleteVariable();
        variableList.Remove(button.variable);
        viewsManager.views[viewsManager.actualView].Item2.Remove(button.variable);
    }

    public void DeleteNodeBlock(FunctionButtonScript button)
    {
        button.function.DeleteFunction();
        viewsManager.DeleteView(button.function, myFunctionList);
    }
    public void DeleteNodeBlock(InputButtonScript button)
    {
        //button.parentFunction.InputList.DelVariable(button.variable);
    }

    public void DeleteNodeBlock(ReferenceButtonScript button)
    {
        //throw exception
        Debug.Log("what?");
    }

    //AddNodeBlock Section

    public void AddNodeBlock(string name, int numberOfInput, int numberOfOutput)
    {
        IFunctionManage function = new FunctionFake
        {
            Name = name,
            OutputType = new MyTypeFake
            {
                EType = Backend.Type.EType.Void,
                TypeName = "Void"
            },
            InputList = new VariableListFake
            { 
                VariableManages = new List<IVariableManage>()
            }
        };

        if (myFunctionList.Contains(function))
        {
            return;
        }

        myFunctionList.Add(function);
        viewsManager.AddNewView(function);
        viewsManager.ChangeView(function);

        //making start node

        NodeBlockController startNodeBlockObject = SpawnNodeBlockWithoutValidation().GetComponent<NodeBlockController>();
        startNodeBlockObject.isStartNodeBlock = true;
        startNodeBlockObject.InstantiateNodeBlockController(function.CreateFunction());
    }
    public void AddNodeBlock(GlobalVariableListManager list, string name)
    {
        IVariableManage variable = new VariableFake 
        { 
            Name = name, 
            Type = new MyTypeFake 
            { 
                EType = Backend.Type.EType.Int, 
                TypeName = "int" 
            }
        };

        if (variableList.Contains(variable))
        {
            return;
        }

        variableList.Add(variable);
    }

    public void AddNodeBlock(LocalVariableListManager list, string name)
    {
        IVariableManage variable = new VariableFake 
        {   
            Name = name,
            Type = new MyTypeFake
            {
                EType = Backend.Type.EType.Int,
                TypeName = "int"
            }
        };

        if (viewsManager.GetLocalVariables().Contains(variable))
        {
            return;
        }

        viewsManager.AddVariableToView(variable);
    }


    //Set NodeBlock to Edit section
    public void SetNodeBlockToEdit(ButtonScript button)
    {
        //throw exception
        Debug.Log("what?");
    }

    public void SetNodeBlockToEdit(ReferenceButtonScript button)
    {
        //throw exception
        Debug.Log("what?");
    }
    public void SetNodeBlockToEdit(InputButtonScript button)
    {
        if (currentVariableEditor == null)
        {
            currentVariableEditor = Instantiate(variableEditorPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            currentVariableEditor.GetComponent<VariableEditor>().InstantiateEditor(button.variable);
        }
    }

    public void SetNodeBlockToEdit(VariableButtonScript button, IVariableManage variable)
    {
        if(currentVariableEditor == null)
        {
            currentVariableEditor = Instantiate(variableEditorPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            currentVariableEditor.GetComponent<VariableEditor>().InstantiateEditor(button.variable);
        }
    }

    public void SetNodeBlockToEdit(FunctionButtonScript button)
    {
        nodeBlockEditor.GetComponent<NodeBlockEditor>().SetNodeBlockToEdit(myFunctionList.Find(x => x.Equals(button.function)));
        nodeBlockEditor.SetActive(true);
    }

    public void ChangeView(FunctionButtonScript button)
    {
        viewsManager.ChangeView(button.function);
        localVariableList.GetComponent<LocalVariableListManager>().ReloadVariables();
    }
}
