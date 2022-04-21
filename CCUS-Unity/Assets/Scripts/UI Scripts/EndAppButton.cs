using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAppButton : MonoBehaviour
{
    GameManager gm = GameManager.GM;
    public void CloseCCUSApplication()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (gm == null)
        {
            gm = GameManager.GM;
        }
    }
    public void ReturnToMenu()
    {
        gm.resetSimData();
        gm.ChangeState("Lobby");
    }
}
