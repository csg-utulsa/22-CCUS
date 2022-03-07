using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TemperatureHandler : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Image CarbonBarFill;
    float currentPPM = 417.5f;

    [Header("Set in Inspector or Dynamically")]
    [SerializeField] private float _maxFillAmount = 100f;
    [SerializeField] private float _minFillAmount = 0f;

    [Header("Average Recorded Temperature in Current Year")]
    [SerializeField] private float _avgTemperature = 19.111f;

    [Header("Current Temperature (do not touch)")]
    [SerializeField] private float _annualTemperature;

    // Equation is y = mx + b;
    // Y is temp, m is C / PPM, b is starting temp

    void Start()
    {
        _annualTemperature = _avgTemperature;
    }

    // FixedUpdate is called 30x per sec

    // Checks PPM before temperature. Then sets a temperature value. Every year, updates  
    void FixedUpdate()
    {
        _annualTemperature = (.006f) * currentPPM + _avgTemperature;
    }
}
