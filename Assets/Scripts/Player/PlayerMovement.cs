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
    public AudioClip fartSound;
    
    public AudioSource audioSource;

    public float maxFuel = 100f;
    public float currentFuel;
    public float lostRateFuel = 2f;




    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            if (currentFuel > 0)
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
                // Play fart sound when there's no fuel
                if (!audioSource.isPlaying || audioSource.clip != fartSound)
                {
                    PlayFartSound();
                }
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

            // Audio
            if (audioSource.isPlaying && audioSource.clip != fartSound)
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
        currentFuel -= thrust * lostRateFuel * Time.deltaTime;
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

    public float GetCurrentFuel()
    {
        return currentFuel;
    }

    // Made so spraying isn't so annoying
    void PlayRandomSpraySound()
    {
        int randomIndex = Random.Range(0, spraySoundClips.Length);
        AudioClip selectedSound = spraySoundClips[randomIndex];
        audioSource.clip = selectedSound;
        audioSource.Play();
    }

    void PlayFartSound()
    {
        audioSource.clip = fartSound;
        audioSource.loop = false;
        audioSource.Play();
    }
}
