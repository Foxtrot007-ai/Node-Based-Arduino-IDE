using System;
using System.Linq;
using TMPro;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class ConnectionPoint : MonoBehaviour
{
    public NodeBlockController nodeBlockController;
    public NodeBlockManager nodeBlockManager;

    public string type;
    public string connectionType;
    
    public int connectionIndex;
  
    public string[] availableConnections = {"InPoint|OutPoint","OutPoint|InPoint","PreviousBlockPoint|NextBlockPoint","NextBlockPoint|PreviousBlockPoint"};
    string[] tags = { "InPoint", "OutPoint", "PreviousBlockPoint", "NextBlockPoint" };

    public GameObject connectedPoint;
    public GameObject typeText;
    public GameObject line;

    Vector3[] points = new Vector3[2];
    Vector2 directionPoint;
    public bool holding = false;
    public bool showLine;

    void Start()
    {
        nodeBlockManager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
        showLine = false;
    }

    private string GetTypeFromController()
    {
        if(connectionType == "InPoint")
        {
            return nodeBlockController.nodeBlock.GetInputType(connectionIndex);
        }
        else if (connectionType == "OutPoint")
        {
            return nodeBlockController.nodeBlock.GetOutputType();
        }
        else
        {
            return "";
        }
    }
    private void UpdateType()
    {
        string currentType = GetTypeFromController();
        if (currentType != type)
        {
            SetType(currentType);

        }
    }

    void Update()
    {
        UpdateType();

        if (transform.position != points[0])
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
    public void InstantiateConnection(NodeBlockController nodeBlockController, string type, int connectionIndex)
    {
        this.nodeBlockController = nodeBlockController;
        SetType(type);
        this.connectionIndex = connectionIndex;
    }


    public void SetType(string type)
    {
        this.type = type;
        if(connectionType == "InPoint" || connectionType == "OutPoint")
        {
            typeText.GetComponent<TMP_Text>().text = type;
            if (connectedPoint != null && connectedPoint.GetComponent<ConnectionPoint>().type != type)
            {
                Unconnect();
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
        if (Input.GetMouseButtonDown(0) && connectedPoint == null)
        {
            points[0] = transform.position;
            directionPoint = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
            holding = true;
            showLine = true;
        }

        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            Unconnect();
        }
    }

    public void Unconnect()
    {
        if (connectedPoint != null)
        {
            connectedPoint.GetComponent<ConnectionPoint>().connectedPoint = null;
        }
        nodeBlockManager.originator.State = new UnconnectAction(this, connectedPoint);
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

    private void OnMouseUp()
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
            nodeBlockManager.originator.State = new ConnectAction(this, connectedPoint);
        }
        else
        {
            ClearLine();
            showLine = false;
        }
        
        holding = false;
    }
}
