using Backend.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        /* 
         if(inputField.GetComponent<TMP_Text>().text != nodeBlock.GetValue())
         {
            inputField.GetComponent<TMP_Text>().text = nodeBlock.GetValue();
         }
         */
    }

    public void EditMe()
    {
        GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>().SetConstantToEdit(this.nodeBlock);
    }

}
