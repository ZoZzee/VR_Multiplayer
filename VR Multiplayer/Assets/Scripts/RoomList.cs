using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class RoomList : MonoBehaviourPunCallbacks
{
    public GameObject roomPrefab;
    public GameObject[] allRooms;
    [SerializeField] private Transform roomContent;
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for(int i =0; i<allRooms.Length; i++)
        {
            if (allRooms[i] != null)
            {
                Destroy(allRooms[i]);
            }

        }

        allRooms = new GameObject[roomList.Count];

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount >= 1)
            {
                GameObject Room = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity, roomContent);
                Room.GetComponent<Room>().Name.text = roomList[i].Name;

                allRooms[i] = Room;
            }
        }
    }
}
