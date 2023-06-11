using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterMelonCollision : MonoBehaviour
{
    public UnityEvent<bool, Vector3> onShouldMove;
    private string playerName = "Cube (1)";

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == playerName)
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
}
