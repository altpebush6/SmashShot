using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private GameManager GM;
    private HealthManager HM;
    
    private float damageInterval = 1f;
    private float damageCooldown;

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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(GM.isGameActive())
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                GameObject ball = collision.gameObject;

                float damageAmount = ball.GetComponent<Rigidbody2D>().velocity.magnitude;

                ball.SetActive(false);

                GetComponent<Enemy>().GetDamage(damageAmount);
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(GM.isGameActive())
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (damageCooldown <= 0)
                {
                    HM.GetDamage();
                    damageCooldown = damageInterval;
                }
            }
        }
    }
}
