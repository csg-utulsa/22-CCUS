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
    [SerializeField] private GameObject gss;
    [SerializeField] private Text _currentYearText;
    [SerializeField] private Text _currentAnnualIncreaseText;
    [SerializeField] private Text _currentPercentageText;
    [SerializeField] private Text _currentPPM_Text;
    [SerializeField] private Text _carbonNeutralCCUS_Text;
    [SerializeField] private Slider _sliderCCUS;

    private GameSimulationState gssScript;

    // Start is called before the first frame update
    void Awake()
    {
        /*gss = GameObject.Find("SimulationManager");
        gssScript = gss.GetComponent<GameSimulationState>();
        _currentYearText.text = "Current Year: " + gssScript.simulationStats.year ;
        _currentAnnualIncreaseText.text = "Current Annual PPM Increase: " + roundToTwoDecimals(gssScript.simulationStats.annualIncrease);
        _currentPercentageText.text = "Current CCUS Percentage: " + gssScript.simulationStats.percentageCCUS + "%";
        _currentPPM_Text.text = "Current CO2 PPM: " + roundToTwoDecimals(gssScript.simulationStats.currentPPM) + "PPM";
        _carbonNeutralCCUS_Text.text = "CCUS Percentage Needed to be Carbon Neutral: " + gssScript.simulationStats.percentageForNeutral + "%";
*/    }

    // Update is called once per frame
    void Update()
    {
        /*_currentYearText.text = "Current Year: " + gssScript.simulationStats.year;
        _currentAnnualIncreaseText.text = "Current Annual PPM Increase: " + roundToTwoDecimals(gssScript.simulationStats.annualIncrease);
        _currentPercentageText.text = "Current CCUS Percentage: " + gssScript.simulationStats.percentageCCUS + "%";
        _currentPPM_Text.text = "Current CO2 PPM: " + roundToTwoDecimals(gssScript.simulationStats.currentPPM) + "PPM";
        _carbonNeutralCCUS_Text.text = "CCUS Percentage Needed to be Carbon Neutral: " + gssScript.simulationStats.percentageForNeutral + "%";
*/    }

    private float roundToTwoDecimals(float unrounded)
    {
        int tempInt = (int)(unrounded * 100f);
        return tempInt / 100f;
    }

    public void updatePercentage()
    {
        //gssScript.simulationStats.percentageCCUS = (int) Mathf.Round(_sliderCCUS.GetComponent<Slider>().value * 100f);
    }

}
