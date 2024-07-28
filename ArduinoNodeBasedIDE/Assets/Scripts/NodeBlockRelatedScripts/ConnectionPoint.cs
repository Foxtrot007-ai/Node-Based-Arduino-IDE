using Backend.API;
using TMPro;
using UnityEngine;

public class ConnectionPoint : MonoBehaviour
{
    public NodeBlockController nodeBlockController;
    public NodeBlockManager nodeBlockManager;

    public IConnection connection;

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
        if (typeText.GetComponent<TMP_Text>().text != connection.IOName)
        {
            typeText.GetComponent<TMP_Text>().text = connection.IOName;
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
            if (connection.Connected.UIPoint.transform.position != points[1])
            {
                points[1] = connection.Connected.UIPoint.transform.position;
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
        typeText.GetComponent<TMP_Text>().text = con.IOName;
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
        connection.Connected.Disconnect();
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


    private void OnMouseUp()
    {
        if(connection.Connected != null)
        {
            holding = false;
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null
            && CompareTag(hit.collider.tag))
        {
            connection.Connect(hit.collider.gameObject.GetComponent<ConnectionPoint>().connection);
        }
        else
        {
            ClearLine();
            showLine = false;
        }
        
        holding = false;
    }
}
