using System;
using Backend.API;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Backend.API.DTO;
using Backend.Type;

public class ConstantEditor : MonoBehaviour
{
    public INode node;

    public GameObject valueField;

    public GameObject typeField;
    private DropdownTypesScript dropdownType;

    void Awake()
    {
        dropdownType = typeField.GetComponentInChildren<DropdownTypesScript>();
    }

    public void InstantiateEditor(INode node)
    {
        this.node = node;
        valueField.GetComponentInChildren<TMP_InputField>().text = node.NodeName;
        dropdownType.option = node.OutputsList[0].IOType.ToString();
    }

    private VariableManageDto MakeVariableDto(string name, string type)
    {
       
        IType etype = Backend.Type.TypeConverter.ToIType(type);


        VariableManageDto dto = new ()
        {
            Type = etype,
            VariableName = name
        };

        return dto;
    }
    public void UpdateVariable()
    {
        string inputName = valueField.GetComponentInChildren<TMP_InputField>().text;
        string inputType = dropdownType.option;
        if (inputName != node.NodeName || inputType != node.OutputsList[0].IOType.ToString())
        {
            //node.Change(MakeVariableDto(inputName, inputType));
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
