/**********
 * Created by: Coleton Wheeler
 * Created on: 2/22/22
 * 
 * Last edited by: N/A
 * Last edited on: N/A
 * 
 * Description: Allows dynamic filling of a UI image, specifically the one contained inside of the CarbonBar parent object.
 * To reference this from another script, add "[SerializeField] private BarHandler bh;" and set the parent object of CarbonBarHandler.cs in the inspector.
 * To update values from another script, do dbh.setFill(fillTotal), dbh.setFillBounds(min, max), etc.
 *****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarHandler : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Image CarbonBarFill;

    [Header("Set in Inspector or Dynamically")]
    [SerializeField] private float _maxFillAmount = 100;
    [SerializeField] private float _minFillAmount = 0;

    //Allows other scripts to adjust the fill amount.
    public void setFill(float fillTotal)
    {
        //Clamps the sent fill value between the minimum and maximum value. Useful for unintentional inputs (i think, unity might do this automatically)
        CarbonBarFill.fillAmount = Mathf.Clamp(fillTotal / _maxFillAmount, _minFillAmount, _maxFillAmount);
    }

    //Allows other scripts to set the minimum and maximum amount of carbon units for the bar to fill.
    public void setFillBounds(float newMin, float newMax)
    {
        _minFillAmount = newMin;
        _maxFillAmount = newMax;
    }

    //Getters for min and max values
    public float getFillMax()
    {
        return _maxFillAmount;
    }

    public float getFillMin()
    {
        return _minFillAmount;
    }

    //Resets the fill total to whatever the max value is set as.
    public void ResetBar()
    {
        setFill(_maxFillAmount);
    }
    //Allows a ResetBar() call with a new min/max setting
    public void ResetBar(float min, float max)
    {
        setFillBounds(min, max);
        setFill(_maxFillAmount);
    }

    //Sets values on start
    private void Start()
    {
        ResetBar();
    }

}
