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

    void Start()
    {
        PhotonNetwork.JoinLobby();
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
        PhotonNetwork.LoadLevel("Level 1");
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
}
