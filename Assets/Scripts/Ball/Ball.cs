using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float minY;

    void Update()
    {
        if(transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }
}
