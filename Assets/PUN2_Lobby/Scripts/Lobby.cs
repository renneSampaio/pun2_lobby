using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Lobby : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject connectionScreen;

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.NickName = "Player" + Random.Range(0, 999).ToString();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        connectionScreen.SetActive(false);
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectionScreen.SetActive(true);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined a Lobby");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room sucessfully created");
    }

}
