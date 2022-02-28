/**********
 * Created by: Coleton Wheeler
 * Created on: 2/23/22
 * 
 * Last edited by: Coleton Wheeler
 * Last edited on: 2/24/22
 * 
 * Description: 
 *****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyHandler : MonoBehaviour
{
    /* ------------ FORMULAS USED ------------- */
    //[A] Calculates how much money it costs to remove 1 annual PPM per year -> CostRemovalPer1PPM = 4.526 billion * CostPerTonCarbonRemoved
    //[B] Calculates the annual cost for neutral carbon emissions -> CarbonNeutralPPMCost = CostRemovalPer1PPM * NetZeroPPM
    //[C] Calculates how much annually it would cost to max out your CCUS -> 100% CCUS = 5PPM Annual Removal = CostRemovalPer1PPM * 5
    //[x] Description -> CCUS cost = (1 - CCUS% / 100) * (100% CCUS)


    [Header("Set in Inspector (in trillions)")]
    public CarbonDataHandler cdh;
    [SerializeField] private float _startingMoney;

    [Header("Set in Inspector (in dollars)")]
    [SerializeField] private float _costPerTonCarbonRemoved;

    private float _currentMoney;
    private float _costToRemovePerPPM;
    private float _costForCarbonNeutral;
    private float _costOfMaxCCUS;
    private float _annualCostOfCCUS;


    void Start()
    {
        _currentMoney = _startingMoney;
    }

    private void Update()
    {
        _costToRemovePerPPM = (4.526f * Mathf.Pow(10, 9)) * _costPerTonCarbonRemoved; // Formula [A]
        _costForCarbonNeutral = _costToRemovePerPPM * cdh.getCarbonNeutralPPM(); // Formula [B]
        _costOfMaxCCUS = cdh.getMaxPPMCCUS() * _costToRemovePerPPM; // Formula [C]
        _annualCostOfCCUS = 1;
    }

    private float roundToTwoDecimalTrillion(float unrounded)
    {
        float inTermsOfTrillion = unrounded / Mathf.Pow(10, 12);
        int tempInt = (int)(inTermsOfTrillion * 100f);
        return tempInt / 100f;
    }
}
