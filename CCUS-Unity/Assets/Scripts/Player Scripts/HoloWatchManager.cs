/**********
 * Created by: Coleton Wheeler
 * Created on: 3/29/22
 * 
 * Last edited by: Coleton Wheeler
 * Last edited on: 3/31/22
 * 
 * Description: Handles the visuals and functions of the watch GUI
 *****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloWatchManager : MonoBehaviour
{
    private GameManager GM;
    private void Awake()
    {
        GM = GameManager.instance;
    }


    public void simulationSceneButton()
    {
        GM.ChangeState("Simulation");
    }

    public void educationSceneButton()
    {
        GM.ChangeState("Education");
    }
}
