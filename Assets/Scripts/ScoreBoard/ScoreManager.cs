using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("ScoreManager").AddComponent<ScoreManager>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Dictionary<string, int> scores = new Dictionary<string, int>();

    public void UpdateScore(string playerName, int score)
    {
        if(scores[playerName] + score >= 0)
        {
            scores[playerName] += score;
        }
        GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().ListActivePlayers();
    }

    public string GetWinner()
    {
        return scores.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
    }
}
