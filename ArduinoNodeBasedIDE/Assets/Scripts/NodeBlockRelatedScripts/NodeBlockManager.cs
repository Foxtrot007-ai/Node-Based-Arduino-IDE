using System;
using System.Collections.Generic;
using UnityEngine;
using Backend.API;
using Backend;
using Backend.Type;

public class NodeBlockManager : MonoBehaviour
{
    //backend manager
    public IBackendManager backendManager = new Startup();

    // language Reference objects
    // public LanguageReferenceParser languageReferenceParser = new LanguageReferenceParser();

    // NodeBlock List objects
    public Vector3 nodeBlockSpawnPoint = Vector3.zero;
    public GameObject nodeBlockPrefab;

    //views
    public ViewsManager viewsManager = new ViewsManager();

    //loader

    public SaveStateModule saveManager = new SaveStateModule();

    //Variable List objects
    //public List<IVariablesManager> variableList = new List<IVariablesManager>();

    public GameObject localVariableList;

    public InfoMessageManager messageInfo;
    //my FunctionsList objects
    //public List<IFunction> myFunctionList = new List<IFunction>();

    //NodeBlock Editor
    public GameObject nodeBlockEditor;

    //Variable Editor
    public GameObject currentVariableEditor;

    //Constant Editor
    public GameObject currentConstantEditor;

    // Start is called before the first frame update
    void Awake()
    {
        messageInfo = GameObject.FindGameObjectWithTag("InfoMessageManager").GetComponent<InfoMessageManager>();
        instantiateBasicFunctions();
        saveManager.Instantiate(this, backendManager, viewsManager);
    }

    //Starting functions
    
    public void instantiateBasicFunctions()
    {
        viewsManager.AddNewView(backendManager.Start);
        viewsManager.ChangeView(backendManager.Start);
        NodeBlockController startNodeBlockObject = SpawnNodeBlockWithoutValidation().GetComponent<NodeBlockController>();
        startNodeBlockObject.isStartNodeBlock = true;
        startNodeBlockObject.InstantiateNodeBlockController(backendManager.Start.StartNode);

        viewsManager.AddNewView(backendManager.Loop);
        viewsManager.ChangeView(backendManager.Loop);
        startNodeBlockObject = SpawnNodeBlockWithoutValidation().GetComponent<NodeBlockController>();
        startNodeBlockObject.isStartNodeBlock = true;
        startNodeBlockObject.InstantiateNodeBlockController(backendManager.Loop.StartNode);

        
    }

    //SearchNodeBlock Section
    public List<IUserFunction> SearchNodeBlocks(List<IUserFunction> list, String nodeBlockName)
    {
        List<IUserFunction> temp = new List<IUserFunction>();
        foreach (IUserFunction node in list)
        {
            if (node.Name.Contains(nodeBlockName)) {
                temp.Add(node);
            }
        }
      
        return temp;
    }

    public List<IVariable> SearchNodeBlocks(List<IVariable> list, String nodeBlockName)
    {
        List<IVariable> temp = new List<IVariable>();
        foreach (IVariable node in list)
        {
            if (node.Name.Contains(nodeBlockName))
            {
                temp.Add(node);
            }
        }
        return temp;
    }

    public List<ITemplate> SearchNodeBlocks(List<ITemplate> list, String nodeBlockName)
    {
        List<ITemplate> temp = new List<ITemplate>();
        foreach (ITemplate node in list)
        {
            if (node.Name.Contains(nodeBlockName))
            {
                temp.Add(node);
            }
        }
        return temp;
    }

    public List<IUserFunction> SearchNodeBlocks(ListManager manager, String nodeBlockName)
    {
        //throw exception
        return null;
    }
   
    public List<IVariable> SearchNodeBlocks(GlobalVariableListManager manager, String nodeBlockName)
    {
        return SearchNodeBlocks(backendManager.GlobalVariables.Variables, nodeBlockName);
    }

    public List<IVariable> SearchNodeBlocks(LocalVariableListManager manager, String nodeBlockName)
    {
        return SearchNodeBlocks(viewsManager.GetLocalVariables(), nodeBlockName);
    }

    public List<IUserFunction> SearchNodeBlocks(FunctionListManager manager, String nodeBlockName)
    {
        return SearchNodeBlocks(backendManager.Functions.Functions, nodeBlockName);
    }

    public List<ITemplate> SearchNodeBlocks(ReferenceListManager manager, String nodeBlockName)
    {
        return SearchNodeBlocks(backendManager.Templates.Templates, nodeBlockName);
    }

    //SpawnNodeBlock section
 
    public GameObject SpawnNodeBlockWithoutValidation()
    {
        nodeBlockSpawnPoint.z = 0;
        GameObject nodeBlockObject = Instantiate(nodeBlockPrefab, nodeBlockSpawnPoint, Quaternion.identity);
        viewsManager.AddToView(nodeBlockObject);
        return nodeBlockObject;
    }


    public GameObject SpawnNodeBlock(INode node)
    {
        GameObject nodeBlockObject = SpawnNodeBlockWithoutValidation();
        nodeBlockObject.GetComponent<NodeBlockController>().InstantiateNodeBlockController(node);
        return nodeBlockObject;
    }

