using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public string nametext;
    public string mode;

    public void SetMode(string mode)
    {
        this.mode = mode;
    }
    public void SetName(string name)
    {
        nametext = name;
        GetComponentInChildren<TMP_Text>().text = name;
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
        else
        {
            Debug.Log(mode);
        }
         
    }
}
