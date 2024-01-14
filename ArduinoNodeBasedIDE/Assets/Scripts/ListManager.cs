using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ListManager : MonoBehaviour
{

    public string lastInput = "";
    public GameObject inputField;
    public GameObject listContainer;
    public GameObject buttonContent;
    public List<GameObject> contentObjects;
    void Start()
    {
        lastInput = "";
        AddContent();
    }
    void Update()
    {
        string readInput = inputField.GetComponent<TMP_InputField>().text;

        if(readInput != lastInput)
        {
            lastInput = readInput;
            DestroyContent();
            AddContent();
        }
    }
    private void AddContent()
    {
        List<string> namesList = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>().getLanguageReferenceNames();
        
        foreach(string s in namesList)
        {
            if (s.Contains(lastInput))
            {
                GameObject newContent = Instantiate(buttonContent);
                newContent.transform.SetParent(listContainer.transform);
                newContent.GetComponent<ButtonScript>().SetName(s);
                newContent.GetComponent<ButtonScript>().SetMode("nodeblock");
                contentObjects.Add(newContent);
                newContent.transform.localScale = Vector3.one;
            }
        }
    }

    private void DestroyContent()
    {
        foreach(var content in contentObjects)
        {
            Destroy(content);
        }

        contentObjects.Clear();
    }
}
