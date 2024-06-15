using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For UI

public class PlayerHUD : MonoBehaviour
{
    //public Text health;
    public Text oxygen;
    public Text fuel;

    //private PlayerHealth playerHealth;
    private OxygenLevel playerOxygen;
    private PlayerMovement playerMovement;


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        //playerHealth = player.GetComponent<PlayerHealth>();
        playerOxygen = player.GetComponent<OxygenLevel>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //health.text = "Health: " + playerHealth.currentHealth.ToString("F0");
        oxygen.text = "Oxygen: " + playerOxygen.currentOxygen.ToString("F0");
        fuel.text = "Fuel: " + playerMovement.currentFuel.ToString("F0");
    }
}
