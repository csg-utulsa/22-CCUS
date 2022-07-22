using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**********
 * Created by: Coleton Wheeler
 * Created on: 7/17/22
 * 
 * Last edited by: 
 * Last edited on: 
 * 
 * Description: Handles switching between keyboard and mouse or VR depending on if a VR is detected or not.
 *****/

public class InputHandler : MonoBehaviour
{
    public bool OverrideInputMethod = false;
    [SerializeField] InputType inputOverride;

    [Space(10)]

    public GameObject VR;
    public GameObject Keyboard;


    private string inputModeString;


    void Awake()
    {
        //inputModeString = GM.somehow get mode from auto detection

        //Overwrite this with inspector option
        if (OverrideInputMethod)
        {
            inputModeString = inputOverride.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inputModeString == "Keyboard")
        {
            EnableKeyboard();
        }
        else if (inputModeString == "VR")
        {
            EnableVR();
        }
    }

    private void EnableVR()
    {
        VR.SetActive(true);
        Keyboard.SetActive(false);
    }

    private void EnableKeyboard()
    {
        Keyboard.SetActive(true);
        VR.SetActive(false);
    }
}

enum InputType
{
    Keyboard, VR
}
