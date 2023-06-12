using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pAttack : MonoBehaviour
{
    public float swingTime = 0.25f;
    public float maxSwingAngle = 45f;
    private Quaternion initialRotation;
    private static bool isAttacking;

    public Transform playerTransform;

    // Add a weight to the left vector
    public float leftWeight = 1.5f;

    public static bool IsAttacking
    {
        get { return isAttacking; }
        private set { isAttacking = value; }
    }

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(SwingStick());
        }
    }

    private IEnumerator SwingStick()
    {
        isAttacking = true;

        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;

        // Get the current forward and left vectors of the player
        Vector3 forward = playerTransform.forward;
        Vector3 left = playerTransform.right;

        // Modify the left vector by multiplying it by leftWeight
        left *= leftWeight;

        // Create a rotation that rotates maxSwingAngle degrees around the combination of forward and left vectors
        Quaternion targetRotation = Quaternion.AngleAxis(maxSwingAngle, (forward + left).normalized) * startRotation;

        while (elapsedTime < swingTime)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, (elapsedTime / swingTime));
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < swingTime)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(targetRotation, initialRotation, (elapsedTime / swingTime));
            yield return null;
        }

        isAttacking = false;
    }

}

