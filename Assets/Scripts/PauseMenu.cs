using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseOverlay;
    public GameObject GameOverOverlay;

    public AudioSource audioSource;
    public AudioClip ButtonClick;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && GameOverOverlay.activeSelf == false)
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
                PauseOverlay.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                PauseOverlay.SetActive(false);
            }

        }
    }

    public void GameOver()
    {
        GameOverOverlay.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseOverlay.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickButton()
    {
        audioSource.PlayOneShot(ButtonClick);
    }
}
