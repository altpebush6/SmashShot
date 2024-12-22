using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBoard : MonoBehaviour
{
    public string username;
    public int score;

    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private TMP_Text scoreText;

    public void SetText()
    {
        usernameText.text = username;
        scoreText.text = score.ToString();
    }
}
