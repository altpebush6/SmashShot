using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject Controls;

    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private Image soundImage;
    [SerializeField] private Sprite muted;
    [SerializeField] private Sprite unmuted;
    private bool paused = false;

    public void OpenControls()
    {
        MainMenu.SetActive(false);
        Controls.SetActive(true);
    }

    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        Controls.SetActive(false);
    }

    public void ToggleMusic()
    {
        if(!paused)
        {
            backgroundMusic.Pause();
            soundImage.sprite = muted;
            paused = true;
        }
        else
        {
            backgroundMusic.Play(0);
            soundImage.sprite = unmuted;
            paused = false;
        }
    }
}
