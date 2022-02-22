/**********
 * Created by: Coleton Wheeler
 * Created on: 2/22/22
 * 
 * Last edited by: N/A
 * Last edited on: N/A
 * 
 * Description: A simple testing UI slider for functionality of CarbonBarHandler
 * ***/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdater : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private Slider slider;
    [SerializeField] private CarbonBarHandler cbh;
    public void onSliderUpdate()
    {
        //Scales multiplaction of slider value (0-1) with factor of range from fillMax to fillMin
        cbh.setFill(slider.GetComponent<Slider>().value * (cbh.getFillMax() - cbh.getFillMin()));
    }
}
