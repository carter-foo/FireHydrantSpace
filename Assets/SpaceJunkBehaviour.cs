using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpaceJunkBehaviour : MonoBehaviour
{
    private Rigidbody mRigidBody;
    private float speed = 100f;
    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize to some random position
        this.transform.position = new Vector3(10, 0, 5);
        
        // Get the rigid body component
        mRigidBody = GetComponent<Rigidbody>();

        // Get the player's current position, then make a direction vector that goes towards it
        Vector3 destPoint = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        moveDir = (destPoint - this.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Move in the direction of the direction vector
        mRigidBody.MovePosition(transform.position + moveDir*speed*Time.deltaTime);
    }
}
