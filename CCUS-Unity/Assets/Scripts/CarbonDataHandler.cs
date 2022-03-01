/**********
 * Created by: Coleton Wheeler
 * Created on: 2/22/22
 * 
 * Last edited by: Coleton Wheeler
 * Last edited on: 2/24/22
 * 
 * Description: Manages background carbon emissions calculations.
 * Stores the value of current CCUS percentage, as well as other factors such as the year and current CO2 PPM
 *****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**********
 * Things to do:
 * Annual PPM increase does not account for differently set max PPM CCUS percentage
 *****/


public class CarbonDataHandler : MonoBehaviour
{
    /* ------------ FORMULAS USED ------------- */
    //[A] Calculating the gross annual CO2 PPM increase -> Gross Annual Increase = (Natural - Industry) + (2 * industry) * (1 - CCUS%)
    //[B] Calculating the net annual CO2 PPM increase accounting for natural carbon sinks -> Net Annual Increase = Annual Increase - Natural Carbon Sink
    //[C] Calculating the amount of CO2 PPM needed to remove to achieve a net zero carbon emissions -> NetZeroPPM = (natural + industry) - (natural carbon sink)
    //[D] Calculating the percentage of CCUS (based on some set max 100% value) to achieve net zero carbon emissions -> Percentage needed = (NetZeroPPM / MaxPPM of CCUS) * 100


    [Header("Set in Inspector")]
    [SerializeField] private int _secsBetweenYears;
    [SerializeField] private int _year;
    [SerializeField] private int _percentageCCUS;

    [Header("Set in Inspector (Values measured in PPM)")]
    [SerializeField] private float _defaultPPM;
    [SerializeField] private float _naturalCarbonEmissions;
    [SerializeField] private float _industryCarbonEmissions;
    [SerializeField] private float _naturalCarbonSink;
    [SerializeField] private float _hundredPercentCCUS_PPM;

    private float _netZeroPPM; 
    private float _currentPPM;
    private float _timeSinceYearUpdated = 0;
    private float _grossAnnualIncrease;
    private int _percentageForNeutral;


    private void Start()
    {
        _percentageCCUS = 0; //Initialize percentage to 0%
        _currentPPM = _defaultPPM; //Set the current PPM to what the default PPM is set to
    }

    private void Update()
    {
        _timeSinceYearUpdated += Time.deltaTime;
        _netZeroPPM = (_naturalCarbonEmissions + _industryCarbonEmissions) - _naturalCarbonSink; // Formula [C]
        _percentageForNeutral = (int)((_netZeroPPM / _hundredPercentCCUS_PPM) * 100); // Formula [D]
        float amtRemoved = _hundredPercentCCUS_PPM / 100f;

        //If year seconds interval has passed
        if (_timeSinceYearUpdated >= _secsBetweenYears)
        {
            _year += 1;
            _timeSinceYearUpdated = 0;
        }

        _grossAnnualIncrease = (_naturalCarbonEmissions + _industryCarbonEmissions) - (amtRemoved * _percentageCCUS) - (_naturalCarbonSink); // Formula [A]
        

        //[Time since last update] / [Seconds between years] is a factor to multiply the annual CO2 impact by the factor of how much time of that year has passed. ex: Time.detlaTime = 0.02 seconds, 0.02/15
       _currentPPM += (Time.deltaTime / _secsBetweenYears) * ((_naturalCarbonEmissions + _industryCarbonEmissions) - (amtRemoved * _percentageCCUS) - (_naturalCarbonSink));
    }

    /******
    ** GETTERS AND SETTERS -------------------------------------------------------------------------------------------------------|
    ******/

    /*Basic info getters*/
    public int getPercentageCCUS()
    {
        return _percentageCCUS;
    }

    public float getMaxPPMCCUS()
    {
        return _hundredPercentCCUS_PPM;
    }

    public int getCurrentYear()
    {
        return _year;
    }

    public float getCurrentPPM()
    {
        return _currentPPM;
    }

    public float getNaturalCarbonEmissions()
    {
        return _naturalCarbonEmissions;
    }

    public float getIndustryCarbonEmissions()
    {
        return _industryCarbonEmissions;
    }

    public float getNaturalCarbonSink()
    {
        return _naturalCarbonSink;
    }
    
    public float getAnnualCarbonIncrease()
    {
        return _grossAnnualIncrease;
    }

    /*Neutral CO2 getters*/
    public float getCarbonNeutralPPM()
    {
        return _netZeroPPM;
    }

    public float getCarbonNeutralCCUS()
    {
        return _percentageForNeutral;
    }

    /*Value setters*/
    public void setPercentageCCUS(int newPercentage)
    {
        Mathf.Clamp(newPercentage, 0, 100);
        _percentageCCUS = newPercentage;
    }

    public void setPercentageCCUS(float newPercentage)
    {
        int intPercentage = (int)newPercentage;
        Mathf.Clamp(intPercentage, 0, 100);
        _percentageCCUS = intPercentage;
    }

    public void setYearInterval(int newYearInterval)
    {
        _secsBetweenYears = newYearInterval;
    }

    public void setYearInterval(float newYearInterval)
    {
        int intYearInterval = (int)newYearInterval;
        _secsBetweenYears = intYearInterval;
    }

    public void setMaxCCUS(float newMaxSink)
    {
        _hundredPercentCCUS_PPM = newMaxSink;
    }

    public void setNaturalEmissions(float newNaturalEmissions)
    {
        _naturalCarbonEmissions = newNaturalEmissions;
    }

    public void setIndustryEmissions(float newIndustryEmissions)
    {
        _industryCarbonEmissions = newIndustryEmissions;
    }

    public void setNaturalSink(float newNaturalSink)
    {
        _naturalCarbonSink = newNaturalSink;
    }
}
