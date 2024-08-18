using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Backend.API;


//function button objets for Function List content
public class FunctionButtonScript : ButtonScript
{
    //Additional Child Class Attributes
    public GameObject spawnButton;
    public GameObject deleteButton;
    public GameObject editButton;
    public GameObject changeButton;


    //Class Methods
    //Function button for setup and loop shouldn't have functionality
    public override void SetNodeBlock(IUserFunction function)
    {
        this.function = function;
        text.GetComponent<TMP_Text>().text = function.Name;
        if(function.Name == "setup" || function.Name == "loop")
        {
            spawnButton.GetComponent<Button>().interactable = false;
            deleteButton.GetComponent<Button>().interactable = false;
            editButton.GetComponent<Button>().interactable = false;
        }
    }

    //functions for UI button
    public override void SpawnNodeBlock()
    {
        nodeBlockManager.SpawnNodeBlock(this);
    }
    public override void DeleteNodeBlock()
    {
        nodeBlockManager.DeleteNodeBlock(this);
    }
    public override void EditMyNodeBlock()
    {
        nodeBlockManager.SetNodeBlockToEdit(this);
    }

    //Additional method for UI button to change current view
    public void ChangeView()
    {
        nodeBlockManager.ChangeView(this);
    }
}
