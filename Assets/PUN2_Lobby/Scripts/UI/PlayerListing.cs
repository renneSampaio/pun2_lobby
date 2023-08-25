using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    private Dictionary<int, GameObject> playerList = new Dictionary<int, GameObject>();

    [SerializeField]
    private GameObject contentContainer;

    [SerializeField]
    private GameObject playerCardPrefab;

    public override void OnEnable()
    {
        base.OnEnable();

        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerData(player.Value);
        }
    }

    public void AddPlayerData(Player newPlayer)
    {
        // Create a new player card and add a  reference to it on the player list
        // so that we destroy it later

        var newPlayerCard = Instantiate(playerCardPrefab, contentContainer.transform);
        var newPlayerText = newPlayerCard.GetComponent<TMP_Text>();
        newPlayerText.text = newPlayer.NickName;

        playerList[newPlayer.ActorNumber] = (newPlayerCard);
    }

    private void CleanupPlayerList()
    {
        foreach (var player in playerList)
        {
            Destroy(player.Value.gameObject);
        }
        playerList.Clear();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered the room! ", newPlayer.NickName);

        AddPlayerData(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left the room! ", otherPlayer.NickName);

        Destroy(playerList[otherPlayer.ActorNumber]);
        playerList.Remove(otherPlayer.ActorNumber);
    }

    public override void OnJoinedRoom()
    {
        playerList.Clear();

        // When first entering the room, get the full list of players
        // and populate the player cache and cards
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerData(player.Value);
        }
    }

    public override void OnLeftRoom()
    {
        // Cleanup all player cards from the ui when leaving the room
        CleanupPlayerList();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        // Cleanup all player cards from the UI in the case of a Disconnect 
        CleanupPlayerList();
    }
}

