using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private float distance;

    [SerializeField] private GameObject player;
    [SerializeField] private SceneLoader SceneLoader;
    [SerializeField] private string sceneName;

    void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < 0.2f)
        {
            SceneLoader.LoadSceneByName(sceneName);
        }
    }
}
