using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicalButtonScript : MonoBehaviour
{
    public UnityEvent ButtonTriggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerEvent"))
        {
            // do an event
            Debug.Log("Button Event has been triggered!");
            ButtonTriggerEvent.Invoke();
            
            
        }
    }
}
