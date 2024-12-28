using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoints; 
    public string playerPrefabName = "Player";

    private void Start()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        if (spawnPoints.Length > 0)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            PhotonNetwork.Instantiate(playerPrefabName, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("No spawn points assigned in the GameManager!");
        }
    }

}