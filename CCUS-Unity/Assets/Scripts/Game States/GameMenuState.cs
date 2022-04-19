/**********
 * Created by: Coleton Wheeler
 * Created on: 3/1/22
 * 
 * Last edited by: N/A
 * Last edited on: N/A
 * 
 * Description: Class to handle the game during the Menu State
 *****/

using UnityEngine;
using UnityEditor.SceneManagement;

public class GameMenuState : GameBaseState
{
    public override void EnterState()
    {
        Debug.Log("Entering Menu State");
        if(EditorSceneManager.GetActiveScene().name != "MenuScene")
        {
            EditorSceneManager.LoadScene("MenuScene");
        }
    }

    public override void UpdateState()
    {

    }
}
