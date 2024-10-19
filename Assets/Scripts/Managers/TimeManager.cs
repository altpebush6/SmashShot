using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    private GameManager GM;

    [SerializeField] private float countdownTime;
    [SerializeField] private TextMeshProUGUI countdownText;

    private bool timerRunning;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        timerRunning = false;
    }

    void Update()
    {
        if(GM.isGameActive())
        {
            timerRunning = true;
        }

        if (timerRunning)
        {
            countdownTime -= Time.deltaTime;

            countdownText.text = countdownTime.ToString("F1") + "s";
            
            if (countdownTime <= 0f)
            {
                countdownTime = 0f;
                timerRunning = false;
            }
        }
    }
}
