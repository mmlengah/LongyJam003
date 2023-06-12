using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMovement : MonoBehaviour
{
    private float speed = 2.0f;
    private float sprintSpeed = 4.0f; // Define a separate sprint speed that is double the normal speed.
    private float jumpStrength = 5.0f; // Jump strength
    private float gravity = -9.8f; // Gravity strength
    private CharacterController characterController;
    private Vector3 moveDirection;
    private float horizontalMove;
    private float verticalMove;
    private bool jump;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        // Check if jump is pressed
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 forwardMovement = transform.forward * verticalMove;
        Vector3 rightMovement = transform.right * horizontalMove;
        float currentSpeed = speed;

        // If shift key is pressed, use sprint speed
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = sprintSpeed;
        }

        // If grounded, allow jumping, else apply gravity.
        if (characterController.isGrounded)
        {
            moveDirection = (forwardMovement + rightMovement).normalized * currentSpeed;
            moveDirection.y = 0;

            if (jump)
            {
                // If jump is pressed, apply an upward force.
                moveDirection.y = jumpStrength;
                jump = false; // reset the jump variable
            }
        }
        else
        {
            // Apply gravity
            moveDirection.y += gravity * Time.fixedDeltaTime;
        }

        characterController.Move(moveDirection * Time.fixedDeltaTime);
    }
}
