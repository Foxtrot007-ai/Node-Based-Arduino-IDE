using UnityEngine;

//Find all dropdown object on scene and update their content (adding new class type situation)
public class DropdownTypesAlerter
{
    public void TypeChangeAlert()
    {
        foreach(GameObject dropdownObject in GameObject.FindGameObjectsWithTag("DropdownType"))
        {
            dropdownObject.GetComponent<DropdownTypesScript>().UpdateDropdownWithData();
        }
    }
}
