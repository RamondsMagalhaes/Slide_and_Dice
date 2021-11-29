using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIStage : MonoBehaviour
{
    public GameObject stopMenu;
    public TextMeshProUGUI pauseDeadText;
    public Button resumeButton;

    private void ToggleStopMenu(bool isShow)
    {
        GameManager.IsGameActive = !isShow;
        stopMenu.SetActive(isShow);
    }
    public void DeathScreen()
    {
        ToggleStopMenu(true);
        pauseDeadText.text = "Dead";
        resumeButton.enabled = false;
    }
    public void PauseScreen(bool isShow)
    {
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in audios)
        {
            if (GameManager.IsGameActive)
            {
                audio.Pause();
            }
            else
            {
                audio.UnPause();
            }
        }
        ToggleStopMenu(isShow);
        pauseDeadText.text = "Pause";

    }
}
