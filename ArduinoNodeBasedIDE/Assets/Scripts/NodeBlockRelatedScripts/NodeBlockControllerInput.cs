using Backend.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBlockControllerInput : NodeBlockController
{
    public GameObject inputField;
    public override void Validation()
    {
        base.Validation();
        ValidateInputChange();
    }

    public void ValidateInputChange()
    {
        /* 
         if(inputField.GetComponent<TMP_Text>().text != ((i_inputNode) nodeBlock).Value)
         {
            (i_inputNode)  nodeBlock).Value = inputField.GetComponent<TMP_Text>().text;
         }
         */
    }

    public void EditMe()
    {
        GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>().SetConstantToEdit(this.nodeBlock);
    }

}
