using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalKeyHole : MonoBehaviour
{
    public GameObject ConnectedDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;


            // trigger the opening door
            ConnectedDoor.GetComponent<DoorSwitch>().DoorOpen = true;

            // make key go away!
            other.gameObject.SetActive(false);

        }
    }
}
