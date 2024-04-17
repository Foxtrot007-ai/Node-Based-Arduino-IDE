using System;
using Backend.API;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VariableEditor : MonoBehaviour
{
    public IVariableManage variable;

    public GameObject variableName;
    public GameObject typeField;

    public void InstantiateEditor(IVariableManage variable)
    {
        this.variable = variable;
        variableName.GetComponentInChildren<TMP_InputField>().text = variable.Name;
        typeField.GetComponentInChildren<TMP_InputField>().text = variable.Type.ToString();
    }

    public void CheckForNewName()
    {
        String inputName = variableName.GetComponentInChildren<TMP_InputField>().text;
        /*if (inputName != variable.Name())
        {
            variable.Name(inputName);
        }*/
    }
    public void CheckForNewType()
    {
        String inputType = typeField.GetComponentInChildren<TMP_InputField>().text;
        /*if (inputType != variable.GetOutputType())
        {
            variable.SetName(inputType);
        }*/
    }
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
    public void SaveChanges()
    {
        CheckForNewName();
        CheckForNewType();
        DestroyMe();
    }
}
