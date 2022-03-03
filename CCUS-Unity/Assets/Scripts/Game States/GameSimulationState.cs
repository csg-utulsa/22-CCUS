/**********
 * Created by: Coleton Wheeler
 * Created on: 3/1/22
 * 
 * Last edited by: N/A
 * Last edited on: N/A
 * 
 * Description: Class to handle the game during the Simulation State
 *****/

using UnityEngine;
using UnityEditor.SceneManagement;

public class GameSimulationState : GameBaseState
{
    [SerializeField] private BasicInfoScriptableObject basicInfo;
    public override void EnterState()
    {
        Debug.Log("Entering Simulation State");
        //Sets active scene to scene found by build index (not great, but for current build it works)
        EditorSceneManager.LoadScene(2);
    }

    public override void UpdateState()
    {
        
    }
}
