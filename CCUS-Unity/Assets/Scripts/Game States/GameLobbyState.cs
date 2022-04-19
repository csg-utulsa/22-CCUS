/**********
 * Created by: Coleton Wheeler
 * Created on: 4/14/22
 * 
 * Last edited by: Coleton Wheeler
 * Last edited on: 4/14/22
 * 
 * Description: Class to handle the game during the Education State
 *****/


using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLobbyState : GameBaseState
{
    private bool completedEducation;
    public override void EnterState()
    {
        Debug.Log("Entering Lobby State");
        if (SceneManager.GetActiveScene().name != "LobbyScene")
            SceneManager.LoadScene("LobbyScene");
    }

    public override void UpdateState()
    {

    }
}
