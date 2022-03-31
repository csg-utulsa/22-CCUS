/**********
 * Created by: Coleton Wheeler
 * Created on: 3/1/22
 * 
 * Last edited by: Coleton Wheeler
 * Last edited on: 3/31/22
 * 
 * Description: Handles all scenes and interactions between them.
 * Handles the GameStates, running Update for each script template through here.
 * Also is the accesser for the SimulationData object. Inefficiently going through Instance.simData to access variables..
 *****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] GameStates StartingGameState; 
    public static GameManager instance { get; private set; }

    private Dictionary<string, GameBaseState> gameStatesDictionary;

    [HideInInspector]
    public SimulationDataScriptableObject simData;

    /* Make a new state for each game state/scene
     * 
     * Make sure to add accompanied Enum for the game state and add to dictionary
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

        //Initialize simulation data with default values
        simData = SimulationDataScriptableObject.CreateInstance<SimulationDataScriptableObject>();
        Debug.Log(simData.year);
        resetSimData();

        gameStatesDictionary = new Dictionary<string, GameBaseState>
        {
            {"Menu", this.GetComponentInChildren<GameMenuState>()},
            {"Simulation", this.GetComponentInChildren<GameSimulationState>()},
            {"Education", this.GetComponentInChildren<GameEducationState>()}
        };

        //Sets the current state to whatever is set in the Inspector as default state
        if (!gameStatesDictionary.TryGetValue(StartingGameState.ToString(), out currentState))
        {
            //If enum does not match a dictionary key, set to default MenuState
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

    //Call this function to reset all sim data
    public void resetSimData()
    {
        Debug.Log("Resetting sim data with default values...");
    }

    //Passes in a key to the dictionary of different game states
    //This also sucks as an implementation, but it suffices and isn't too hard to add onto later
    public void ChangeState(string newState)
    {
        if (gameStatesDictionary.TryGetValue(newState, out currentState))
        {
            currentState.EnterState();
        }
        else
        {
            throw new System.Exception("State not found");
        }
    }
}

//Holds enum values for all possible scenes - used for inspector drop down menu
enum GameStates
{
    Menu, Education, Simulation
}