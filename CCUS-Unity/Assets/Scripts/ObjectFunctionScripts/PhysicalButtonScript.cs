using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PhysicalButtonScript : MonoBehaviour
{
    GameManager gm;

    private void Start ()
    {
        gm = GameManager.GM;
        if (gm==null) {
            Debug.LogWarning("Game Manager was not found in LobbyScene");
        } 
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.ChangeState("Simulation");
        }
    }
}
