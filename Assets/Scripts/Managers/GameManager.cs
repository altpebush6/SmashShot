using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameState;
    private bool gameOverState;
    private bool pauseState;

    [SerializeField] private CameraManager CameraManager;
    [SerializeField] private BallManager BallManager;
    [SerializeField] private TimeManager TimeManager;

    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseMenu;

    void Start()
    {
        gameState = false;        
    }

    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if(isGameActive())
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        SetGameDeactive();
        SetPauseActive();  
    }

    public void Resume()
    {
        Time.timeScale = 1;
        SetPauseDeactive();
        SetGameActive();      
    }

    public bool isGameActive()      { return gameState; }
    public void SetGameActive()     { gameState = true; }
    public void SetGameDeactive()   { gameState = false; }

    public bool isPauseActive()      { return pauseState; }
    public void SetPauseActive()
    {
        pauseState = true; 
        pauseMenu.SetActive(true);
    }
    public void SetPauseDeactive()
    {
        pauseState = false; 
        pauseMenu.SetActive(false);
    }

    public bool isGameOverActive()      { return gameOverState; }
    public void SetGameOverDeactive()   { gameOverState = false; }
    public void SetGameOverActive()
    {
        gameOverState = true; 
        gameOver.SetActive(true);
    }
}
