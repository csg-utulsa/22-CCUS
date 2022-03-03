using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicInfoScriptableObject : ScriptableObject
{
    [Header("Set in Inspector")]
    public int year;
    public int secsBetweenYears;

    // Scripts subscripe to this to get notified of year changes
    [System.NonSerialized]
    public UnityEvent infoChangeEvent;

    private void OnEnable()
    {
        if (infoChangeEvent == null)
            infoChangeEvent = new UnityEvent();
    }

    public void IncreaseYear()
    {
        Debug.Log("Increasing year");
        year++;
        infoChangeEvent.Invoke();
    }

}
