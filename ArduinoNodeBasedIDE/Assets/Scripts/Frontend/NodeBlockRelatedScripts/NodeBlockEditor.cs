using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Backend.API;
using Backend.API.DTO;
using UnityEngine.Windows;


//Function Editor script for Function editor UI control
public class NodeBlockEditor : MonoBehaviour
{
    public IUserFunction currentNodeBlock;

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

    //Update function checking changes in definitions
    private void Update()
    {
        if(currentNodeBlock != null)
        {
            CheckForNewName();
            CheckForNewOutputType();
        }
    }
       
    //Public function to set current nodeblock to edit
    public void SetNodeBlockToEdit(IUserFunction nodeBlock)
    {
        instantiated = true;
        currentNodeBlock = nodeBlock;
        nodeBlockName.GetComponentInChildren<TMP_InputField>().text = currentNodeBlock.Name;
        string type = currentNodeBlock.OutputType.TypeName;
        dropdownType.option = type[0].ToString().ToUpper() + type.Substring(1);
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
                OutputType = Backend.Type.TypeConverter.ToIType(outputType)
            };
            currentNodeBlock.Change(dto);
        }
    }

    //managing funtion attributes
    public void InstantiateInputs() 
    {
        foreach(GameObject input in inputObjects)
        {
            Destroy(input);
        }

        inputObjects.Clear();

        foreach(IVariable variable in currentNodeBlock.InputList.Variables)
        {
            inputObjects.Add(CreateButton(variable));
        }
    }
    
    public void AddInput()
    {
        if (currentNodeBlock == null)
        {
            return;
        }

        IVariable newVariable = currentNodeBlock.InputList.AddVariable(new Backend.API.DTO.VariableManageDto
        {
            VariableName = "var" + ++newVariableNameIndex,
            Type = new Backend.Type.PrimitiveType(Backend.Type.EType.Int)
        });
        inputObjects.Add(CreateButton(newVariable));
    }
    
    public void DeleteInput(GameObject input)
    {
        IVariable variableToRemove = input.GetComponent<ButtonScript>().variable;
        currentNodeBlock.InputList.DeleteVariable(variableToRemove);
        inputObjects.Remove(input);
        Destroy(input);
    }

    protected GameObject CreateButton(IVariable variable)
    {
        GameObject newContent = Instantiate(inputButtonPrefab);
        newContent.transform.SetParent(listContainer.transform);
        newContent.transform.localScale = Vector3.one;
        newContent.GetComponent<InputButtonScript>().SetNodeBlock(variable);
        return newContent;
    }
}
