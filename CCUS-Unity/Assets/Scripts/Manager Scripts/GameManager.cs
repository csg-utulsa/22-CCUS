/**********
 * Created by: Coleton Wheeler
 * Created on: 3/1/22
 * 
 * Last edited by: N/A
 * Last edited on: N/A
 * 
 * Description: Handles all scenes and interactions between them.
 * Handles the GameState's, running Update for each script template through here.
 *****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] GameStates StartingGameState; 
    public static GameManager instance { get; private set; }

    GameBaseState currentState;
    GameMenuState MenuState;
    GameEducationState EducationState;
    GameSimulationState SimulationState;

    void Awake()
    {
        SimulationState = GameObject.Find("SimulationManager").GetComponent<GameSimulationState>();
        //Sets the current state to whatever is set in the Inspector as default state
        if (StartingGameState == GameStates.Menu)
        {
            currentState = MenuState;
        }
        else if (StartingGameState == GameStates.Education)
        {
            currentState = EducationState;
        }
        else if (StartingGameState == GameStates.Simulation)
        {
            currentState = SimulationState;
        }
        else
        {
            currentState = MenuState;
        }

        //Runs EnterState() of the newly set state script
        currentState.EnterState();
    }

    void Update()
    {
        //Runs the UpdateState() function in the currently set state script
        if (currentState)
            currentState.UpdateState();
    }
}

//Holds enum values for all possible scenes - used for inspector drop down menu
enum GameStates
{
    Menu, Education, Simulation
}
