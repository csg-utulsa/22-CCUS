using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PhysicalButtonScript : MonoBehaviour
{
    GameManager gm = GameManager.GM;

    private void Start ()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.resetSimData();
            gm.ChangeState(GameState.Simulation);
        }
    }
}
