using Backend.API;
using TMPro;
using UnityEngine;
using Backend.API.DTO;
using Backend.Type;


//Variable editor class that reading user inputs and updating variable definition by backend interfaces
public class VariableEditor : MonoBehaviour
{
    public IVariable variable;

    public GameObject variableNameField;

    public GameObject typeField;
    private DropdownTypesScript dropdownType;

    void Awake()
    {
        dropdownType = typeField.GetComponentInChildren<DropdownTypesScript>();
    }

    public void InstantiateEditor(IVariable variable)
    {
        this.variable = variable;
        variableNameField.GetComponentInChildren<TMP_InputField>().text = variable.Name;
        string type = variable.Type.TypeName;
        dropdownType.option = type[0].ToString().ToUpper() + type.Substring(1);
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

    //Changing type and variable name by dto then deactivating variable editor
    public void SaveChanges()
    {
        UpdateVariable();
        DestroyMe();
    }
}
