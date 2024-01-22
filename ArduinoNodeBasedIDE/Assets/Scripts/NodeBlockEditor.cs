using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeBlockEditor : MonoBehaviour
{
    public GameObject inputStartPoint;
    public GameObject outputStartPoint;
    public GameObject field;

    public NodeBlock currentNodeBlock;

    public string nbName;
    public GameObject nodeBlockName;

    public GameObject inputEditorPrefab;
    public GameObject outputEditorPrefab;
    public Vector3 inputPointIncrease;
    public int numberOfInputs;
    public GameObject[] inputs;
    public GameObject output;
    public void SetNodeBlockToEdit(NodeBlock nodeBlock)
    {
        nbName = nodeBlock.GetName();
        nodeBlockName.GetComponent<TMP_Text>().text = nbName;

        CleanEditor();

        numberOfInputs = nodeBlock.GetNumberOfInputs();

        inputs = new GameObject[numberOfInputs];

        for(int i = 0; i < numberOfInputs; i++)
        {
            GameObject temp = Instantiate(inputEditorPrefab, inputStartPoint.transform.position + i * inputPointIncrease, Quaternion.identity);
            temp.transform.SetParent(this.transform);
            temp.transform.localScale = Vector3.one;
            temp.GetComponentInChildren<TMP_InputField>().text = nodeBlock.GetInputType(i);
            inputs[i] = temp;
        }


        if (nodeBlock.returnOutputBlock)
        {
            GameObject temp = Instantiate(outputEditorPrefab, outputStartPoint.transform.position, Quaternion.identity);
            temp.transform.SetParent(this.transform);
            temp.transform.localScale = Vector3.one;
            temp.GetComponentInChildren<TMP_InputField>().text = nodeBlock.GetOutputType();
            output = temp;
        }

        currentNodeBlock = nodeBlock;
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
        NodeBlockManager manager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
        for (int i = 0; i < numberOfInputs; i++)
        {
            string newType = inputs[i].GetComponentInChildren<TMP_InputField>().text;
            if (newType != currentNodeBlock.GetInputType(i)) {
                manager.updateInputType(i, nbName, newType, currentNodeBlock.GetNodeBlockType());
            } 
        }


        if (currentNodeBlock.returnOutputBlock)
        {
            string newType = output.GetComponentInChildren<TMP_InputField>().text;
            if (newType != currentNodeBlock.GetOutputType())
            {
                manager.updateOutputType(nbName, newType, currentNodeBlock.GetNodeBlockType());
            }
        }
    }
}
