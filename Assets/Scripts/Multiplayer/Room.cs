using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room : MonoBehaviour
{
    public TMP_Text name;

    public void JoinRoom()
    {
        GameObject.Find("RoomManager").GetComponent<RoomManager>().JoinRoom(name.text);
    }
}
