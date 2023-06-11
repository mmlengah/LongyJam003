using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WaterMelonCollision))]
public class WaterMelonMovement : MonoBehaviour{

    private WaterMelonCollision waterMelonCollision;
    
    public UnityEvent squash;
    public UnityEvent unsquash;

    private float forwardForce = 1f;
    private float upwardForce = 3f;
    private float jumpInterval = 1f;

    private bool isMoving = false;
    private bool isJumping = false;

    private Rigidbody rb;
    private Vector3 playerLocation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        waterMelonCollision = GetComponent<WaterMelonCollision>();       
        
        waterMelonCollision.onShouldMove.AddListener(ShouldMove);
    }

    void Update()
    {
        Move();
    }

    void Jump()
    {
        //squash here
        squash.Invoke();

        isJumping = true;

        Vector3 jumpDirection = playerLocation - transform.position; // Reverse the subtraction order
        jumpDirection.Normalize();

        // Apply the jump forces
        rb.AddForce(-jumpDirection * forwardForce, ForceMode.VelocityChange); // Add a negative sign to make the enemy jump in the opposite direction
        rb.AddForce(transform.up * upwardForce, ForceMode.VelocityChange);

        StartCoroutine(WaitAndResetJump());

        //unsquash
        unsquash.Invoke();
    }


    IEnumerator WaitAndResetJump()
    {
        yield return new WaitForSeconds(jumpInterval);
        isJumping = false;
    }

    private void Move()
    {
        if (!isMoving || isJumping) return;
        Jump();
    }

    private void ShouldMove(bool b, Vector3 p)
    {
        isMoving = b;
        playerLocation = p;
    }
}
