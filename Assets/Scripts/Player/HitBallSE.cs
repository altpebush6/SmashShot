using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBallSE : MonoBehaviour
{
    [SerializeField] private AudioSource hitBallAudio; 

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Ball"))
        {
            hitBallAudio.Play();
        }
    }
}
