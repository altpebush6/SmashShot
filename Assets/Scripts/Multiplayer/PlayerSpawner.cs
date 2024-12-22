using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoint;

    public delegate void PlayerCreated(GameObject player);
    public static event PlayerCreated OnPlayerCreated;

    void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        string playerName = PhotonNetwork.LocalPlayer.NickName;

        GameObject player = PhotonNetwork.Instantiate("Player", spawnPoint[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.position, Quaternion.identity);

        player.name = playerName;
        player.GetComponent<PlayerManager>().Mark.SetActive(true);

        GameObject.Find("HealthManager").GetComponent<HealthManager>().ResetHealth();

        OnPlayerCreated?.Invoke(player);
    }
}
