using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
