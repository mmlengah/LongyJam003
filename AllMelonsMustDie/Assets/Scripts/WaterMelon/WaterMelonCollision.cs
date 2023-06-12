using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class WaterMelonCollision : MonoBehaviour
{
    public UnityEvent<bool, Vector3> onShouldMove;
    private string playerName = "Player";

    // Structure to hold Rigidbody state
    private struct RigidbodyState
    {
        public Vector3 position;
        public Quaternion rotation;
        public bool isKinematic;
    }

    // List to hold the original state
    private List<RigidbodyState> originalState = new List<RigidbodyState>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == playerName)
        {
            onShouldMove.Invoke(true, other.transform.position);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == playerName)
        {
            onShouldMove.Invoke(false, Vector3.zero);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.name == "Stick" && pAttack.IsAttacking)
        {
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        // Save original state
        foreach (Transform child in transform)
        {
            Rigidbody rigidbody = child.GetComponent<Rigidbody>();
            if (rigidbody == null) { continue; }

            BoxCollider collider = child.GetComponent<BoxCollider>();
            if (collider == null) { continue; }

            originalState.Add(new RigidbodyState
            {
                position = child.position,
                rotation = child.rotation,
                isKinematic = rigidbody.isKinematic
            });

            // Enable the Rigidbody
            rigidbody.isKinematic = false;

            collider.enabled = true;
        }

        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}

