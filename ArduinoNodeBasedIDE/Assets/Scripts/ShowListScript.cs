using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowListScript : MonoBehaviour
{
    public GameObject searchBarPrefab;
    public GameObject currentSearchBar;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(2))
        {
            if(currentSearchBar != null)
            {
                Destroy(currentSearchBar);
            }

            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorPosition.z = 0;
            currentSearchBar = Instantiate(searchBarPrefab, cursorPosition, Quaternion.identity);
            
        }
    }
}
