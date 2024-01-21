using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject text;
    public string nametext;
    public string mode;

    public void SetMode(string mode)
    {
        this.mode = mode;
    }
    public void SetName(string name)
    {
        nametext = name;
        text.GetComponent<TMP_Text>().text = name;
    }
    public void CreateNode()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("NodeBlocksManager");
        if (mode == "nodeblock")
        {
            manager.GetComponent<NodeBlockManager>().SpawnNodeBlock(nametext);
        }
        else if (mode == "variable")
        {
            manager.GetComponent<NodeBlockManager>().SpawnVariableNodeBlock(nametext);
        }
        else if (mode == "view")
        {
            manager.GetComponent<NodeBlockManager>().ChangeView(nametext);
        } 
        else
        {
            Debug.Log(mode);
        }    
    }
    public void DeleteNode()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("NodeBlocksManager");
        if (mode == "nodeblock")
        {
            Debug.Log(mode);
        }
        else if (mode == "variable")
        {
            manager.GetComponent<NodeBlockManager>().DeleteVariable(nametext);
            GameObject.FindGameObjectWithTag("VariableList").GetComponent<VariableListManager>().UpdateContent();

        }
        else if (mode == "view")
        {
            manager.GetComponent<NodeBlockManager>().DeleteView(nametext);
            GameObject.FindGameObjectWithTag("FunctionList").GetComponent<FunctionListManager>().UpdateContent();
        }
        else
        {
            Debug.Log(mode);
        }

    }

    public void EditMyNodeBlock()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("NodeBlocksManager");
        if (mode == "nodeblock")
        {
            Debug.Log(mode);
        }
        else if (mode == "variable")
        {
            manager.GetComponent<NodeBlockManager>().SetNodeBlockToEdit(nametext, NodeBlockTypes.Variable);
        }
        else if (mode == "view")
        {
            manager.GetComponent<NodeBlockManager>().SetNodeBlockToEdit(nametext, NodeBlockTypes.Function);
        }
        else
        {
            Debug.Log(mode);
        }
    }

}
