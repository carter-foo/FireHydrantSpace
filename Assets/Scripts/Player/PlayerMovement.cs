using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrust = 1f;
    public float decelerationRate = 0.95f;
    private Rigidbody rb;
    public Transform cameraTransform;
    public Transform extinguisherNozzle; // added so the foam particles can continously follow direction of camera when shot out
    public ParticleSystem foamParticleSystem;
    public AudioClip[] spraySoundClips;
    public AudioSource audioSource;

    public float maxFuel = 100f;
    public float currentFuel;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        currentFuel = maxFuel;

        // Audio
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;

        // Foam Particles
        if (foamParticleSystem != null)
        {
            var main = foamParticleSystem.main;
            main.simulationSpace = ParticleSystemSimulationSpace.World; // mkaing sure its set to world not local cause im dumb

            foamParticleSystem.Stop();
            ParticleSystem.EmissionModule emission = foamParticleSystem.emission;
            emission.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && currentFuel > 0)
        {
            ApplyThrust();

            // Foam Particles
            if (foamParticleSystem != null)
            {
                foamParticleSystem.transform.position = extinguisherNozzle.position;
                foamParticleSystem.transform.rotation = extinguisherNozzle.rotation;

                ParticleSystem.EmissionModule emission = foamParticleSystem.emission;
                emission.enabled = true;
                if (!foamParticleSystem.isPlaying)
                {
                    foamParticleSystem.Play();
                }
            }

            // Audio
            if (!audioSource.isPlaying)
            {
                PlayRandomSpraySound();
            }
        } 
        else
        {
            Decelerate();

            // Foam Particles
            if (foamParticleSystem != null)
            {
                ParticleSystem.EmissionModule emission = foamParticleSystem.emission;
                emission.enabled = false;
            }

            // Auido
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    void ApplyThrust()
    {
        Vector3 forceDirection = -cameraTransform.forward; // Thrust Backwards
        rb.AddForce(forceDirection * thrust, ForceMode.Acceleration);

        // Fuel Economy
        currentFuel -= thrust * Time.deltaTime;
        if (currentFuel < 0) currentFuel = 0; 
    }

    void Decelerate() 
    {
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, (1 - decelerationRate) * Time.deltaTime);
    }

    public void Refuel(float amount)
    {
        currentFuel += amount;
        if (currentFuel > maxFuel) currentFuel = maxFuel;
    }

    public Vector3 GetCurrentVelocity() 
    {
        return rb.velocity;
    }

    // Made so spraying isn't so annoying
    void PlayRandomSpraySound()
    {
        int randomIndex = Random.Range(0, spraySoundClips.Length);
        AudioClip selectedSound = spraySoundClips[randomIndex];
        audioSource.clip = selectedSound;
        audioSource.Play();
    }
}
