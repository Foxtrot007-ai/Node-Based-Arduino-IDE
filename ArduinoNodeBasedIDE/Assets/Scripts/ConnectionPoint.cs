using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ConnectionPoint : MonoBehaviour
{
    public string nodeBlockName;
    public string type;
    public string connectionType;
    public bool interactiveDefinition = false;
    public GameObject typeText;
    public int connectionIndex;
    Vector3[] points = new Vector3[2];
    public string[] availableConnections = {"InPoint|OutPoint","OutPoint|InPoint","PreviousBlockPoint|NextBlockPoint","NextBlockPoint|PreviousBlockPoint"};
    string[] tags = { "InPoint", "OutPoint", "PreviousBlockPoint", "NextBlockPoint" };
    public GameObject connectedPoint;
    Vector2 directionPoint;
    public bool holding = false;
    public GameObject line;
    public bool showLine;
    // Start is called before the first frame update
    void Start()
    {
        showLine = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != points[0])
        {
            points[0] = transform.position;
            DrawLine();
        }

        if(connectedPoint != null)
        {
            if (connectedPoint.transform.position != points[1])
            {
                points[1] = connectedPoint.transform.position;
                DrawLine();
            }
        }
        else
        {
            if (!holding)
            {
                ClearLine();
                showLine = false;
            }
        }

    }

    public void SetType(string type)
    {
        this.type = type;
        if(connectionType == "InPoint" || connectionType == "OutPoint")
        {
            typeText.GetComponent<TMP_Text>().text = type;
        }
        
    }

    private void ClearLine()
    {
        Vector3[] zeroes = new Vector3[2];
        line.GetComponent<LineRenderer>().SetPositions(zeroes);
    }

    private void DrawLine()
    {
        if (showLine)
        {
            line.GetComponent<LineRenderer>().SetPositions(points);
        }
    }
    private void OnMouseOver()
    {
        Debug.Log("MouseOver");
        if (Input.GetMouseButtonDown(0) && connectedPoint == null)
        {
            points[0] = transform.position;
            directionPoint = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
            holding = true;
            showLine = true;
            Debug.Log("Clicked");
        }

        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            unconnect();
        }
    }

    private void unconnect()
    {
        if (connectedPoint != null)
        {
            connectedPoint.GetComponent<ConnectionPoint>().connectedPoint = null;
        }

        connectedPoint = null;
        showLine = false;
        ClearLine();
    }

    private void OnMouseDrag()
    {
        if (holding)
        {
            points[1] = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - directionPoint;
            DrawLine();
        }

    }
    private bool checkColliderTag(string tag)
    {
        return tags.Contains(tag);
    }
    private bool checkConnectable(string otherConnectionType, string otherType)
    {
        string myConnection = connectionType + "|" + otherConnectionType;
        return availableConnections.Contains(myConnection) && type == otherType;
    }

    public void changeType(string newType)
    {
        if(interactiveDefinition == false)
        {
            typeText.GetComponent<TMP_Text>().text = name;
            if(connectedPoint.GetComponent<ConnectionPoint>().type != newType)
            {
                unconnect();
            }
        }
    }

    public void changeDefinition()
    {
        GameObject
            .FindGameObjectWithTag("NodeBlocksManager")
            .GetComponent<NodeBlockManager>()
            .updateTypes(connectionIndex, 
                         nodeBlockName,
                         typeText.GetComponent<TMP_InputField>().text,
                         NodeBlockTypes.Function);
    }


    void OnMouseUp()
    {
        if(connectedPoint != null)
        {
            holding = false;
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null 
            && checkColliderTag(hit.transform.tag)
            && checkConnectable(hit.collider.gameObject.GetComponent<ConnectionPoint>().connectionType,
                                hit.collider.gameObject.GetComponent<ConnectionPoint>().type)
            && hit.collider.gameObject.GetComponent<ConnectionPoint>().connectedPoint == null)
        {
            connectedPoint = hit.collider.gameObject;
            hit.collider.gameObject.GetComponent<ConnectionPoint>().connectedPoint = gameObject;
        }
        else
        {
            ClearLine();
            showLine = false;
        }
        
        holding = false;
    }
}
