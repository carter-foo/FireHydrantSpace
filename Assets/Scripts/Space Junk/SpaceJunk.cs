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
        // // Initialize to some random position
        // this.transform.position = new Vector3(10, 0, 5);
        
        // Get the rigid body component
        mRigidBody = GetComponent<Rigidbody>();

        // Start moving in the move direction
        mRigidBody.AddForce(moveDir*speed, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
