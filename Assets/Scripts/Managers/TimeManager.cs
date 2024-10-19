using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    private GameManager GM;

    [SerializeField] private float countdownTime;
    [SerializeField] private TextMeshProUGUI countdownText;

    private Color startColor;
    private Color dangerColor;

    [SerializeField] private float dangerTime;

    private bool timerRunning;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        timerRunning = false;

        startColor = new Color(1f, 1f, 1f, 1f);
        dangerColor = new Color(1f, 0.5f, 0.5f, 1f);
    }

    void Update()
    {
        if(GM.isGameActive())
        {
            timerRunning = true;
        }
        else
        {
            timerRunning = false;
        }

        if (timerRunning)
        {
            countdownTime -= Time.deltaTime;

            countdownText.text = countdownTime.ToString("F1") + "s";

            if (countdownTime <= dangerTime)
            {
                float t = Mathf.Clamp01((dangerTime - countdownTime) / dangerTime);  // Normalize t between 0 and 1
                countdownText.color = Color.Lerp(startColor, dangerColor, t);  // Smoothly transition color
            }


            if (countdownTime <= 0f)
            {
                countdownTime = 0f;
                timerRunning = false;
                GM.SetGameDeactive();
                GM.SetGameOverActive();
            }
        }
    }
}
