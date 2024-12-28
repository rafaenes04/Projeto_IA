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
    public Button startGameButton;

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

        playerNameInput.text = playerName;
        PhotonNetwork.NickName = playerName;

        startGameButton.onClick.AddListener(StartGame);
        startGameButton.gameObject.SetActive(false);
    }

    private void UpdatePlayerName(string newName)
    {
        if (!string.IsNullOrEmpty(newName))
        {
            playerName = newName; 
            PhotonNetwork.NickName = playerName;
            Debug.Log($"Player name updated to: {playerName}");
        }
        else
        {
            Debug.LogWarning("Player name cannot be empty!");
        }
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

        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 4 }; 
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
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            { "SkinIndex", selectedSkinIndex },
            { "PlayerName", playerName }
        });

        Debug.Log($"Player {playerName} is ready with skin {selectedSkinIndex}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Joined party: {PhotonNetwork.CurrentRoom.Name}");

        // Enable the start button for the master client
        if (PhotonNetwork.IsMasterClient)
        {
            startGameButton.gameObject.SetActive(true);
        }
    }
    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Switch to the game scene
            PhotonNetwork.LoadLevel("GabrielSampleScene"); // Replace "GameScene" with your actual scene name
        }
        else
        {
            Debug.LogWarning("Only the master client can start the game!");
        }
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