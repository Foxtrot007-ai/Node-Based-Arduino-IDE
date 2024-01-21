using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VariableListManager : MonoBehaviour
{
    public GameObject inputField;
    public GameObject listContainer;
    public GameObject buttonContent;
    public List<GameObject> contentObjects;
    void Start()
    {
        AddContent();
    }

    public void CreateNewVariable()
    {
        GameObject.FindGameObjectWithTag("NodeBlocksManager")
                    .GetComponent<NodeBlockManager>()
                        .AddVariableNodeBlock(inputField.GetComponent<TMP_InputField>().text);
        UpdateContent();
    }

    private void AddContent()
    {
        List<string> namesList = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>().getVariablesNames();

        foreach (string s in namesList)
        {
            GameObject newContent = Instantiate(buttonContent);
            newContent.transform.SetParent(listContainer.transform);
            newContent.GetComponent<ButtonScript>().SetName(s);
            newContent.GetComponent<ButtonScript>().SetMode("variable");
            contentObjects.Add(newContent);
            newContent.transform.localScale = Vector3.one;
        }
    }

    private void DestroyContent()
    {
        foreach (var content in contentObjects)
        {
            Destroy(content);
        }

        contentObjects.Clear();
    }

    public void UpdateContent()
    {
        DestroyContent();
        AddContent();
    }
}
