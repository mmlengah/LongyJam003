using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaterMelonCollision))]
public class WaterMelonMovement : MonoBehaviour
{
    // Reference to WaterMelonCollision script
    private WaterMelonCollision waterMelonCollision;

    private float forwardForce = 1f;

    private float upwardForce = 1f;

    private float jumpInterval = 1f;

    private bool isMoving = false;

    private bool isJumping = false;

    private Rigidbody rb;

    private Vector3 playerLocation;

    void Start()
    {
        waterMelonCollision = GetComponent<WaterMelonCollision>();
        rb = GetComponent<Rigidbody>();
        waterMelonCollision.onShouldMove.AddListener(ShouldMove);
    }

    void Update()
    {
        Move();
    }

    IEnumerator Jump()
    {
        isJumping = true;

        Vector3 jumpDirection = transform.position - playerLocation;
        jumpDirection.Normalize();

        // Apply the jump forces
        rb.AddForce(jumpDirection * forwardForce, ForceMode.VelocityChange);
        rb.AddForce(transform.up * upwardForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(jumpInterval);

        isJumping = false;
    }

    private void Move()
    {
        if (!isMoving || isJumping) return;
        StartCoroutine(Jump());
    }

    private void ShouldMove(bool b, Vector3 p)
    {
        isMoving = b;
        playerLocation = p;
    }
}
