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
        switch (type)
        {
            case "start":
                nodeBlockManager.ChangeViewToStart();
                break;
            case "loop":
                nodeBlockManager.ChangeViewToLoop();
                break;
            case "save":
                nodeBlockManager.SaveState();
                break;
            case "generate":
                nodeBlockManager.GenerateCode();
                break;
            case "constant":
                nodeBlockManager.SpawnNodeBlockConstant();
                break;
        }
    }
}
