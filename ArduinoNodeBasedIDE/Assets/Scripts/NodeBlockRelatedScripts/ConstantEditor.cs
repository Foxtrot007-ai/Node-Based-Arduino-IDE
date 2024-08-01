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

    public void InstantiateEditor(INode node)
    {
        this.node = node;
        //valueField.GetComponentInChildren<TMP_InputField>().text = node.GetValue();
    }

    public void UpdateVariable()
    {
        string value = valueField.GetComponentInChildren<TMP_InputField>().text;
        /*if (value != node.GetValue())
        {
            //node.ChangeValue(value);
        }*/
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
