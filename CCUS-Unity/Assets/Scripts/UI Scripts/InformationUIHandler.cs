/**********
 * Created by: Coleton Wheeler
 * Created on: 2/23/22
 * 
 * Last edited by: Coleton Wheeler
 * Last edited on: 3/3/22
 * 
 * Description: Displays simulation values for testing and examples of referencing
 *****/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationUIHandler : MonoBehaviour
{
    [Header("Set in Inspector (Carbon Data)")]
    [SerializeField] private Text _currentYearText;
    [SerializeField] private Text _currentAnnualIncreaseText;
    [SerializeField] private Text _currentPercentageText;
    [SerializeField] private Text _currentPPM_Text;
    [SerializeField] private Text _carbonNeutralCCUS_Text;
    [SerializeField] private Slider _sliderCCUS;

    [Header("Set in Inspector (Money Data)")]
    public Text currentMoney;
    public Text annualSpending;
    public Text annualBudget;

    private SimulationDataScriptableObject simulationData;
    private GameManager gm;
    private float timeSinceIncrease = 0f;
    private bool increasing = false;
    private bool decreasing = false;

    // Start is called before the first frame update
    void Awake()
    {
        gm = GameManager.GM;
        simulationData = gm.simData;
    }

    // Update is called once per frame
    void Update()
    {
        if (simulationData == null)
        {
            Debug.LogWarning("Simulation data is null");
            simulationData = gm.simData;
            return;
        }
        _currentYearText.text = "Current Year: " + simulationData.year;
        _currentAnnualIncreaseText.text = "Current Annual PPM Increase: " + roundToTwoDecimals(simulationData.annualIncrease);
        _currentPercentageText.text = "Current CCUS Percentage: " + simulationData.percentageCCUS + "%";
        _currentPPM_Text.text = "Current CO2 PPM: " + roundToTwoDecimals(simulationData.currentPPM) + "PPM";
        _carbonNeutralCCUS_Text.text = "CCUS Percentage Needed to be Carbon Neutral: " + simulationData.percentageForNeutral + "%";
        currentMoney.text = "Current Money: " + roundToTwoDecimals(simulationData.currentMoney) + " Trillion";
        annualSpending.text = "Annual Spending: " + roundToTwoDecimals(simulationData.annualCostOfCCUS) + " Trillion";
        annualBudget.text = "Annual Budget: " + roundToTwoDecimals(simulationData.annualBudget) + " Trillion";
        timeSinceIncrease += Time.deltaTime;
        if (timeSinceIncrease > 0.1f)
        {
            if (increasing)
                simulationData.percentageCCUS += 1;
            if (decreasing)
                simulationData.percentageCCUS -= 1;
            timeSinceIncrease = 0f;
            simulationData.percentageCCUS = Mathf.Clamp(simulationData.percentageCCUS, 0, 100);
        }
    }

    private float roundToTwoDecimals(float unrounded)
    {
        int tempInt = (int)(unrounded * 100f);
        return tempInt / 100f;
    }

    public void updatePercentage()
    {
        simulationData.percentageCCUS = (int) Mathf.Round(_sliderCCUS.GetComponent<Slider>().value * 100f);
    }

    public void IncreaseToggle()
    {
        increasing = !increasing;
    }

    public void DecreaseToggle()
    {
        decreasing = !decreasing;
    }

    public void SlowSpeedWorld()
    {
        simulationData.secondsPerYear = 2f;
    }

    public void NormalSpeedWorld()
    {
        simulationData.secondsPerYear = 1f;
    }

    public void FastSpeedWorld()
    {
        simulationData.secondsPerYear = 0.4f;
    }
}

