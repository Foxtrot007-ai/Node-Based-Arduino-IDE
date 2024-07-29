using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class buttonStartLoopSwitchScript : MonoBehaviour
{
    NodeBlockManager nodeBlockManager;
    public string type = "start";

    public void Start()
    {
        
        nodeBlockManager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
    }
    public void OnClick()
    {
        if(type == "start")
        {
            nodeBlockManager.ChangeViewToStart();
        }
        else if(type == "loop")
        {
            nodeBlockManager.ChangeViewToLoop();
        }
        else if (type == "save")
        {
            nodeBlockManager.SaveState();
        }
        else if (type == "generate")
        {
            nodeBlockManager.GenerateCode();
        }
    }
}
