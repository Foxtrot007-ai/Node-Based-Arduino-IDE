using Backend.API;
using UnityEngine;
using TMPro;



//Class adding new functionality for user input showing
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
