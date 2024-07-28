using Backend.Type;
using Backend.Validator;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownTypesScript : MonoBehaviour
{
    private TMP_Dropdown dropdown; 
    public string option { get { return dropdown.options[dropdown.value].text; } set { SetDropdownOption(value); } }
    public void Awake()
    {
        dropdown = GetComponentInChildren<TMP_Dropdown>();
        UpdateDropdownWithData();
    }
    public List<string> TypeList()
    {
        List<string> types = new List<string>();
        types.AddRange(ClassTypeValidator.Instance.GetAllClassTypes());
        types.AddRange(Enum.GetNames(typeof(EType)));
        return types;
    }
    public void UpdateDropdownWithData()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(TypeList());
    }

    private void SetDropdownOption(string type)
    {
        dropdown.value = dropdown.options.FindIndex(option => option.text == type);
    }
}
