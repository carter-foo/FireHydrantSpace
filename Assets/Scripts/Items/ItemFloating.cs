using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloating : MonoBehaviour
{
    public Vector3 axis = Vector3.right;
    public float floatSpeed = 0.2f;
    public float floatHeight = 0.1f;
    private Vector3 startPos;

    public float rotateSpeed = 90f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // // Floating effect
        // float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        // transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotating effect
        transform.Rotate(axis * rotateSpeed * Time.deltaTime);
    }
}
