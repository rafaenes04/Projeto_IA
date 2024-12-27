using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Animator animator;

    public float speed = 5f;
    private Vector3 moveDirection;
    private bool movementEnabled = true;

    void Update()
    {
        if (!movementEnabled)
        {
            // Exit update early if movement is disabled
            animator.SetBool("isRunning", false);
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        moveDirection = new Vector3(horizontal, 0, vertical);
        
        if (moveDirection.magnitude > 0)
        {
            // Normalize to maintain consistent movement speed
            moveDirection.Normalize();

            // Rotate the player to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Move the character
        characterController.Move(moveDirection * speed * Time.deltaTime);
        bool isMoving = moveDirection.magnitude > 0; // Check if there's any movement
        animator.SetBool("isRunning", isMoving);
    }
    public void SetMovementEnabled(bool enabled)
    {
        movementEnabled = enabled;
        if (!enabled)
        {
            // Stop any residual velocity when movement is disabled
            moveDirection = Vector3.zero;
        }
    }

    public void SetPlayerPosition(Vector3 position)
    {
        // Teleport the player to a specific position
        if (characterController != null)
        {
            characterController.enabled = false; // Disable to avoid conflict with physics
            transform.position = position;
            transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 180f, 0f);
            characterController.enabled = true;

        }
    }

}
