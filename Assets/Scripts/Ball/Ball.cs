using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviour
{
    public string owner;

    void Start()
    {
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;

        PhotonView photonView = GetComponent<PhotonView>();

        owner = photonView.Owner.NickName;

        if(!photonView.IsMine)
        {
            Color color = PlayerColors.GetPlayerColor(photonView.Owner.ActorNumber);
            GetComponent<SpriteRenderer>().color = color;
            transform.Find("Trail").gameObject.GetComponent<TrailRenderer>().startColor  = color;
        }
    }
}
