using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        // Check if Player is game ended :3
        if (currentHealth == 0)
        {
            Die();
        }
    }

    void Die()
    {
        // GAME OVER
        Debug.Log("Player has become one with space trash");
    }

    // For when Player is hit
    void OnCollisionEnter(Collision collision)
    {
        TakeDamage(25f);

    }
}
