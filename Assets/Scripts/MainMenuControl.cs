using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public GameObject instructionsText;
    public GameObject creditsText;
    public GameObject playButton;
    public GameObject creditsButton;
    public GameObject quitButton;
    public GameObject backButton;

    public void Play () {
        SceneManager.LoadScene("GameScene");
    }

    public void Quit () {
        Application.Quit();
    }

    public void Back () {
        creditsText.SetActive(false);
        backButton.SetActive(false);

        instructionsText.SetActive(true);
        playButton.SetActive(true);
        creditsButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void ShowCredits () {
        instructionsText.SetActive(false);
        playButton.SetActive(false);
        creditsButton.SetActive(false);
        quitButton.SetActive(false);

        creditsText.SetActive(true);
        backButton.SetActive(true);
    }
}
