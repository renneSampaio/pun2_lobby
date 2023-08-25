using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Text;
using WebSocketSharp;

public class RoomUI : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject roomContainer;

    [SerializeField]
    private TMP_Text chatBox;

    [SerializeField]
    private TMP_InputField chatInput;

    private PlayerListing playerList;

    // Method called by the ExitRoomButton
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinedRoom()
    {
        // When entering the room, activate the room interface and clear chat box
        roomContainer.SetActive(true);
        chatBox.text = string.Empty;
    }

    public override void OnLeftRoom()
    {
        roomContainer.SetActive(false);
    }

    public void PrepateChatMessage()
    {
        string chatMessage = chatInput.text.Trim();

        if (chatMessage.IsNullOrEmpty())
        {
            return;
        }

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(PhotonNetwork.NickName);
        stringBuilder.Append(": ");
        stringBuilder.AppendLine(chatMessage);

        this.photonView.RPC("SendChatMessage", RpcTarget.All, stringBuilder.ToString());

        chatInput.text = string.Empty;
    }

    [PunRPC]
    void SendChatMessage(string message)
    {
        chatBox.text += message;
    }

}
