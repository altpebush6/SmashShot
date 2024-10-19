using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    [SerializeField] private Transform camera;
    [SerializeField] private Transform player;

    void Update()
    {
        transform.position = new Vector2(camera.position.x * 0.2f, player.position.y * -0.1f);
    }
}
