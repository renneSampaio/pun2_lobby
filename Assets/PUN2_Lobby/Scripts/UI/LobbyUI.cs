using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using WebSocketSharp;

public class LobbyUI : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject lobbyContainer;

    [SerializeField]
    private TMP_Text playerNameText;

    [SerializeField]
    private TMP_InputField playerNameInput;

    [SerializeField]
    private TMP_InputField roomNameInput;

    public void CreateRoom()
    {

        string roomName = roomNameInput.text.Trim();
        if (roomName.IsNullOrEmpty())
        {
            return;
        }

        PhotonNetwork.CreateRoom(roomName);
    }

    public void SetNickName()
    {
        string playerName = playerNameInput.text.Trim();
        if (playerName.IsNullOrEmpty())
        {
            return;
        }

        PhotonNetwork.NickName = playerName;
        playerNameText.text = playerName;
    }

    public override void OnJoinedLobby()
    {
        lobbyContainer.SetActive(true);
        playerNameText.text = PhotonNetwork.NickName;
    }

    public override void OnJoinedRoom()
    {
        lobbyContainer.SetActive(false);
    }

    public override void OnLeftRoom()
    {
        lobbyContainer.SetActive(true);
    }
}
