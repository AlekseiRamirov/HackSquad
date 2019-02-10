using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGroup : Photon.PunBehaviour
{

    [SerializeField]
    private GameObject roomPrefab;
    private GameObject RoomPrefab { get { return roomPrefab; } }

    private List<RoomPrefab> roomPrefabsList = new List<RoomPrefab>();
    private List<RoomPrefab> RoomPrefabsList { get { return roomPrefabsList; } }

    public GameObject panelListRooms;

    public override void OnReceivedRoomListUpdate()
    {
        if (panelListRooms.activeSelf)
        {
            RoomInfo[] rooms = PhotonNetwork.GetRoomList();

            foreach (RoomInfo room in rooms)
            {
                RoomReceived(room);
            }

            RemoveOldRooms();
        }
    }



    private void RoomReceived(RoomInfo room)
    {
        Debug.LogWarning("Aca entra");
        int index = RoomPrefabsList.FindIndex(x => x.RoomName == room.Name);
        Debug.LogWarning("Index arrojado" + index);

        if (index == -1)
        {
            if (room.IsVisible && room.PlayerCount < room.MaxPlayers)
            {
                GameObject roomPrefabObj = Instantiate(RoomPrefab);
                roomPrefabObj.transform.SetParent(transform, false);

                RoomPrefab roomPrefab = roomPrefabObj.GetComponent<RoomPrefab>();
                RoomPrefabsList.Add(roomPrefab);

                index = (RoomPrefabsList.Count - 1);
            }
        }

        if (index != -1)
        {
            RoomPrefab roomPrefab = RoomPrefabsList[index];
            roomPrefab.SetRoomNameText(room.Name);
            roomPrefab.Updated = true;
        }
    }

    private void RemoveOldRooms()
    {
        List<RoomPrefab> removeRooms = new List<RoomPrefab>();

        foreach (RoomPrefab roomPrefab in RoomPrefabsList)
        {
            if (!roomPrefab.Updated)
                removeRooms.Add(roomPrefab);
            else
                roomPrefab.Updated = false;

            Debug.LogWarning("Se añadió una sala eliminda");
        }

        foreach (RoomPrefab roomPrefab in removeRooms)
        {
            GameObject roomPrefabObj = roomPrefab.gameObject;
            RoomPrefabsList.Remove(roomPrefab);
            Destroy(roomPrefabObj);
            Debug.LogWarning("Se destruyo el prefab de una sala");
        }
    }

}
