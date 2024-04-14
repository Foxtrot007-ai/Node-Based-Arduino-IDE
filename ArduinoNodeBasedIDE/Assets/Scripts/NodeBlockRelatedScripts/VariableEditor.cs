using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VariableEditor : MonoBehaviour
{
    public NodeBlock currentNodeBlock;

    public GameObject nodeBlockName;
    public GameObject typeField;

    public void InstantiateEditor(NodeBlock variable)
    {
        currentNodeBlock = variable;
        nodeBlockName.GetComponentInChildren<TMP_InputField>().text = variable.GetName();
        typeField.GetComponentInChildren<TMP_InputField>().text = variable.GetOutputType();
    }

    public void CheckForNewName()
    {
        String inputName = nodeBlockName.GetComponentInChildren<TMP_InputField>().text;
        if (inputName != currentNodeBlock.GetName())
        {
            currentNodeBlock.SetName(inputName);
        }
    }
    public void CheckForNewType()
    {
        String inputType = typeField.GetComponentInChildren<TMP_InputField>().text;
        if (inputType != currentNodeBlock.GetOutputType())
        {
            currentNodeBlock.SetName(inputType);
        }
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
