using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deadParticle;

    [SerializeField] private Slider healthBar;

    [SerializeField] private float maxHealth;
    private float health;

    [SerializeField] private AudioSource destroyAudio;

    void Start()
    {
        health = maxHealth;
        RenderHealthBar();

        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;
        
        if(!GetComponent<PhotonView>().IsMine)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public void GetDamage(float damage, string owner)
    {
        health -= damage;

        ScoreManager.Instance.UpdateScore(owner, 10);

        if(health <= 0)
        {
            PhotonNetwork.Instantiate(deadParticle.name, transform.position, Quaternion.identity);
            // destroyAudio.Play();
            gameObject.SetActive(false);
        }

        RenderHealthBar();
    }

    void RenderHealthBar()
    {
        healthBar.value = health / maxHealth;
    }
}
