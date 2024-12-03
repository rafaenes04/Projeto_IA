using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    [SerializeField] CharacterController characterController;

    private float speed = 3f;

    private Vector3 moveDirection;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection.Normalize(); // Ensure consistent speed in diagonal movement

        // Move the character
        characterController.Move(moveDirection * speed * Time.deltaTime);
    }


}
