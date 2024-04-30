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

    private int newVariableNameIndex = 0;

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
        List<IVariableManage> inputsVariablesFromListContainer = GetAllInputVariables();
        List<IVariableManage> inputsToAdd = inputsVariablesFromListContainer.FindAll(input => !currentNodeBlock.InputList.VariableManages.Contains(input));
        List<IVariableManage> inputsOldInputsToAdd = inputsVariablesFromListContainer.FindAll(input => currentNodeBlock.InputList.VariableManages.Contains(input));
        List<IVariableManage> inputsToDelete = currentNodeBlock.InputList.VariableManages.FindAll(input => !inputsOldInputsToAdd.Contains(input));

        foreach (IVariableManage input in inputsToDelete)
        {
            currentNodeBlock.InputList.DeleteVariable(input);
        }

        foreach (IVariableManage input in inputsToAdd)
        {
            currentNodeBlock.InputList.AddVariable(input);
        }
    }

    public void InstantiateInputs() 
    {
        foreach(GameObject input in inputObjects)
        {
            Destroy(input);
        }

        inputObjects.Clear();

        foreach(IVariableManage variable in currentNodeBlock.InputList.VariableManages)
        {
            inputObjects.Add(CreateButton(variable));
        }
    }
    
    public void AddInput()
    {
        IVariableManage variable = new VariableFake 
        { 
            Name = "var" + ++newVariableNameIndex, 
            Type = new MyTypeFake 
            { 
                TypeName = "Int", 
                EType = Backend.Type.EType.Int 
            } 
        };
        inputObjects.Add(CreateButton(variable));
    }
    
    public void DeleteInput(GameObject input)
    {
        inputObjects.Remove(input);
        Destroy(input);
    }

    protected GameObject CreateButton(IVariableManage variable)
    {
        GameObject newContent = Instantiate(inputButtonPrefab);
        newContent.transform.SetParent(listContainer.transform);
        newContent.transform.localScale = Vector3.one;
        newContent.GetComponent<InputButtonScript>().SetNodeBlock(variable);
        return newContent;
    }
}