    public void SpawnNodeBlock(ButtonScript button)
    {
        //throw exception
        Debug.Log("what?");
    }
    public void SpawnNodeBlock(ReferenceButtonScript button)
    {
        SpawnNodeBlock(button.template.CreateNodeInstance());
    }
    public void SpawnNodeBlock(VariableButtonScript button)
    {
        SpawnNodeBlock(button.variable.CreateGetNode());
    }
    public void SpawnSetNodeBlock(VariableButtonScript button)
    {
        //setter
        SpawnNodeBlock(button.variable.CreateSetNode());
    }
    public void SpawnNodeBlock(FunctionButtonScript button)
    {
        SpawnNodeBlock(button.function.CreateNode());
    }
    public void SpawnNodeBlock(InputButtonScript button)
    {
        //throw exception
        Debug.Log("what?");
    }

    //DeleteNodeBlock Section
    public void DeleteNodeBlock(ButtonScript button)
    {
        //throw exception
        Debug.Log("what?");
    }
    public void DeleteNodeBlock(VariableButtonScript button)
    {
        button.variable.Delete();
        button.GetComponentInParent<FunctionListManager>().UpdateContent();
    }
    public void DeleteNodeBlock(InputButtonScript button)
    {
        //throw exception
        Debug.Log("what?");
    }

    public void DeleteNodeBlock(FunctionButtonScript button)
    {
        button.function.Delete();
        Debug.Log(button.function.IsDelete);
        button.GetComponentInParent<FunctionListManager>().UpdateContent();
    }

    public void DeleteNodeBlock(ReferenceButtonScript button)
    {
        //throw exception
        Debug.Log("what?");
    }

    //AddNodeBlock Section

    public void AddNodeBlock(string name, int numberOfInput, int numberOfOutput)
    {
        IUserFunction newFuntion = backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
        {
            FunctionName = name,
            OutputType = new VoidType()
        });

        viewsManager.AddNewView(newFuntion);
        viewsManager.ChangeView(newFuntion);

        //making start node

        NodeBlockController startNodeBlockObject = SpawnNodeBlockWithoutValidation().GetComponent<NodeBlockController>();
        startNodeBlockObject.isStartNodeBlock = true;
        startNodeBlockObject.InstantiateNodeBlockController(newFuntion.StartNode);
    }
    public void AddNodeBlock(GlobalVariableListManager list, string name)
    {
        IVariable newVariable = backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
        {
            VariableName = name,
            Type = new Backend.Type.PrimitiveType(EType.Int)
        });
    }

    public void AddNodeBlock(LocalVariableListManager list, string name)
    {
        IVariable newVariable = viewsManager.actualView.Variables.AddVariable(new Backend.API.DTO.VariableManageDto
        {
            VariableName = name,
            Type = new Backend.Type.PrimitiveType(EType.Int)
        });
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
        if (!currentVariableEditor.activeSelf)
        {
            currentVariableEditor.SetActive(true);
            currentVariableEditor.GetComponent<VariableEditor>().InstantiateEditor(button.variable);    
        }
    }

    public void SetNodeBlockToEdit(VariableButtonScript button)
    {
        if (!currentVariableEditor.activeSelf)
        {
            currentVariableEditor.SetActive(true);
            currentVariableEditor.GetComponent<VariableEditor>().InstantiateEditor(button.variable);
        }
    }

    public void SetConstantToEdit(INode node)
    {
        if (!currentConstantEditor.activeSelf)
        {
            currentConstantEditor.SetActive(true);
            currentConstantEditor.GetComponent<ConstantEditor>().InstantiateEditor(node);
        }
    }

    public void SetNodeBlockToEdit(FunctionButtonScript button)
    {
        nodeBlockEditor.GetComponent<NodeBlockEditor>().SetNodeBlockToEdit(button.function);
        nodeBlockEditor.SetActive(true);
    }

    public void ChangeView(FunctionButtonScript button)
    {
        viewsManager.ChangeView(button.function);
        localVariableList.GetComponent<LocalVariableListManager>().ReloadVariables();
    }

    public void ChangeViewToStart()
    {
        viewsManager.ChangeView(backendManager.Start);
        localVariableList.GetComponent<LocalVariableListManager>().ReloadVariables();
    }

    public void ChangeViewToLoop()
    {
        viewsManager.ChangeView(backendManager.Loop);
        localVariableList.GetComponent<LocalVariableListManager>().ReloadVariables();
    }

    public void SaveState()
    {
       // backendManager.Save("Assets/Resources/enviroment.json");
       // saveManager.Save();
        messageInfo.addMessage("Code saved succesfully", 0.3f);
    }

    public void LoadState()
    {
       // backendManager.Load("Assets/Resources/enviroment.json");
       // saveManager.Load();
        messageInfo.addMessage("Code saved succesfully", 0.3f);
    }

    public void GenerateCode()
    {
        messageInfo.addMessage("Code generate succesfully", 0.3f);
    }

    public void SpawnNodeBlockConstant()
    {
        //SpawnNodeBlock(backendManager.GetConstantINode());   
    }
}
