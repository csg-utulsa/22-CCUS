/**********
 * Created by: Coleton Wheeler
 * Created on: 2/22/22
 * 
 * Last edited by: N/A
 * Last edited on: N/A
 * 
 * Description: Allows dynamic filling of a UI image, specifically the one contained inside of the CarbonBar parent object.
 * To reference this from another script, add "[SerializeField] private BarHandler bh;" and set the parent object of CarbonBarHandler.cs in the inspector.
 * To update values from another script, do dbh.setFill(fillTotal), dbh.setFillBounds(min, max), etc.
 *****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarHandler : MonoBehaviour
{

    private SimulationDataScriptableObject simulationStats;


    [Header("Set in Inspector")]
    public Image CarbonBarFill;
    [SerializeField] private GameObject CurrentTemperatureText;

    [Header("Set in Inspector or Dynamically")]
    [SerializeField] private float _maxFillAmount = 100;
    [SerializeField] private float _minFillAmount = 0;

    [Header("Set for Sacramento, CA Temperature")]
    [SerializeField] private float StartingTemp = 91.0f;

    [SerializeField] private static float LowestTemp = 90.0f;
    [SerializeField] private static float HighestTemp = 94.6f;

    [Header("Do Not Touch, dynamic temperature")]
    [SerializeField] private float CurrentTemp;

    private Text curTempText;

    //Allows other scripts to adjust the fill amount.
    public void setFill(float fillTotal)
    {
        //Clamps the sent fill value between the minimum and maximum value. Useful for unintentional inputs (i think, unity might do this automatically)
        CarbonBarFill.fillAmount = Mathf.Clamp(fillTotal / _maxFillAmount, _minFillAmount, _maxFillAmount);
    }

    //Allows other scripts to set the minimum and maximum amount of carbon units for the bar to fill.
    public void setFillBounds(float newMin, float newMax)
    {
        _minFillAmount = newMin;
        _maxFillAmount = newMax;
    }

    //Getters for min and max values
    public float getFillMax()
    {
        return _maxFillAmount;
    }

    public float getFillMin()
    {
        return _minFillAmount;
    }

    //Resets the fill total to whatever the max value is set as.
    public void ResetBar()
    {
        setFill(_maxFillAmount);
    }
    //Allows a ResetBar() call with a new min/max setting
    public void ResetBar(float min, float max)
    {
        setFillBounds(min, max);
        setFill(_maxFillAmount);
    }

    //Sets values on start
    private void Start()
    {
        simulationStats = GameObject.Find("SimulationManager").GetComponent<GameSimulationState>().simulationStats;
        curTempText = CurrentTemperatureText.GetComponent<Text>();
        Debug.Log(simulationStats.currentPPM);
        ResetBar();
        CurrentTemp = StartingTemp;
        float percentageTempFill = ((StartingTemp - LowestTemp)/(HighestTemp - LowestTemp)) * 100;
        setFill(percentageTempFill);
    }

    // 90 F is as low as it will go

    // 
    private void Update()
    {
        float changePPM = simulationStats.currentPPM - simulationStats.defaultPPM;
        //Debug.Log(changePPM);
        float changeTemp = changePPM / 91.666667f; // conversion in Fahrenheit 1c = 1.8 f
        CurrentTemp = changeTemp + StartingTemp;
        //Debug.Log(CurrentTemp);

        float percentageTempFill = ((CurrentTemp - LowestTemp) / (HighestTemp - LowestTemp)) * 100;
        setFill(percentageTempFill);

        curTempText.text = "Current Temp: " + ((float)Mathf.Round(CurrentTemp * 100f) / 100f).ToString();
    }
}
