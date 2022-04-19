using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    GameManager gm;

    private void Start()
    {
        gm = GameManager.GM;
        if (gm == null)
        {
            Debug.LogWarning("GameManager not found on MainMenu.cs");
        }
    }
    public void StartGame()
    {
        gm.ChangeState("Simulation");
    }
}
