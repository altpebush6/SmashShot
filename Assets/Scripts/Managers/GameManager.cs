using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool gameState;

    [SerializeField] private CameraManager CameraManager;
    [SerializeField] private BallManager BallManager;
    [SerializeField] private TimeManager TimeManager;

    void Start()
    {
        gameState = false;        
    }

    void Update()
    {

    }

    public bool isGameActive(){ return gameState; }

    public void SetGameActive()
    {
        gameState = true;
    }

    public void SetGameDeactive()
    {
        gameState = false;
    }
}
