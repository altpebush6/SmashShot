using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    private GameManager GM;

    [SerializeField] private float health;

    [SerializeField] private Texture2D fullHeart;
    [SerializeField] private Texture2D emptyHeart;

    [SerializeField] private GameObject[] hearts;

    [SerializeField] private Animator playerAnim;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        foreach(GameObject heart in hearts)
        {
            heart.GetComponent<RawImage>().texture = fullHeart;
        } 
    }

    public void GetDamage()
    {
        health--;
        RenderHearts();

        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        playerAnim.CrossFade("Die", 0f);
        GM.SetGameDeactive();
        GM.SetGameOverActive();
    }

    void RenderHearts()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].GetComponent<RawImage>().texture = fullHeart;
            }
            else
            {
                hearts[i].GetComponent<RawImage>().texture = emptyHeart;
            }
        }
    }
}
