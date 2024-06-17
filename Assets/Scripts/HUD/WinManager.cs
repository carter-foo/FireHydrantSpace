using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public float fadeToBlackTime = 5.0f;
    public GameObject[] disableOnWin;
    public GameObject winScreen;
    public CameraFollow cutsceneCamera;
    public GameObject playerCamera;
    public UnityEngine.UI.RawImage blackImage;

    public AudioClip yesClip;
    public AudioClip partyBlower;
    public float volume = 0.5f;

    private GameObject player;
    private float fadeOpacity = -1.0f;
    private bool inCutscene = false;

    // Start is called before the first frame update
    void Start()
    {
        winScreen.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!inCutscene) return;

        fadeOpacity += Time.deltaTime / fadeToBlackTime;
        if(fadeOpacity >= 1.0f) {
            fadeOpacity = 1.0f;
            inCutscene = false;
            ShowWinScreen();
        }

        var col = blackImage.color;
        col.a = fadeOpacity;
        blackImage.color = col;
    }

    public void Win() {
        player.SetActive(false);
        playerCamera.SetActive(false);
        cutsceneCamera.gameObject.SetActive(true);
        FindObjectOfType<ShipFlyCutscene>().Begin();
        foreach (var item in disableOnWin)
        {
            item.SetActive(false);
        }
        inCutscene = true;
    }

    public void ShowWinScreen()
    {
        AudioSource.PlayClipAtPoint(partyBlower, Camera.main.transform.position, volume);
        AudioSource.PlayClipAtPoint(yesClip, Camera.main.transform.position, volume);

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
