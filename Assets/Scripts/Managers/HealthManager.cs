using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private float health;

    [SerializeField] private Texture2D fullHeart;
    [SerializeField] private Texture2D emptyHeart;

    [SerializeField] private GameObject[] hearts;

    void Start()
    {
        foreach(GameObject heart in hearts)
        {
            heart.GetComponent<RawImage>().texture = fullHeart;
        } 
    }

    void Update()
    {
        
    }
}
