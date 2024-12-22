using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomName;

    [SerializeField] private GameObject[] allRooms;
    [SerializeField] private GameObject RoomPrefab;
    [SerializeField] private Transform ContentTF;

    [SerializeField] private GameObject[] allPlayers;
    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private Transform PlayersContentTF;
    [SerializeField] private TMP_Text playerNum;
    [SerializeField] private TMP_Text roomNameText;

    [SerializeField] private GameObject CreateRoomMenu;
    [SerializeField] private GameObject JoinRoomMenu;
    [SerializeField] private GameObject RoomMenu;

    [SerializeField] private GameObject StartButton;

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();

        GameObject.Find("ServerManager").GetComponent<ConnectToServer>().DisconnectFromServer();
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomName.text, new RoomOptions() {MaxPlayers = 4, IsVisible = true, IsOpen = true}, TypedLobby.Default, null);
        Debug.Log("Room is Created.");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined to the lobby.");
    }    
    public override void OnLeftLobby()
    {
        Debug.Log("Left the lobby.");
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        CreateRoomMenu.SetActive(false);
        JoinRoomMenu.SetActive(false);
        RoomMenu.SetActive(true);
        GameObject.Find("SceneManager").GetComponent<MainMenuManager>().currentPage = RoomMenu;
        ListActivePlayers();

        if(PhotonNetwork.IsMasterClient)
        {
            StartButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel("Level 1");
    }

    public void LeaveRoom()
    {
        StartButton.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ListActivePlayers();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ListActivePlayers();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("The new Master Client is now: " + newMasterClient.NickName);
        if(PhotonNetwork.IsMasterClient)
        {
            StartButton.SetActive(true);
        }
    }

    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (GameObject room in allRooms)
        {
            if(room != null)
            {
                Destroy(room);
            }
        }

        allRooms = new GameObject[roomList.Count];
        int i = 0;

        foreach (RoomInfo room in roomList)
        {
            if(room.IsOpen && room.IsVisible && room.PlayerCount >= 1)
            {
                GameObject Room = Instantiate(RoomPrefab, Vector3.zero, Quaternion.identity, ContentTF);
                Room.GetComponent<Room>().name.text = room.Name;

                allRooms[i] = Room;
                i++;
            }
        }
    }

    public void ListActivePlayers()
    {
        Player[] playerList = PhotonNetwork.PlayerList;

        foreach (GameObject player in allPlayers)
        {
            if(player != null)
            {
                Destroy(player);
            }
        }

        allPlayers = new GameObject[playerList.Length];
        int i = 0;

        foreach (Player player in playerList)
        {
            GameObject playerGO = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity, PlayersContentTF);
            playerGO.GetComponent<PlayerInfo>().SetName(player.NickName);

            allPlayers[i] = playerGO;
            i++;
        }

        playerNum.text = playerList.Length.ToString() + "/4";
    }
}
