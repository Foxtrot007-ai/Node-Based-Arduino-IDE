using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public string nametext; 
    public void SetName(string name)
    {
        nametext = name;
        GetComponentInChildren<TMP_Text>().text = name;
    }
    public void CreateNode()
    {
        GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>().CreateNodeBlock(nametext);
        GameObject.FindGameObjectWithTag("SearchBar").SetActive(false);
    }
}
