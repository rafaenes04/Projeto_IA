using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("UI Elements")]
    public InputField partyIdInput;
    public InputField playerNameInput;
    public Dropdown skinDropdown;
    public Button createPartyButton;
    public Button joinPartyButton;
    public Button readyButton;

    [Header("Player Settings")]
    public string playerName;
    public int selectedSkinIndex = 0;

    [Header("Preview Character")]
    public Transform previewCharacterSpawnPoint;
    public GameObject[] characterPrefabs; 
    private GameObject currentPreviewCharacter;

    private void Start()
    {
        // Connect to Photon master server
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        // Set up button listeners
        createPartyButton.onClick.AddListener(CreateParty);
        joinPartyButton.onClick.AddListener(JoinParty);
        readyButton.onClick.AddListener(ReadyUp);

        // Assign default player name
        playerNameInput.text = $"Player{Random.Range(1000, 9999)}";
    }

    public void UpdatePlayerName()
    {
        playerName = playerNameInput.text;
        PhotonNetwork.NickName = playerName; // Set Photon player nickname
    }

    public void UpdateSkinSelection()
    {
        selectedSkinIndex = skinDropdown.value;
    }

    public void CreateParty()
    {
        string partyId = partyIdInput.text;

        if (string.IsNullOrEmpty(partyId))
        {
            Debug.LogWarning("Party ID cannot be empty!");
            return;
        }

        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 4 }; // Set max players
        PhotonNetwork.CreateRoom(partyId, roomOptions);
    }

    public void JoinParty()
    {
        string partyId = partyIdInput.text;

        if (string.IsNullOrEmpty(partyId))
        {
            Debug.LogWarning("Party ID cannot be empty!");
            return;
        }

        PhotonNetwork.JoinRoom(partyId);
    }

    public void ReadyUp()
    {
        Debug.Log($"Player {playerName} is ready with skin {selectedSkinIndex}");
        // Additional ready-up logic can go here
    }

    // Callback for when the player joins a room
    public override void OnJoinedRoom()
    {
        Debug.Log($"Joined party: {PhotonNetwork.CurrentRoom.Name}");
        // Spawn the player in the room
        PhotonNetwork.Instantiate("PlayerPrefab", Vector3.zero, Quaternion.identity);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Create room failed: {message}");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Join room failed: {message}");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon master server.");
    }
}