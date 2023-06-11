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

    private Quaternion currentRotation;
    private Quaternion desiredRotation;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        waterMelonCollision = GetComponent<WaterMelonCollision>();       
        
        waterMelonCollision.onShouldMove.AddListener(ShouldMove);

        currentRotation = transform.rotation;
    }

    void Update()
    {
        Move();

        if (isMoving)
        {
            float rotationSpeed = 5.0f; // control the speed of rotation
            currentRotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * rotationSpeed);
            transform.rotation = currentRotation;
        }
    }

    void Jump()
    {
        //squash here
        squash.Invoke();

        isJumping = true;

        Vector3 jumpDirection = transform.position - playerLocation;
        jumpDirection.y = 0;
        jumpDirection.Normalize();

        desiredRotation = Quaternion.LookRotation(-jumpDirection);

        // Apply the jump forces
        rb.AddForce(jumpDirection * forwardForce, ForceMode.VelocityChange); 
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
