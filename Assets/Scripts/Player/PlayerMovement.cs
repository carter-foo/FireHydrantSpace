using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrust = 10.0f;
    public float decelerationRate = 0.95f;
    private Rigidbody rb;
    public Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ApplyThrust();
        } 
        else
        {
            Decelerate();
        }
    }

    void ApplyThrust()
    {
        Vector3 forceDirection = -cameraTransform.forward; // Thrust Backwards
        rb.AddForce(forceDirection * thrust, ForceMode.Acceleration);
    }

    void Decelerate() 
    {
        rb.velocity *= decelerationRate;
    }
}
