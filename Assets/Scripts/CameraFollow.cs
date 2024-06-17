using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Ship").transform;
        transform.position = target.transform.position + Vector3.right * 20.0f;
    }

    void Update() {
        transform.LookAt(target);
    }
}
