using System;
using Backend.API;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Backend.API.DTO;
using Backend.Type;

public class VariableEditor : MonoBehaviour
{
    public IVariableManage variable;

    public GameObject variableNameField;

    public GameObject typeField;
    private DropdownTypesScript dropdownType;

    void Awake()
    {
        dropdownType = typeField.GetComponentInChildren<DropdownTypesScript>();
    }

    public void InstantiateEditor(IVariableManage variable)
    {
        this.variable = variable;
        variableNameField.GetComponentInChildren<TMP_InputField>().text = variable.Name;
        dropdownType.option = variable.Type.TypeName;
    }

    private VariableManageDto MakeVariableDto(string name, string type)
    {
        EType etype = EType.Class;

        try
        {
            etype = (EType)Enum.Parse(typeof(EType), type);
        }
        catch
        {
            Debug.Log("Parse problem, probably class");
        }

        VariableManageDto dto = new VariableManageDto
        {
            Type = new MyTypeFake { EType = etype, TypeName = type },
            VariableName = name
        };

        return dto;
    }
    public void UpdateVariable()
    {
        string inputName = variableNameField.GetComponentInChildren<TMP_InputField>().text;
        string inputType = dropdownType.option;
        if (inputName != variable.Name || inputType != variable.Type.TypeName)
        {
            variable.Change(MakeVariableDto(inputName, inputType));
        }
    }
    public void DestroyMe()
    {
        gameObject.SetActive(false);
    }
    public void SaveChanges()
    {
        UpdateVariable();
        DestroyMe();
    }
}
