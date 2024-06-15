using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrust = 1f;
    public float decelerationRate = 0.95f;
    private Rigidbody rb;
    public Transform cameraTransform;

    public float maxFuel = 100f;
    public float currentFuel;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        currentFuel = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && currentFuel > 0)
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

        // Fuel Economy
        currentFuel -= thrust * Time.deltaTime;
        if (currentFuel < 0) currentFuel = 0; 
    }

    void Decelerate() 
    {
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, (1 - decelerationRate) * Time.deltaTime);
    }

    public void Refuel(float amount)
    {
        currentFuel += amount;
        if (currentFuel > maxFuel) currentFuel = maxFuel;
    }

    public Vector3 GetCurrentVelocity() 
    {
        return rb.velocity;
    }
}
