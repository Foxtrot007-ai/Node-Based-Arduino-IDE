using Backend.API;
using TMPro;
using UnityEngine;

public class ConnectionPoint : MonoBehaviour
{
    public NodeBlockController nodeBlockController;
    public NodeBlockManager nodeBlockManager;

    public IConnection connection;
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

    private void UpdateType()
    {
        if (typeText.GetComponent<TMP_Text>().text != connection.InOutName)
        {
            typeText.GetComponent<TMP_Text>().text = connection.InOutName;
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

        if(connection.Connected != null)
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
    public void InstantiateConnection(IConnection con)
    {
        this.connection = con;
        typeText.GetComponent<TMP_Text>().text = con.InOutName;
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
        if (Input.GetMouseButtonDown(0) && connection.Connected == null)
        {
            points[0] = transform.position;
            directionPoint = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
            holding = true;
            showLine = true;
        }

        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            Disconnect();
        }
    }

    public void Disconnect()
    {
        if (connection.Connected != null)
        {
            connectedPoint.GetComponent<ConnectionPoint>().connectedPoint = null;
        }
        connection.Connected.Disconnect();
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
        return tag.Contains(tag);
    }
  

    private void OnMouseUp()
    {
        if(connection.Connected != null)
        {
            holding = false;
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null 
            && checkColliderTag(hit.transform.tag)
            && hit.collider.gameObject.GetComponent<ConnectionPoint>().connection.Connected == null)
        {
            connection.Connect(hit.collider.gameObject.GetComponent<ConnectionPoint>().connection);
            if (connection.Connected != null)
            {
                connectedPoint = hit.collider.gameObject;
                hit.collider.gameObject.GetComponent<ConnectionPoint>().connectedPoint = gameObject;
            }
        }
        else
        {
            ClearLine();
            showLine = false;
        }
        
        holding = false;
    }
}
