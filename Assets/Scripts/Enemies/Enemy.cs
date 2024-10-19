using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyObj;

    [SerializeField] private Slider healthBar;

    [SerializeField] private float maxHealth;
    private float health;


    void Start()
    {
        health = maxHealth;
        RenderHealthBar();
    }

    public void GetDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            enemyObj.SetActive(false);
        }

        RenderHealthBar();
    }

    void RenderHealthBar()
    {
        healthBar.value = health / maxHealth;
    }
}
