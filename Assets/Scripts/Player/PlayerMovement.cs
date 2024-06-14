using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrust = 10.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ApplyThrust();
        }
    }

    void ApplyThrust()
    {
        Vector3 forceDirection = -transform.forward;
        rb.AddForce(forceDirection * thrust, ForceMode.Acceleration);
    }
}
