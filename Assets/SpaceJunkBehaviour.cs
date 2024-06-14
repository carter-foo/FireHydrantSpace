using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceJunkBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(10, 0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-0.1f, 0, 0);
    }
}
