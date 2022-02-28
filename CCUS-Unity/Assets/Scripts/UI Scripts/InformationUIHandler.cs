/**********
 * Created by: Coleton Wheeler
 * Created on: 2/23/22
 * 
 * Last edited by: Coleton Wheeler
 * Last edited on: 2/24/22
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
    [SerializeField] private CarbonDataHandler cdh;
    [SerializeField] private Text _currentYearText;
    [SerializeField] private Text _currentAnnualIncreaseText;
    [SerializeField] private Text _currentPercentageText;
    [SerializeField] private Text _currentPPM_Text;
    [SerializeField] private Text _carbonNeutralCCUS_Text;
    [SerializeField] private Slider _sliderCCUS;

    // Start is called before the first frame update
    void Start()
    {
        _currentYearText.text = "Current Year: " + cdh.getCurrentYear();
        _currentAnnualIncreaseText.text = "Current Annual PPM Increase: " + roundToTwoDecimals(cdh.getAnnualCarbonIncrease());
        _currentPercentageText.text = "Current CCUS Percentage: " + cdh.getPercentageCCUS() + "%";
        _currentPPM_Text.text = "Current CO2 PPM: " + roundToTwoDecimals(cdh.getCurrentPPM()) + "PPM";
        _carbonNeutralCCUS_Text.text = "CCUS Percentage Needed to be Carbon Neutral: " + cdh.getCarbonNeutralCCUS() + "%";
    }

    // Update is called once per frame
    void Update()
    {
        _currentYearText.text = "Current Year: " + cdh.getCurrentYear();
        _currentAnnualIncreaseText.text = "Current Annual PPM Increase: " + roundToTwoDecimals(cdh.getAnnualCarbonIncrease());
        _currentPercentageText.text = "Current CCUS Percentage: " + cdh.getPercentageCCUS() + "%";
        _currentPPM_Text.text = "Current CO2 PPM: " + roundToTwoDecimals(cdh.getCurrentPPM()) + "PPM";
        _carbonNeutralCCUS_Text.text = "CCUS Percentage Needed to be Carbon Neutral: " + cdh.getCarbonNeutralCCUS() + "%";
    }

    private float roundToTwoDecimals(float unrounded)
    {
        int tempInt = (int)(unrounded * 100f);
        return tempInt / 100f;
    }

    public void updatePercentage()
    {
        cdh.setPercentageCCUS(Mathf.Round(_sliderCCUS.GetComponent<Slider>().value * 100f));
    }

}
