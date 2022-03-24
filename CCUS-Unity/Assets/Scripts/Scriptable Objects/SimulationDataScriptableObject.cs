using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SimulationDataScriptableObject", menuName = "ScriptableObjects/Simulation Manager")]
public class SimulationDataScriptableObject : ScriptableObject
{
    [Header("Set in Inspector")]
    public int percentageCCUS = 0;
    public int year = 2022;
    public int secondsPerYear = 3;

    [Header("Set in Inspector (PPM values)")]
    public float defaultPPM = 417.5f;
    public float naturalCarbonEmissions = 7.5f;
    public float industryCarbonEmissions = 2.5f;
    public float naturalCarbonSink = 7.5f;
    public float hundredPercentCCUS_PPM = 5f;

    [Header("Dynamic Variables (do not touch)")]
    public float currentPPM;
    public float annualIncrease;
    public float netZeroPPM;
    public int percentageForNeutral;


    public void SetupObject()
    {
        //Initialize dynamic values
        currentPPM = defaultPPM;

    }
    public void IncreaseCarbonAmount(float ppm)
    {
        currentPPM += ppm;
    }
}
