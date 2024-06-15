using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPack : MonoBehaviour
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
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            AudioSource.PlayClipAtPoint(pickUpSound, transform.position, volume);
            playerMovement.Refuel(refillAmount);
            Destroy(gameObject);
        }
    }
}
