using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterMelonCollision : MonoBehaviour
{
    public UnityEvent<bool, Vector3> onShouldMove;

    private void OnTriggerEnter(Collider other)
    {
        onShouldMove.Invoke(true, other.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        onShouldMove.Invoke(false, Vector3.zero);
    }
}
