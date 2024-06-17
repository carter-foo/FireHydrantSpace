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
    public AudioClip[] playOnHurt;
    public UnityEngine.UI.RawImage[] hurtImages;
    public UnityEngine.UI.RawImage suffocationImage;

    public float volume = 0.5f;
    public float damageOnHit = 25.0f;

    public float maxHealth = 100f;
    public float currentHealth;
    public float hurtFade = 1.0f;
    public float suffocationFade = 10.0f;

    private float hurtOpacity = 0.0f;
    private float suffocationOpacity = 0.0f;

    private OxygenLevel oxygenLevel;
    private GameOverManager gameOverManager;
    private WinManager winManager;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {

        currentHealth = maxHealth;
        oxygenLevel = GetComponent<OxygenLevel>();
        
        gameOverManager = FindObjectOfType<GameOverManager>();
        winManager = FindAnyObjectByType<WinManager>();


    }

    // Update is called once per frame
    void Update()
    {
        hurtOpacity -= hurtFade * Time.deltaTime;
        foreach (var hurtImage in hurtImages)
        {
            var col = hurtImage.color;
            col.a = hurtOpacity;
            hurtImage.color = col;
        }

        if(oxygenLevel.currentOxygen <= 0) {
            suffocationOpacity += Time.deltaTime / suffocationFade;
        }
        else {
            suffocationOpacity = 0.0f;
        }

        {
            var col = suffocationImage.color;
            col.a = suffocationOpacity;
            suffocationImage.color = col;
        }

        if(suffocationOpacity >= 1.0f) {
            suffocationOpacity = 1.0f;
            if(!isDead)
                Die();
        }
    }

    public void TakeDamage(float damage)
    {
        hurtOpacity = 1.0f;
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
        // HurtSoundRandomizer();
        // GAME OVER
        isDead = true;
        Debug.Log("Player has become one with space trash");
       
        var test = GameObject.FindObjectOfType<HUDMarkers>();
        if (test)
        {
            test.enabled = false;
        }
        gameOverManager.ShowGameOver();
        
    }

    // For when Player is hit
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Ship"))
        {
            Debug.Log("Should be Wining");
            winManager.Win();
            hurtOpacity = 0.0f;
            suffocationOpacity = 0.0f;
        } 
        else
        {
            Debug.Log("Offfff");
            TakeDamage(damageOnHit);
        }


    }

    void HurtSoundRandomizer()
    {
        int randomIndex = Random.Range(0, hurtSounds.Length);
        AudioClip selectedSound = hurtSounds[randomIndex];

        AudioSource.PlayClipAtPoint(selectedSound, transform.position, volume);
        foreach (var sound in playOnHurt)
        {
            AudioSource.PlayClipAtPoint(sound, transform.position, 1.0f);
        }
    }

   
}
