using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Camera parameters
    public Vector2 cameraDirection;
    public float cameraSpeed;
    public static CameraController cameraInstance;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        // Camera 
        cameraDirection.x += Input.GetAxis("Mouse X") * cameraSpeed;
        cameraDirection.y += Input.GetAxis("Mouse Y") * cameraSpeed;
        cameraDirection.y = Mathf.Clamp(cameraDirection.y, -90, 90);
        transform.localRotation = Quaternion.Euler(-cameraDirection.y, cameraDirection.x, 0);
    }
}
