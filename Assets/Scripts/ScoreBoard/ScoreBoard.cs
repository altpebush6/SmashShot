using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private RectTransform PlayerContentTF;

    private GameObject[] allPlayers;

    void Start()
    {
        ListActivePlayers();
    }

    public void ListActivePlayers()
    {
        DestroyAllChildren();
        
        Player[] playerList = PhotonNetwork.PlayerList;

        if(allPlayers != null)
        {
            foreach (GameObject player in allPlayers)
            {
                if(player != null)
                {
                    Destroy(player);
                }
            }
        }

        allPlayers = new GameObject[playerList.Length];
        int i = 0;

        foreach (Player player in playerList)
        {
            GameObject playerBoard = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity, PlayerContentTF);
           
            RectTransform playerBoardRT = playerBoard.GetComponent<RectTransform>();
            Vector2 currentAnchoredPos = playerBoardRT.anchoredPosition;
            playerBoardRT.anchoredPosition = new Vector2(0, (player.ActorNumber - 1) * (-35));

            PlayerBoard playerBoardScript = playerBoard.GetComponent<PlayerBoard>();
            playerBoardScript.username = player.NickName;
            playerBoardScript.score = (ScoreManager.Instance.scores.ContainsKey(playerBoardScript.username)) ? ScoreManager.Instance.scores[playerBoardScript.username] : 0;
            playerBoardScript.SetText();

            if(!(ScoreManager.Instance.scores.ContainsKey(playerBoardScript.username)))
            {
                ScoreManager.Instance.scores[playerBoardScript.username] = playerBoardScript.score;
            }

            allPlayers[i] = playerBoard;
            i++;
        }
    }

    void DestroyAllChildren()
    {
        foreach (Transform child in PlayerContentTF)
        {
            Destroy(child.gameObject);
        }
    }
}
