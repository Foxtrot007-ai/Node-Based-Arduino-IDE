using UnityEngine;

public class buttonStartLoopSwitchScript : MonoBehaviour
{
    NodeBlockManager nodeBlockManager;
    public string type = "setup";

    public void Start()
    {
        nodeBlockManager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
    }

    //Deciding which functionality use 
    public void OnClick()
    {
        switch (type)
        {
            case "setup":
                nodeBlockManager.ChangeViewToSetup();
                break;
            case "loop":
                nodeBlockManager.ChangeViewToLoop();
                break;
            case "save":
                nodeBlockManager.SaveState();
                break;
            case "load":
                nodeBlockManager.LoadState();
                break;
            case "generate":
                nodeBlockManager.GenerateCode();
                break;
            case "exit":
                Application.Quit();
                break;
        }
    }
}
