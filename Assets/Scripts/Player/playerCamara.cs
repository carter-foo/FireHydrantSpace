using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamara : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse Input
        float mouseX = Input.GetAxisRaw("Mouse X") * 0.016f * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * 0.016f * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        // Limit Vertical View Roaatation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate Camara and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
