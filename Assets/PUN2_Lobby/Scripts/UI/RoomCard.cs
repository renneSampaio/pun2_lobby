using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class RoomCard : MonoBehaviourPunCallbacks
{
    public string roomName;


    // Method called by the RoomCard button
    public void EnterRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
