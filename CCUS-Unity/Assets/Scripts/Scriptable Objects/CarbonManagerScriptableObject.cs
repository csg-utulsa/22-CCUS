using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CarbonManagerScriptableObject", menuName = "ScriptableObjects/Carbon Manager")]
public class CarbonManagerScriptableObject : ScriptableObject
{
    [Header("Set in Inspector")]
    public int percentageCCUS;

    [Header("Set in Inspector (PPM values)")]
    [SerializeField] private float defaultPPM;
    public float naturalCarbonEmissions;
    public float industryCarbonEmissions;
    public float naturalCarbonSink;
    public float hundredPercentCCUS_PPM;

    [Header("Dynamic Variables (do not touch)")]
    public float currentPPM;
    public float annualIncrease;
    public float netZeroPPM;
    public int percentageForNeutral;

    // Scripts subscripe to this to get notified of carbon changes
    [System.NonSerialized]
    public UnityEvent carbonChangeEvent;

    private void OnEnable()
    {
        //Initialize dynamic values
        currentPPM = defaultPPM;
        UpdateDyanmicVariables();

        if (carbonChangeEvent == null)
            carbonChangeEvent = new UnityEvent();
    }
    public void IncreaseCarbonAmount(float ppm)
    {
        currentPPM += ppm;
        UpdateDyanmicVariables();
        carbonChangeEvent.Invoke();
    }

    private void UpdateDyanmicVariables()
    {
        annualIncrease = (naturalCarbonEmissions + industryCarbonEmissions) - ((hundredPercentCCUS_PPM / 100f) * percentageCCUS) - (naturalCarbonSink);
        netZeroPPM = (naturalCarbonEmissions + industryCarbonEmissions) - naturalCarbonSink;
        percentageForNeutral = (int)((netZeroPPM / hundredPercentCCUS_PPM) * 100);
    }
}
