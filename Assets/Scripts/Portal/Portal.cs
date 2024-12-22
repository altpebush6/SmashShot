using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

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

            if(!portalPassed)
            {
                ScoreManager.Instance.UpdateScore(player.name, 100);
                portalPassed = true;
                portalAudio.Play();
                Instantiate(passParticle, transform.position, Quaternion.identity);

                if(player.GetComponent<PhotonView>().IsMine)
                {
                    GM.SetGameDeactive();
                    StartCoroutine(PassPortal());
                }
            }
        }
    }

    IEnumerator PassPortal()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerManager>().PassPortal(sceneName);
    }
}
