using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform player; // Player transform
    public float rotationSpeed = 20f; // Speed of the camera rotation

    private Vector3 offset;

    void Start()
    {
        // Set initial offset relative to the player
        offset = transform.position - player.position;
    }

    void Update()
    {
        RotateAroundPlayer();
    }

    void RotateAroundPlayer()
    {
        // Rotate the camera around the player on the Y-axis
        transform.position = player.position + offset;
        transform.RotateAround(player.position, Vector3.up, rotationSpeed * Time.deltaTime);

        // Make sure the camera is always looking at the player
        transform.LookAt(player);
    }
}