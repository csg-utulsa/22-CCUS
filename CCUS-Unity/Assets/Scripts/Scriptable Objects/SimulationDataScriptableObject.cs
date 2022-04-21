using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SimulationDataScriptableObject", menuName = "ScriptableObjects/Simulation Manager")]
public class SimulationDataScriptableObject : ScriptableObject
{
    //Default values for inspector
    [Header("Set in Inspector")]
    public int startingPercentageCCUS = 0;
    public int defaultYear = 2022;
    public float secondsPerYear = 1f;

    [Header("Set in Inspector (in trillions)")]
    public float startingMoney = 10f;
    public float annualBudget = 1f;

    [Header("Set in Inspector (in dollars)")]
    public float costPerTonCarbonRemoved = 230f;

    [Header("Set in Inspector (in PPM)")]
    public float startingPPM = 417.5f;
    public float naturalCarbonEmissions = 7.5f;
    public float industryCarbonEmissions = 2.5f;
    public float naturalCarbonSink = 7.5f;
    public float hundredPercentCCUS_PPM = 5f;


    //Dynamic Variables
    public int year;
    public int percentageCCUS;
    public float currentPPM;
    public float annualIncrease;
    public float netZeroPPM;
    public int percentageForNeutral;

    public float currentMoney;
    public float costToRemovePPM;
    public float costForCarbonNeutral;
    public float costOfMaxCCUS;
    public float annualCostOfCCUS;
    public float currentTemperature;
    public bool environmentSaved;

    public SimulationDataScriptableObject()
    {
        //Initialize dynamic variables
        year = defaultYear;
        percentageCCUS = startingPercentageCCUS;
        currentPPM = startingPPM;
        annualIncrease = 0f;
        netZeroPPM = 0f;
        percentageForNeutral = 0;
        currentMoney = startingMoney;
        costToRemovePPM = 0f;
        costForCarbonNeutral = 0f;
        costOfMaxCCUS = 0f;
        annualCostOfCCUS = 0f;
        currentTemperature = 91.0f;
        environmentSaved = true;
    }
}
