using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform camera;
    private Transform playerTF;
    private PlayerSpawner playerSpawner;

    void Start()
    {
        playerSpawner = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
        PlayerSpawner.OnPlayerCreated += HandlePlayerCreated;
    }

    void OnDestroy()
    {
        PlayerSpawner.OnPlayerCreated -= HandlePlayerCreated;
    }

    void Update()
    {
        if(playerTF != null)
        {
            transform.position = new Vector2(camera.position.x * 0.2f, playerTF.position.y * -0.1f);
        }
    }

    private void HandlePlayerCreated(GameObject player)
    {
        playerTF = player.transform;
    }
}
