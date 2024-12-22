using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthManager : MonoBehaviour
{

    private GameManager GM;
    private PlayerSpawner playerSpawner;
    private GameObject player;

    [SerializeField] private float health;
    [SerializeField] private Image damageEffect;
    [SerializeField] private Texture2D fullHeart;
    [SerializeField] private Texture2D emptyHeart;

    [SerializeField] private GameObject[] hearts;

    void Awake()
    {
        PlayerSpawner.OnPlayerCreated += HandlePlayerCreated;
    }

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        foreach(GameObject heart in hearts)
        {
            heart.GetComponent<RawImage>().texture = fullHeart;
        } 
    }

    void OnDestroy()
    {
        PlayerSpawner.OnPlayerCreated -= HandlePlayerCreated;
    }

    public void GetDamage()
    {
        health--;
        RenderHearts();

        if(health <= 0)
        {
            Die();
        }

        StartCoroutine(DamageEffect());
    }

    IEnumerator DamageEffect()
    {
        Color startColor = damageEffect.color;
        startColor.a = 0.4f;
        damageEffect.color = startColor;

        float duration = 1.0f;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0.4f, 0f, elapsed / duration);
            startColor.a = alpha;
            damageEffect.color = startColor;
            yield return null;
        }

        startColor.a = 0f;
        damageEffect.color = startColor;
    }

    public void Die()
    {
        player.GetComponent<PlayerManager>().DestroyPlayer();
        Destroy(player);
        GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>().SpawnPlayer();
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

    private void HandlePlayerCreated(GameObject player)
    {
        if(player.GetComponent<PhotonView>().IsMine)
        {
            this.player = player;
        }
    }

    public void ResetHealth()
    {
        health = 3f;   
        RenderHearts();
    }
}
