using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeBlockEditor : MonoBehaviour
{
    public NodeBlock currentNodeBlock;

    public GameObject nodeBlockName;

    public GameObject inputEditorPrefab;
    public GameObject outputEditorPrefab;

    public GameObject inputStartPoint;
    public GameObject outputStartPoint;

    public Vector3 inputPointIncrease;

    public GameObject[] inputs;

    public GameObject output;

    public NodeBlockManager nodeBlockManager;

    public void Start()
    {
        nodeBlockManager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
    }
    public void SetNodeBlockToEdit(NodeBlock nodeBlock)
    {
        currentNodeBlock = nodeBlock;
        UpdateField();
        CleanEditor();
        InstantiateInputs();
        InstantiateOutput();
    }

    public void UpdateField()
    {
        nodeBlockName.GetComponent<TMP_Text>().text = currentNodeBlock.GetName();
    }

    public void InstantiateInputs() 
    {
        int numberOfInputs = currentNodeBlock.GetNumberOfInputs();

        inputs = new GameObject[numberOfInputs];

        for (int i = 0; i < numberOfInputs; i++)
        {
            inputs[i] = CreatePort(inputEditorPrefab, inputStartPoint.transform.position + i * inputPointIncrease, currentNodeBlock.GetInputType(i));
        }
    }

    public void InstantiateOutput()
    {
        if (currentNodeBlock.returnOutputBlock)
        {
            output = CreatePort(outputEditorPrefab, outputStartPoint.transform.position, currentNodeBlock.GetOutputType());
        }
    }

    public GameObject CreatePort(GameObject prefab, Vector3 point, string type)
    { 
        GameObject temp = Instantiate(prefab, point, Quaternion.identity);
        temp.transform.SetParent(this.transform);
        temp.transform.localScale = Vector3.one;
        temp.GetComponentInChildren<TMP_InputField>().text = type;
        return temp;
    }

    public void CleanEditor()
    {
        foreach(var node in inputs)
        {
            GameObject.Destroy(node);
        }
        
        if(output != null)
        {
            GameObject.Destroy(output);
        }
    }

    public void UpdateNodeBlockConnectionsTypes()
    {
        for (int i = 0; i < currentNodeBlock.GetNumberOfInputs(); i++)
        {
            string newType = inputs[i].GetComponentInChildren<TMP_InputField>().text;
            if (newType != currentNodeBlock.GetInputType(i)) {
                nodeBlockManager.updateInputType(i, newType, currentNodeBlock);
            } 
        }

        if (currentNodeBlock.returnOutputBlock)
        {
            string newType = output.GetComponentInChildren<TMP_InputField>().text;
            if (newType != currentNodeBlock.GetOutputType())
            {
                nodeBlockManager.updateOutputType(newType, currentNodeBlock);
            }
        }
    }
}
