using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using Unity.VisualScripting;

public class RoomListing : MonoBehaviourPunCallbacks
{
    private Dictionary<string, RoomCard> cachedRoomList = new Dictionary<string, RoomCard>();

    [SerializeField]
    private GameObject contentContainer;

    [SerializeField]
    private GameObject roomPrefab;

    public void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        foreach (var info in roomList)
        {
            if (info.RemovedFromList)
            {
                if (cachedRoomList.ContainsKey(info.Name))
                {
                    Destroy(cachedRoomList[info.Name].gameObject);
                    cachedRoomList.Remove(info.Name);
                }
            }
            else
            {
                if (!cachedRoomList.ContainsKey(info.Name))
                {
                    var newRoomObj = Instantiate(roomPrefab, contentContainer.transform);
                    var newRoomCardText = newRoomObj.GetComponentInChildren<TMP_Text>();
                    newRoomCardText.text = info.Name;

                    var newRoomCard = newRoomObj.GetComponent<RoomCard>();
                    newRoomCard.roomName = info.Name;
                    cachedRoomList[info.Name] = newRoomCard;
                }
            }
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        foreach (var roomInfo in cachedRoomList)
        {
            Destroy(roomInfo.Value.gameObject);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room listing updated");
        UpdateCachedRoomList(roomList);
    }

    public override void OnJoinedLobby()
    {
        cachedRoomList.Clear();
    }

    public override void OnLeftLobby()
    {
        cachedRoomList.Clear();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        foreach (var roomInfo in cachedRoomList)
        {
            Destroy(roomInfo.Value.gameObject);
        }
        cachedRoomList.Clear();
    }
}
