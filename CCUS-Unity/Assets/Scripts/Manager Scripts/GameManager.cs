/**********
 * Created by: Coleton Wheeler
 * Created on: 3/1/22
 * 
 * Last edited by: Coleton Wheeler
 * Last edited on: 4/14/22
 * 
 * Description: Handles all scenes and interactions between them.
 * Handles the GameStates, running Update for each script template through here.
 * Also is the accesser for the SimulationData object. Inefficiently going through Instance.simData to access variables..
 *****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region GameManager Singleton
    static private GameManager gm; //refence GameManager
    static public GameManager GM { get { return gm; } } //public access to read only gm 

    //Check to make sure only one gm of the GameManager is in the scene
    void CheckGameManagerIsInScene()
    {

        //Check if instnace is null
        if (gm == null)
        {
            gm = this; //set gm to this gm of the game object
            Debug.Log(gm);
        }
        else //else if gm is not null a Game Manager must already exsist
        {
            Destroy(this.gameObject); //In this case you need to delete this gm
            Debug.Log("Game Manager exists. Deleting...");
        }
        Debug.Log(gm);
    }//end CheckGameManagerIsInScene()
    #endregion



    [TextArea]
    [Tooltip("Doesn't do anything. Just for basic information to project contributors")]
    public string Notes = "For functionality, please name the scenes in the build after the game state name. Ex: MenuState -> MenuScene";

    [Header("Set in Inspector")]
    [SerializeField] GameStates StartingGameState; 
    public static GameManager instance { get; private set; }

    private Dictionary<string, GameBaseState> gameStatesDictionary;
    public bool isPaused = false;

    [HideInInspector]
    public SimulationDataScriptableObject simData;

    /* Make a new state for each game state/scene
     * 
     * Make sure to add accompanied Enum for the game state and add to dictionary
     */
    GameBaseState currentState;
    GameMenuState MenuState;
    GameLobbyState LobbyState;
    GameEducationState EducationState;
    GameSimulationState SimulationState;
    GameResultsState ResultsState;


    void Awake()
    {
        CheckGameManagerIsInScene();
    }
    void Start()
    {
        //Initialize simulation data with default values
        resetSimData();

        //Dictionary to store game states. Lets the code reference the script by just calling a string value
        gameStatesDictionary = new Dictionary<string, GameBaseState>
        {
            {"Menu", this.GetComponent<GameMenuState>()},
            {"Lobby", this.GetComponent<GameLobbyState>()},
            {"Simulation", this.GetComponent<GameSimulationState>()},
            {"Education", this.GetComponent<GameEducationState>()},
            {"Results", this.GetComponent<GameResultsState>() }
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
        if (isPaused)
            return;

        //Runs the UpdateState() function in the currently set state script
        if (currentState)
            currentState.UpdateState();
    }

    //Call this function to reset all sim data
    public void resetSimData()
    {
        Debug.Log("Resetting sim data with default values...");
        simData = SimulationDataScriptableObject.CreateInstance<SimulationDataScriptableObject>();
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
    Menu, Lobby, Education, Simulation, Results
}