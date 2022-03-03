/**********
 * Created by: Coleton Wheeler
 * Created on: 3/1/22
 * 
 * Last edited by: Coleton Wheeler
 * Last edited on: 3/1/22
 * 
 * Description: Class to handle the game during the Education State
 *****/


using UnityEngine;
using UnityEditor.SceneManagement;

public class GameEducationState : GameBaseState
{
    public override void EnterState()
    {
        Debug.Log("Entering Education State");
        //Sets active scene to scene found by build index (not great, but for current build it works)
        EditorSceneManager.LoadScene(1);
    }

    public override void UpdateState()
    {

    }
}
