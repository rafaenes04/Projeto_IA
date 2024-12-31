using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Room : MonoBehaviour
{
    public Text Name;
    public Text PlayerCount;  

    public void UpdateRoomInfo(string roomName, int playerCount, int maxPlayers)
    {
        Name.text = roomName;
        PlayerCount.text = $"{playerCount}/{maxPlayers}";
    }
    public void JoinRoom()
    {
        GameObject.Find("CreateAndJoin").GetComponent<CreateAndJoin>().JoinRoomInList(Name.text);
    }
}
