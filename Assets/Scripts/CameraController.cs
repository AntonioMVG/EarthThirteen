using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;

    [Header("Rotation")]
    public float minXRot;
    public float maxXRot;
    public float rotSpeed;

    [Header("Zoom")]
    public float minZoom;
    public float maxZoom;
    public float zoomSpeed;
    
    private Camera cam;
    private float currentXRot;
    private float currentZoom;

    private void Start()
    {
        cam = Camera.main;

        currentZoom = cam.transform.localPosition.y;
        currentXRot = -50;
    }

    private void Update()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        // Getting the scroll wheel value
        currentZoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;

        // Clamping it between the min/max values
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Applying to the camera
        cam.transform.localPosition = Vector3.up * currentZoom;

        // Camera look
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            currentXRot += -y * rotSpeed;
            currentXRot = Mathf.Clamp(currentXRot, minXRot, maxXRot);
            transform.eulerAngles = new Vector3(currentXRot, transform.eulerAngles.y + (x * rotSpeed), 0f);
        }

        // Camera move
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;
        forward.y = 0f;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Get a local direction
        Vector3 dir = forward * moveZ + right * moveX;
        dir.Normalize();
        dir *= moveSpeed * Time.deltaTime;
        transform.position += dir;
    }
}
