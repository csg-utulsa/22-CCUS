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

    /* Make a new state for each game state/scene
     * 
     * Make sure to add accompanied Enum for the game state, as well as startup if logic
     */
    GameBaseState currentState;
    GameMenuState MenuState;
    GameEducationState EducationState;
    GameSimulationState SimulationState;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        //This isn't a great implementation yet. At the moment, every new scene will need a State variable setup like this. Along with an if statement accompanying it               
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
            //If Enum State doesn't exist, default to MenuState
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
