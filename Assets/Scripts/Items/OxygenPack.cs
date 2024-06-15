using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class OxygenPack : MonoBehaviour
{
    public AudioClip pickUpSound;
    public float volume;

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
            AudioSource.PlayClipAtPoint(pickUpSound, transform.position, volume);
            oxygenLevel.RefillOxygen(refillAmount);
            Destroy(gameObject);
        }
    }
 
}
