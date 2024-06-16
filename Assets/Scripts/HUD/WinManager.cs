using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public GameObject winScreen;

    public AudioClip yesClip;
    public AudioClip partyBlower;
    public float volume = 0.5f;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        winScreen.SetActive(false);

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowWinScreen()
    {
        AudioSource.PlayClipAtPoint(partyBlower, playerTransform.position, volume);
        AudioSource.PlayClipAtPoint(yesClip, playerTransform.position, volume);

        Debug.Log("Game Win");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f; 

        winScreen.SetActive(true); 
    }

    public void RestartGame()
    {

        Time.timeScale = 1f; 

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

}
