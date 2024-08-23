using Backend.API;
using TMPro;
using UnityEngine;
using Backend.API.DTO;
using Backend.Type;

//Script for Constant Editor UI 
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
        string type = node.OutputsList[0].IOName;
        dropdownType.option = type[0].ToString().ToUpper() + type.Substring(1);
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

    //Save changes updates type and value then deactivating UI object on scene
    public void SaveChanges()
    {
        UpdateVariable();
        DestroyMe();
    }
}
