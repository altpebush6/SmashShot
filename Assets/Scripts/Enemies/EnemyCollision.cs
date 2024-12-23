using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyCollision : MonoBehaviour
{
    private GameManager GM;
    private HealthManager HM;

    [SerializeField] private GameObject explosion;
    
    private float damageInterval = 1f;
    private float damageCooldown;
    private float damageCooldownOtherPlayers;

    void Start()
    {        
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        HM = GameObject.Find("HealthManager").GetComponent<HealthManager>();
    }

    void Update()
    {
        if (damageCooldown > 0)
        {
            damageCooldown -= Time.deltaTime;
        }

        if (damageCooldownOtherPlayers > 0)
        {
            damageCooldownOtherPlayers -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(GM != null)
        {
            if(GM.isGameActive())
            {
                if (collision.gameObject.CompareTag("Ball"))
                {
                    GameObject ball = collision.gameObject;

                    float damageAmount = 4f;

                    Instantiate(explosion, ball.transform.position, Quaternion.identity);

                    ball.SetActive(false);

                    GetComponent<Enemy>().GetDamage(damageAmount, ball.GetComponent<Ball>().owner);
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(GM.isGameActive())
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (damageCooldownOtherPlayers <= 0)
                {
                    ScoreManager.Instance.UpdateScore(collision.gameObject.name, -5);  
                    damageCooldownOtherPlayers = damageInterval;
                }

                if(damageCooldown <= 0 && collision.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    HM.GetDamage();
                    damageCooldown = damageInterval;
                }
            }
        }
    }
}
