/**********
 * Created by: Coleton Wheeler
 * Created on: 3/1/22
 * 
 * Last edited by: Ben Reyes
 * Last edited on: 4/21/22 <- added wildfire value
 * 
 * Description: Class to handle the game during the Simulation State
 *****/

using UnityEngine;
using System;
using UnityEngine.SceneManagement;


/* ------------ CCUS FORMULAS USED ------------- */
//[A] Calculating the anual increase of air CCUS -> AnnualIncrease = (natural emissions + industry emissions) - ((ppm value of 100% CCUS / 100) * current percentage CCUS) - (natural carbon sink)
//[B] Calculating the amount of CO2 PPM needed to remove to achieve a net zero carbon emissions -> NetZeroPPM = (natural + industry) - (natural carbon sink)
//[C] Calculating the percentage of CCUS (based on some set max 100% value) to achieve net zero carbon emissions -> Percentage needed = (NetZeroPPM / MaxPPM of CCUS) * 100

/* ------------ MONEY FORMULAS USED ------------- */
//[D] Calculates how much money it costs to remove 1 annual PPM per year -> CostRemovalPer1PPM = 4.526 billion * CostPerTonCarbonRemoved
//[E] Calculates the annual cost for neutral carbon emissions -> CarbonNeutralPPMCost = CostRemovalPer1PPM * NetZeroPPM
//[F] Calculates how much annually it would cost to max out your CCUS -> 100% CCUS = 5PPM Annual Removal = CostRemovalPer1PPM * 5
//[G] Description -> CCUS cost = (1 - CCUS% / 100) * (100% CCUS)


public class GameSimulationState : GameBaseState
{
    GameManager gm;
    SimulationDataScriptableObject simulationData;

    private float timeSinceYearUpdated = 0;

    public void Start()
    {
        gm = GameManager.GM;
        if (gm.simData != null)
            simulationData = gm.simData;
    }
    public override void EnterState()
    {
        Debug.Log("Entering Simulation State");

        //Sets active scene to scene found by build name (not great, but for current build it works)
        if (SceneManager.GetActiveScene().name != "SimulationScene")
            SceneManager.LoadScene("SimulationScene");
    }

    public override void UpdateState()
    {
        if (simulationData == null)
        {
            Debug.LogWarning("Simulation data is null");
            simulationData = gm.simData;
            return;
        }

        timeSinceYearUpdated += Time.deltaTime;

        //If year seconds interval has passed
        if (timeSinceYearUpdated >= simulationData.secondsPerYear)
        {
            simulationData.year += 1;
            timeSinceYearUpdated = 0;
        }

        UpdateCarbonInformation();
        UpdateMoneyInformation();

        // Check: Should Game End?
        // IF yes (at year 2222 ? ) switch to result screen!
        if(simulationData.year > 2222)
        {
            // switch scenes to the result page!
            gm.ChangeState("Results");
        }
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

        float fogThickness = (simulationData.currentPPM - simulationData.startingPPM) / 327.5f;
        if (fogThickness < 0 )
        {
            fogThickness = 0;
        }
        // Create fog and orange color for "Wildfire" in script < MAX PPM is around 745 PPM. MIN is Starting ppm - 417.5
        RenderSettings.fogColor = Color.red;

        RenderSettings.fogDensity = 0.08f * fogThickness;

    }

    private void UpdateMoneyInformation()
    {
        //Updates player current money with annual increase * time passed through year
        simulationData.currentMoney += (simulationData.annualBudget - simulationData.annualCostOfCCUS) * (Time.deltaTime / simulationData.secondsPerYear);
        Debug.Log(simulationData.currentMoney);

        //Calculates the cost to remove 1PPM of CO2 per year ---- Formula [D]
        simulationData.costToRemovePPM = 0.004526f * simulationData.costPerTonCarbonRemoved;

        //Calculates the cost required annually for carbon neutral emissions ---- Formula [E]
        simulationData.costForCarbonNeutral = simulationData.costToRemovePPM * simulationData.netZeroPPM;

        //Calculates how much it would cost annually to max out your CCUS percentage ---- Formula [F]
        simulationData.costOfMaxCCUS = simulationData.costToRemovePPM * simulationData.hundredPercentCCUS_PPM;

        //Calculates the annual cost you're spending on CCUS ---- Formula [G]
        simulationData.annualCostOfCCUS = (simulationData.percentageCCUS / 100f) * (simulationData.costOfMaxCCUS);
    }

    private float roundToTwoDecimalTrillion(float unrounded)
    {
        float inTermsOfTrillion = unrounded / Mathf.Pow(10, 12);
        int tempInt = (int)(inTermsOfTrillion * 100f);
        return tempInt / 100f;
    }

}
