using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceJunk : MonoBehaviour
{
    public float speed;
    public Vector3 moveDir;

    private Rigidbody mRigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the rigid body component
        mRigidBody = GetComponent<Rigidbody>();

        // Start moving in the move direction
        mRigidBody.AddForce(moveDir*speed, ForceMode.VelocityChange);

        // Add a small random torque
        Vector3 torque = Random.onUnitSphere*Random.Range(0, 10);
        mRigidBody.AddTorque(torque, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
