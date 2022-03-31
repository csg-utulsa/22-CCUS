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
    GameManager GM = GameManager.instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
