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
