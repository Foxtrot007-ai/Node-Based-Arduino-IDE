using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowListScript : MonoBehaviour
{
    public GameObject searchBarPrefab;
    public Vector3 clickPoint;
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(2))
        {
            clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject.FindGameObjectWithTag("NodeBlocksManager").GetComponent<NodeBlockManager>().nodeBlockSpawnPoint = clickPoint;
            searchBarPrefab.SetActive(true);
        }
    }
}
