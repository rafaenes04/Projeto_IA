using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomList : MonoBehaviourPunCallbacks
{
    public GameObject RoomPrefab;
    public GameObject[] AllRooms;
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for(int i = 0; i < AllRooms.Length; i++)
        {
            if(AllRooms[i] != null)
            {
                Destroy(AllRooms[i]);
            }
        }

        AllRooms = new GameObject[roomList.Count];


        for (int i = 0; i < roomList.Count; i++) {
            if (roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount >= 0)
            {
               GameObject Room = Instantiate(RoomPrefab, Vector3.zero, Quaternion.identity,GameObject.Find("Content").transform);
                Room.transform.localPosition = new Vector3(Room.transform.localPosition.x, Room.transform.localPosition.y, 0);
                Room.GetComponent<Room>().Name.text = roomList[i].Name;

                Room.GetComponent<Room>().UpdateRoomInfo(roomList[i].Name, roomList[i].PlayerCount, 4);

                AllRooms[i] = Room;

            }

            
        } 
    }
}
