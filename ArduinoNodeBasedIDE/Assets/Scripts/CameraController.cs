using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //zoom variables
    public float zoom;
    public float zoomValue;
    public float minZoom = 15f;
    public float maxZoom = 100f;
    public float velosity = 0f;
    public float smoothTime = 0.25f;

    //movement variables
    private Vector3 directionPoint;


    private Camera cameraComponent;
    void Awake()
    {
        cameraComponent = GetComponent<Camera>();
    }
    void Start()
    {
        zoom = cameraComponent.orthographicSize;
    }
    void Update()
    {
        zoomManagement();
        moveManagement();
    }

    private void zoomManagement()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomValue;
        if (zoom < minZoom) zoom = minZoom;
        if (zoom > maxZoom) zoom = maxZoom;
        cameraComponent.orthographicSize = Mathf.SmoothDamp(cameraComponent.orthographicSize, zoom, ref velosity, smoothTime);
    }

    private void moveManagement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            directionPoint = cameraComponent.ScreenToWorldPoint(Input.mousePosition);
        }
            

        if (Input.GetMouseButton(1))
        {
            Vector3 moveVector = directionPoint - cameraComponent.ScreenToWorldPoint(Input.mousePosition);

            cameraComponent.transform.position += moveVector;
        }
    }
}
