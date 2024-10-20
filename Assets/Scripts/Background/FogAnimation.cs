using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float resetPositionX = -10f;
    [SerializeField] private float startPositionX = 10f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x <= resetPositionX)
        {
            transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);
        }
    }
}
