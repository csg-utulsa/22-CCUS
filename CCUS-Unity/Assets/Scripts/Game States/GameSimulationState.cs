/**********
 * Created by: Coleton Wheeler
 * Created on: 3/1/22
 * 
 * Last edited by: Coleton Wheeler
 * Last edited on: 3/3/22
 * 
 * Description: Class to handle startup and update() handling while in the Simulation State
 *****/

using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.Events;
using System;

/* ------------ FORMULAS USED ------------- */
//[A] Calculating the anual increase of air CCUS -> AnnualIncrease = (natural emissions + industry emissions) - ((ppm value of 100% CCUS / 100) * current percentage CCUS) - (natural carbon sink)
//[B] Calculating the amount of CO2 PPM needed to remove to achieve a net zero carbon emissions -> NetZeroPPM = (natural + industry) - (natural carbon sink)
//[C] Calculating the percentage of CCUS (based on some set max 100% value) to achieve net zero carbon emissions -> Percentage needed = (NetZeroPPM / MaxPPM of CCUS) * 100


public class GameSimulationState : GameBaseState
{
    GameManager GM = GameManager.instance;
    public static SimulationDataScriptableObject simulationStats;

    private float timeSinceYearUpdated = 0;
    public override void EnterState()
    {
        simulationStats = ScriptableObject.CreateInstance<SimulationDataScriptableObject>();

        //Run scriptable object setup

        Debug.Log("Entering Simulation State");
        //Sets active scene to scene found by build index (not great, but for current build it works)
        EditorSceneManager.LoadScene(2);
        simulationStats.SetupObject();
    }

    public override void UpdateState()
    {
        timeSinceYearUpdated += Time.deltaTime;

        //If year seconds interval has passed
        if (timeSinceYearUpdated >= simulationStats.secondsPerYear)
        {
            simulationStats.year += 1;
            timeSinceYearUpdated = 0;
        }

        UpdateCarbonInformation();
        UpdateMoneyInformation();
    }

    

    private void UpdateCarbonInformation()
    {
        //Calculates the Annual Increase of CO2 PPM based on factors ---- Formula [A]
        simulationStats.annualIncrease = (simulationStats.naturalCarbonEmissions + simulationStats.industryCarbonEmissions) -
            ((simulationStats.hundredPercentCCUS_PPM / 100f) * simulationStats.percentageCCUS) - simulationStats.naturalCarbonSink;

        float increaseAmount = (Time.deltaTime / simulationStats.secondsPerYear) * simulationStats.annualIncrease;
        simulationStats.IncreaseCarbonAmount(increaseAmount);

        //Calculates the neccessary PPM removal for net neutral CO2 emissions ---- Formula [B]
        simulationStats.netZeroPPM = (simulationStats.naturalCarbonEmissions + simulationStats.industryCarbonEmissions) - simulationStats.naturalCarbonSink;
        //Calculates the necessary percentage of CCUS for net neutral CO2 emissions ---- Formula [C]
        simulationStats.percentageForNeutral = (int)((simulationStats.netZeroPPM / simulationStats.hundredPercentCCUS_PPM) * 100);
    }

    private void UpdateMoneyInformation()
    {

    }

}
