using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class CameraManager : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cameraPool; // Pre-configured Cinemachine cameras
    private Dictionary<PhotonView, CinemachineVirtualCamera> playerCameraMap = new Dictionary<PhotonView, CinemachineVirtualCamera>();

    void Start()
    {
        AssignLocalPlayerCamera();
    }

    private void AssignLocalPlayerCamera()
    {
        // Find all players in the scene
        foreach (var player in FindObjectsOfType<playercontroller>()) // Replace PlayerController with your player script
        {
            PhotonView photonView = player.GetComponent<PhotonView>();
            if (photonView != null && photonView.IsMine)
            {
                AssignCameraToPlayer(player.transform, photonView);
                break;
            }
        }
    }

    private void AssignCameraToPlayer(Transform playerTransform, PhotonView photonView)
    {
        // Check if the player already has a camera assigned
        if (playerCameraMap.ContainsKey(photonView))
        {
            Debug.LogWarning("Camera already assigned to this player.");
            return;
        }

        // Find an available camera from the pool
        foreach (var cam in cameraPool)
        {
            if (cam.Follow == null) // Camera is unassigned
            {
                cam.Follow = playerTransform; // Set the camera's Follow target
                playerCameraMap[photonView] = cam; // Map the player to the camera
                Debug.Log($"Camera assigned to player: {photonView.Owner.NickName}");
                return;
            }
        }

        Debug.LogError("No available cameras in the pool to assign!");
    }

    public void ReleaseCamera(PhotonView photonView)
    {
        // If the player disconnects or leaves, release their camera
        if (playerCameraMap.TryGetValue(photonView, out var assignedCamera))
        {
            assignedCamera.Follow = null; // Release the camera
            playerCameraMap.Remove(photonView);
            Debug.Log($"Camera released for player: {photonView.Owner.NickName}");
        }
    }
}
