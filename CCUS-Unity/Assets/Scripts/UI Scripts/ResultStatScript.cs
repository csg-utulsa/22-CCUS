using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultStatScript : MonoBehaviour
{
    public TMP_Text ppmStat;
    public TMP_Text tempStat;
    public TMP_Text moneyStat;
    public TMP_Text envStat;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GM;

        ppmStat.text = "Ending PPM: " + gm.simData.currentPPM;

        tempStat.text = "Ending Temperature (F): " + gm.simData.currentTemperature;

        moneyStat.text = "Ending Money: " + gm.simData.currentMoney + "Trillion";

        if(gm.simData.environmentSaved)
        {
            envStat.text = "You Saved the Environment!";
        } else
        {
            envStat.text = "It got too hot for the Environment.";
        }

    }

}
