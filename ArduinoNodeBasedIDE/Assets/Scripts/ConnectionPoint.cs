using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ConnectionPoint : MonoBehaviour
{
    public string type;
    Vector3[] points = new Vector3[2];
    public string[] availableConnections = {"InPoint|OutPoint","OutPoint|InPoint","PreviousBlockPoint|NextBlockPoint","NextBlockPoint|PreviousBlockPoint"};
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
        if (Input.GetMouseButtonDown(0))
        {
            points[0] = transform.position;
            directionPoint = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
            holding = true;
            showLine = true;
            Debug.Log("Clicked");
        }

        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            if(connectedPoint != null)
            {
                connectedPoint.GetComponent<ConnectionPoint>().connectedPoint = null;
            }

            connectedPoint = null;
            showLine = false;
            ClearLine();
        }
    }
    private void OnMouseDrag()
    {
        if (holding)
        {
            points[1] = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - directionPoint;
            DrawLine();
        }

    }

    private bool checkConnectable(string otherTag)
    {
        string myConnection = gameObject.tag + "|" + otherTag;
        return availableConnections.Contains(myConnection);
    }


    void OnMouseUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null 
            && checkConnectable(hit.transform.tag)
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
