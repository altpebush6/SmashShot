using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameState;
    private bool gameOverState;

    [SerializeField] private CameraManager CameraManager;
    [SerializeField] private BallManager BallManager;
    [SerializeField] private TimeManager TimeManager;

    [SerializeField] private GameObject gameOver;

    void Start()
    {
        gameState = false;        
    }

    public bool isGameActive()      { return gameState; }
    public void SetGameActive()     { gameState = true; }
    public void SetGameDeactive()   { gameState = false; }

    public bool isGameOverActive()      { return gameOverState; }
    public void SetGameOverDeactive()   { gameOverState = false; }
    public void SetGameOverActive()
    {
        gameOverState = true; 
        gameOver.SetActive(true);
    }
}
