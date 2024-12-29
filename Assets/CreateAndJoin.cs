using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public InputField input_Create;
    public InputField input_Join;
 
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(input_Create.text, new RoomOptions() {MaxPlayers = 4, IsVisible = true, IsOpen = true}, TypedLobby.Default, null );
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(input_Join.text);
    }
    public void JoinRoomInList(string RoomName)
    {
        PhotonNetwork.JoinRoom(RoomName);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GabrielSampleScene");
    }
}
