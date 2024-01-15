using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowListScript : MonoBehaviour
{
    public GameObject searchBarPrefab;
    public GameObject variableListPrefab;
    public GameObject functionListPrefab;
    public Vector3 clickPoint;
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>().nodeBlockSpawnPoint = clickPoint;
            searchBarPrefab.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>().nodeBlockSpawnPoint = clickPoint;
            variableListPrefab.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>().nodeBlockSpawnPoint = clickPoint;
            functionListPrefab.SetActive(true);
        }
    }
}
