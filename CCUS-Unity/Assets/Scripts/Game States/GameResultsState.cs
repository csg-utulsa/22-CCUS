using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultsState : GameBaseState
{
    // Start is called before the first frame update
    public override void EnterState()
    {
        Debug.Log("Entering Lobby State");
        if (SceneManager.GetActiveScene().name != "ResultsScene")
            SceneManager.LoadScene("ResultsScene");
    }


    public override void UpdateState()
    {

    }
}
