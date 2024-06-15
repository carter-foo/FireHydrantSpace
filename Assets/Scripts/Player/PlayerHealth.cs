using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //public AudioClip hurtSound1;
    //public AudioClip hurtSound2;
    //public AudioClip hurtSound3;
    //public AudioClip hurtSound4;
    //public AudioClip hurtSound5;

    public AudioClip[] hurtSounds;

    public float volume = 0.5f;

    public float maxHealth = 100f;
    public float currentHealth;

    private OxygenLevel oxygenLevel;

    // Start is called before the first frame update
    void Start()
    {

        currentHealth = maxHealth;
        oxygenLevel = GetComponent<OxygenLevel>();
        
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {

        HurtSoundRandomizer();

        //currentHealth -= damage;
        //if (currentHealth < 0) currentHealth = 0;
        oxygenLevel.DepleteOxygen(damage);

        // Check if Player is game ended :3
        //if (currentHealth == 0)
        //{
        //    Die();
        //}
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    } 

    public void Die()
    {
            // GAME OVER
            Debug.Log("Player has become one with space trash");
        
    }

    // For when Player is hit
    void OnCollisionEnter(Collision collision)
    {
        TakeDamage(10f);

    }

    void HurtSoundRandomizer()
    {
        int randomIndex = Random.Range(0, hurtSounds.Length);
        AudioClip selectedSound = hurtSounds[randomIndex];

        AudioSource.PlayClipAtPoint(selectedSound, transform.position, volume);

    }

   
}
