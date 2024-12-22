using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Username : MonoBehaviour 
{
    public void SetUserName(string username)
    {
        PhotonNetwork.NickName = username;
    }
}
