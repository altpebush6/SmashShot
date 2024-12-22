using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Portal : MonoBehaviour
{
    private GameManager GM;

    [SerializeField] private float distance;

    [SerializeField] private GameObject gameDone;
    [SerializeField] private GameObject passParticle;

    [SerializeField] private AudioSource portalAudio;
    [SerializeField] private string sceneName;
    
    private GameObject player;
    private PlayerSpawner playerSpawner;
    private bool portalPassed;

    void Awake()
    {
        PlayerSpawner.OnPlayerCreated += HandlePlayerCreated;
    }

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        playerSpawner = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
        
        portalPassed = false;
    }

    void OnDestroy()
    {
        PlayerSpawner.OnPlayerCreated -= HandlePlayerCreated;
    }

    private void HandlePlayerCreated(GameObject player)
    {
        if(player.GetComponent<PhotonView>().IsMine)
        {
            this.player = player;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;

            if(player.GetComponent<PhotonView>().IsMine && !portalPassed)
            {
                portalAudio.Play();

                Instantiate(passParticle, transform.position, Quaternion.identity);

                player.GetComponent<PlayerManager>().PassPortal(sceneName);

                portalPassed = true;
            }
        }
    }
}
