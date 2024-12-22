using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private MainMenuManager mainMenuManager;

    public void StartConnection()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        mainMenuManager.OpenLobby();
    }

    public void DisconnectFromServer()
    {
        PhotonNetwork.Disconnect();
    }
}
