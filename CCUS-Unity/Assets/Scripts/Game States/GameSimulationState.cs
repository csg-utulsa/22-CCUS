/**********
 * Created by: Coleton Wheeler
 * Created on: 3/1/22
 * 
 * Last edited by: Coleton Wheeler
 * Last edited on: 3/31/22
 * 
 * Description: Class to handle the game during the Simulation State
 *****/

using UnityEngine;
using UnityEditor.SceneManagement;
using System;

/* ------------ CCUS FORMULAS USED ------------- */
//[A] Calculating the anual increase of air CCUS -> AnnualIncrease = (natural emissions + industry emissions) - ((ppm value of 100% CCUS / 100) * current percentage CCUS) - (natural carbon sink)
//[B] Calculating the amount of CO2 PPM needed to remove to achieve a net zero carbon emissions -> NetZeroPPM = (natural + industry) - (natural carbon sink)
//[C] Calculating the percentage of CCUS (based on some set max 100% value) to achieve net zero carbon emissions -> Percentage needed = (NetZeroPPM / MaxPPM of CCUS) * 100

/* ------------ MONEY FORMULAS USED ------------- */
//[D] Calculates how much money it costs to remove 1 annual PPM per year -> CostRemovalPer1PPM = 4.526 billion * CostPerTonCarbonRemoved
//[E] Calculates the annual cost for neutral carbon emissions -> CarbonNeutralPPMCost = CostRemovalPer1PPM * NetZeroPPM
//[F] Calculates how much annually it would cost to max out your CCUS -> 100% CCUS = 5PPM Annual Removal = CostRemovalPer1PPM * 5
//[] Description -> CCUS cost = (1 - CCUS% / 100) * (100% CCUS)


public class GameSimulationState : GameBaseState
{
    GameManager gm;
    SimulationDataScriptableObject simulationData;

    private float timeSinceYearUpdated = 0;

    public void Start()
    {
        gm = GameManager.GM;
        simulationData = gm.simData;
        Debug.Log(simulationData.year);
    }
    public override void EnterState()
    {
        Debug.Log("Entering Simulation State");

        //Sets active scene to scene found by build index (not great, but for current build it works)
        if (EditorSceneManager.GetActiveScene().name != "SimulationScene")
            EditorSceneManager.LoadScene("SimulationScene");
    }

    public override void UpdateState()
    {
        Debug.Log(simulationData.currentPPM);
        timeSinceYearUpdated += Time.deltaTime;

        //If year seconds interval has passed
        if (timeSinceYearUpdated >= simulationData.secondsPerYear)
        {
            simulationData.year += 1;
            timeSinceYearUpdated = 0;
        }

        UpdateCarbonInformation();
        UpdateMoneyInformation();
    }

    

    private void UpdateCarbonInformation()
    {
        //Calculates the Annual Increase of CO2 PPM based on factors ---- Formula [A]
        simulationData.annualIncrease = (simulationData.naturalCarbonEmissions + simulationData.industryCarbonEmissions) -
            ((simulationData.hundredPercentCCUS_PPM / 100f) * simulationData.percentageCCUS) - simulationData.naturalCarbonSink;

        float increaseAmount = (Time.deltaTime / simulationData.secondsPerYear) * simulationData.annualIncrease;
        simulationData.currentPPM += increaseAmount;

        //Calculates the neccessary PPM removal for net neutral CO2 emissions ---- Formula [B]
        simulationData.netZeroPPM = (simulationData.naturalCarbonEmissions + simulationData.industryCarbonEmissions) - simulationData.naturalCarbonSink;
        //Calculates the necessary percentage of CCUS for net neutral CO2 emissions ---- Formula [C]
        simulationData.percentageForNeutral = (int)((simulationData.netZeroPPM / simulationData.hundredPercentCCUS_PPM) * 100);

        Debug.Log(simulationData.currentPPM);
    }

    private void UpdateMoneyInformation()
    {

    }

    private float roundToTwoDecimalTrillion(float unrounded)
    {
        float inTermsOfTrillion = unrounded / Mathf.Pow(10, 12);
        int tempInt = (int)(inTermsOfTrillion * 100f);
        return tempInt / 100f;
    }

}
