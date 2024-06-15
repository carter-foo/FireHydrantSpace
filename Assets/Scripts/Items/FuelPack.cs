using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPack : MonoBehaviour
{
    public AudioClip pickUpSound;
    public float volume;

    public float refillAmount = 25f;

    public float floatSpeed = 0.5f;
    public float floatHeight = 0.5f;
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
        // Floating effect
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotating effect
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
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
