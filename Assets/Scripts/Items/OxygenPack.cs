using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenPack : MonoBehaviour
{
    public float refillAmount = 25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        OxygenLevel oxygenLevel = other.GetComponent<OxygenLevel>();
        if (oxygenLevel != null)
        {
            oxygenLevel.RefillOxygen(refillAmount);
            Destroy(gameObject);
        }
    }
 
}
