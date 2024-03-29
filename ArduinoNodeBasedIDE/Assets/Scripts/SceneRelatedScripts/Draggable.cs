using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    private Vector2 originPoint;
    private Vector2 directionPoint;
    public bool colliding = false;
    public bool holding = false;
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            originPoint = transform.position;
            directionPoint = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
            holding = true;
        }

        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            DestroyMe();
        }
    }
    private void OnMouseDrag()
    {
        if (holding)
        {
            Vector2 moveVector = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - directionPoint;
            transform.position = moveVector;
        }

    }

    private void OnMouseUp()
    {
        if (colliding)
        {
            transform.position = originPoint;
        }

        holding = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NodeBlock")
        {
            colliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NodeBlock")
        {
            colliding = false;
        }
    }
}
