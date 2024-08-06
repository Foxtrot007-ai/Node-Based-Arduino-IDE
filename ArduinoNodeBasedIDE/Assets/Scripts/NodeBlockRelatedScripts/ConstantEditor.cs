using System;
using Backend.API;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Backend.API.DTO;
using Backend.Type;
using Codice.CM.Client.Differences;
using Backend.Variables;

public class ConstantEditor : MonoBehaviour
{
    public IInputNode node;

    public GameObject valueField;

    public GameObject typeField;
    private DropdownTypesScript dropdownType;
    void Awake()
    {
        dropdownType = typeField.GetComponentInChildren<DropdownTypesScript>();
    }

    public void InstantiateEditor(INode node)
    {
        this.node = (IInputNode) node;
        valueField.GetComponentInChildren<TMP_InputField>().text = this.node.Value;
    }
    private InputNodeValueDto MakeInputNodeDto(string value, string type)
    {

        IType etype = Backend.Type.TypeConverter.ToIType(type);


        InputNodeValueDto dto = new()
        {
            Type = etype,
            Value = value
        };

        return dto;
    }
    public void UpdateVariable()
    {
        string inputValue = valueField.GetComponentInChildren<TMP_InputField>().text;
        string inputType = dropdownType.option;
        try
        {
            node.SetValue(MakeInputNodeDto(inputValue, inputType));

        }
        catch
        {
            Debug.Log("invalid data");
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
