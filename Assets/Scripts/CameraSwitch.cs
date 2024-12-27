using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera floor1Camera;
    public Cinemachine.CinemachineVirtualCamera floor2Camera;
    public BoxCollider switchTrigger;
    public List<Vector3> playerPositions;
    private int currentPositionIndex = 0;
    private playercontroller playerController;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playercontroller playerController = other.GetComponent<playercontroller>();

            if (playerController != null && currentPositionIndex < playerPositions.Count)
            {
                // Lock the triggering player to a specific position
                playerController.SetMovementEnabled(false);
                playerController.SetPlayerPosition(playerPositions[currentPositionIndex]);
                currentPositionIndex++;

                // Switch camera after locking the player
                SwitchCamera();
            }
            else if (currentPositionIndex >= playerPositions.Count)
            {
                Debug.LogWarning("No more positions available for players!");
            }
        }
    }

    void SwitchCamera()
    {
        floor1Camera.Priority = 0;
        floor2Camera.Priority = 10;
    }
   
}