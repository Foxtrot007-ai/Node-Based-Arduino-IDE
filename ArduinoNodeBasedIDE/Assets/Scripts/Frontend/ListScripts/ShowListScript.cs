using UnityEngine;

//Script for key binding to open different UI controls again on scene
public class ShowListScript : MonoBehaviour
{
    //supported UI controls
    public GameObject referenceListPrefab;
    public GameObject globalVariableListPrefab;
    public GameObject functionListPrefab;
    public GameObject localVariableListPrefab;
    public NodeBlockManager nodeBlockManager;
    public Vector3 clickPoint;

    public void Start()
    {
        nodeBlockManager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
    }

    public void setListActive(GameObject list)
    {
        clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        nodeBlockManager.nodeBlockSpawnPoint = clickPoint;
        list.SetActive(true);
    }


    //Keyboard key binding
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            setListActive(referenceListPrefab);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            setListActive(globalVariableListPrefab);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            setListActive(localVariableListPrefab);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            setListActive(functionListPrefab);
        }
    }
}
