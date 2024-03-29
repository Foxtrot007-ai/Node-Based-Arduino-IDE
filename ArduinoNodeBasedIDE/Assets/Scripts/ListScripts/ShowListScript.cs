using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowListScript : MonoBehaviour
{
    public GameObject referenceListPrefab;
    public GameObject variableListPrefab;
    public GameObject functionListPrefab;
    public NodeBlockManager nodeBlockManager;
    public Vector3 clickPoint;

    public void Start()
    {
        nodeBlockManager = GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>();
    }

    private void setListActive(GameObject list)
    {
        clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        nodeBlockManager.nodeBlockSpawnPoint = clickPoint;
        list.SetActive(true);
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            setListActive(referenceListPrefab);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            setListActive(variableListPrefab);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            setListActive(functionListPrefab);
        }
    }
}
