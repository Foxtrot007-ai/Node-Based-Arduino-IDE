using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Backend.API;
using Backend.API.DTO;


public class NodeBlockEditor : MonoBehaviour
{
    public IFunctionManage currentNodeBlock;

    public GameObject nodeBlockName;

    public GameObject inputButtonPrefab;

    public GameObject outputTypeField;
    private DropdownTypesScript dropdownType;


    public bool instantiated = false;
    public GameObject listContainer;
    public List<GameObject> inputObjects;

    public NodeBlockManager nodeBlockManager;

    void Awake()
    {
        dropdownType = outputTypeField.GetComponentInChildren<DropdownTypesScript>();
        nodeBlockManager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
        instantiated = false;
    }
    public void UpdateFunction()
    {
        if (instantiated)
        {
            CheckForNewName();
            CheckForNewOutputType();
            CheckForNewInputs();
        }
    }
    public void SetNodeBlockToEdit(IFunctionManage nodeBlock)
    {
        instantiated = true;
        currentNodeBlock = nodeBlock;
        nodeBlockName.GetComponentInChildren<TMP_InputField>().text = currentNodeBlock.Name;
        dropdownType.option = currentNodeBlock.OutputType.TypeName;
        InstantiateInputs();
    }

    public void CheckForNewName()
    {
        String inputName = nodeBlockName.GetComponentInChildren<TMP_InputField>().text;
        if (inputName != currentNodeBlock.Name)
        {
            FunctionManageDto dto = new FunctionManageDto
            {
                FunctionName = inputName,
                OutputType = currentNodeBlock.OutputType
            };
            currentNodeBlock.Change(dto);
        }
    }

    public void CheckForNewOutputType()
    {
        String outputType = dropdownType.option;
        if (outputType != currentNodeBlock.OutputType.TypeName)
        {
            FunctionManageDto dto = new FunctionManageDto 
            { 
                FunctionName = currentNodeBlock.Name, 
                OutputType = new MyTypeFake 
                {
                    TypeName = outputType, 
                    EType = (Backend.Type.EType)Enum.Parse(typeof(Backend.Type.EType), outputType)
                }
            };
            currentNodeBlock.Change(dto);
        }
    }
    
    private List<IVariableManage> GetAllInputVariables()
    {
        List<IVariableManage> inputs = new List<IVariableManage>();
        foreach(GameObject input in inputObjects)
        {
            inputs.Add(input.GetComponent<ButtonScript>().variable);
        }
        return inputs;
    }

    private void CheckForNewInputs()
    {
        //to do
    }

    public void InstantiateInputs() 
    {
        foreach(IVariableManage variable in currentNodeBlock.InputList.VariableManages)
        {
            inputObjects.Add(CreateButton(variable));
        }
    }

    public void CleanInput(GameObject inputButton)
    {
        inputObjects.Remove(inputButton);
        GameObject.Destroy(inputButton);
    }
    
    public void AddInput(string name, string type)
    {
        IVariableManage variable = new VariableFake { Name = name, Type = new MyTypeFake { TypeName = type } };
        currentNodeBlock.InputList.AddVariable(variable);
        inputObjects.Add(CreateButton(variable));
    }

    protected GameObject CreateButton(IVariableManage variable)
    {
        GameObject newContent = Instantiate(inputButtonPrefab);
        newContent.transform.SetParent(listContainer.transform);
        newContent.GetComponentInChildren<TMP_InputField>().text = variable.ToString();
        newContent.transform.localScale = Vector3.one;
        newContent.GetComponent<ButtonScript>().variable = variable;
        return newContent;
    }
}
