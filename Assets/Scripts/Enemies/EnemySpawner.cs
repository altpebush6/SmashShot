using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] patrols;

    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            for(int i = 0; i < enemyPrefabs.Length; i++)
            {
                GameObject enemy = PhotonNetwork.Instantiate(enemyPrefabs[i].name, patrols[i].transform.position, Quaternion.identity);

                EnemyPatrol ep = enemy.GetComponent<EnemyPatrol>();

                ep.SetCurrentPoint(patrols[i].transform.Find("Point.A"));
                ep.SetPointA(patrols[i].transform.Find("Point.A"));
                ep.SetPointB(patrols[i].transform.Find("Point.B"));
            }
        }
    }
}
