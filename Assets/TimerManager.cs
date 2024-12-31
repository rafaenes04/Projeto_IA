using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class TimerManager : MonoBehaviourPunCallbacks
{
    public Text countdownText; 
    public Text gameTimerText;
    public Button startButton;

    private float countdownDuration = 5f;
    private float gameTimer = 0f;
    private bool countdownStarted = false;
    private bool gameStarted = false;

    void Start()
    {
        // Show the start button only to the room owner
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.gameObject.SetActive(true);
            startButton.onClick.AddListener(StartCountdown);
        }
        else
        {
            startButton.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (countdownStarted && countdownDuration > 0)
        {
            // Countdown logic
            countdownDuration -= Time.deltaTime;
            countdownText.text = $"Starting in: {Mathf.Ceil(countdownDuration)}";
        }
        else if (countdownStarted && !gameStarted)
        {
            // End countdown
            countdownStarted = false;
            gameStarted = true;
            photonView.RPC("EnablePlayerMovement", RpcTarget.All); // Enable movement for all players
        }

        if (gameStarted)
        {
            // Game timer logic
            startButton.gameObject.SetActive(false);
            gameTimer += Time.deltaTime;
            gameTimerText.text = $"Time: {gameTimer:F2}";
        }
    }

    public void StartCountdown()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("Only the Master Client can start the countdown.");
            return;
        }

        if (photonView == null)
        {
            Debug.LogError("PhotonView is not assigned.");
            return;
        }

        photonView.RPC("SyncCountdownStart", RpcTarget.All, countdownDuration);
    }

    [PunRPC]
    void SyncCountdownStart(float duration)
    {
        countdownDuration = duration;
        countdownStarted = true;
        countdownText.gameObject.SetActive(true);
    }

    [PunRPC]
    void EnablePlayerMovement()
    {
        foreach (var player in FindObjectsOfType<playercontroller>()) // Replace PlayerController with your script
        {
            player.EnableMovement();
        }
        countdownText.gameObject.SetActive(false); // Hide countdown text
    }
}

