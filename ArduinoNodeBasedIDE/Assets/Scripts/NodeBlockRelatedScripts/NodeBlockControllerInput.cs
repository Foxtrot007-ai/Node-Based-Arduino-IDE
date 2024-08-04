using Backend.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Backend.Node;

public class NodeBlockControllerInput : NodeBlockController
{
    public GameObject inputField;
    public override void Validation()
    {
        CheckForNameChange();
        ValidateInputChange();
    }

    public void ValidateInputChange()
    {
        if(inputField.GetComponent<TMP_Text>().text != ((IInputNode)nodeBlock).Value)
        {
            inputField.GetComponent<TMP_Text>().text = ((IInputNode)nodeBlock).Value;
        }
    }

    public void EditMe()
    {
        GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>().SetConstantToEdit(this.nodeBlock);
    }

}
