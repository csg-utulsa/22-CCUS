/**********
 * Created by: Coleton Wheeler
 * Created on: 3/1/22
 * 
 * Last edited by: Coleton Wheeler
 * Last edited on: 8/22/22
 * 
 * Description: Handles all scenes and interactions between them.
 * Handles the GameStates, running Update for each script template through here.
 * Also is the accesser for the SimulationData object. Inefficiently going through Instance.simData to access variables..
 *****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

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
            Debug.Log(gm + " Loaded");
        }
        else //else if gm is not null a Game Manager must already exsist
        {
            Destroy(this.gameObject); //In this case you need to delete this gm
            Debug.Log("Game Manager exists. Deleting...");
        }
    }//end CheckGameManagerIsInScene()
    #endregion

    [Header("Set in Inspector")]
    [SerializeField] GameState StartingGameState; 
    public static GameManager instance { get; private set; }
    public bool isPaused = false;
    [HideInInspector] public SimulationDataScriptableObject simData;

    [Space(10)]

    //Input handling and initialization variables
    [Header("Input type variables")]
    public bool OverrideInputMethod = false;
    [SerializeField] InputType inputOverride;
    public InputType InputMode { get; private set; }

    /* Make a new state for each game state/scene
     * 
     * Make sure to add accompanied Enum for the game state
     */
    GameBaseState currentStateScript;
    [SerializeField] GameMenuState MenuState;
    [SerializeField] GameLobbyState LobbyState;
    [SerializeField] GameSimulationState SimulationState;
    [SerializeField] GameResultsState ResultsState;


    void Awake()
    {
        DetectInputMode();
        CheckGameManagerIsInScene();
    }
    void Start()
    {
        //Initialize simulation data with default values
        resetSimData();

        //Sets the current state to whatever is set in the Inspector as default state
        ChangeState(StartingGameState);
    }

    void Update()
    {
        if (isPaused)
            return;

        //Runs the UpdateState() function in the currently set state script
        currentStateScript.UpdateState();
    }

    //Call this function to reset all sim data
    public void resetSimData()
    {
        Debug.Log("Resetting sim data with default values...");
        simData = ScriptableObject.CreateInstance<SimulationDataScriptableObject>();
    }

    //Passes in a key to the dictionary of different game states
    //This also sucks as an implementation, but it suffices and isn't too hard to add onto later
    public void ChangeState(GameState newState)
    {
        switch(newState)
        {
            case GameState.Menu:
                currentStateScript = MenuState;
                break;
            case GameState.Lobby:
                currentStateScript = LobbyState;
                break;
            case GameState.Simulation:
                currentStateScript = SimulationState;
                break;
            case GameState.Results:
                currentStateScript = ResultsState;
                break;
            default:
                throw new System.Exception("State not found");
        }

        currentStateScript.EnterState();
    }

    #region VR Detection (non-functional)
    void DetectInputMode()
    {
        if (IsVRAttatched())
        {
            InputMode = InputType.VR;
        }
        else
        {
            InputMode = InputType.Keyboard;
        }

        if (OverrideInputMethod)
        {
            InputMode = inputOverride;
        }

    }

    bool IsVRAttatched()
    {
        List<XRDisplaySubsystem> displaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(displaySubsystems);
        foreach (var subsystem in displaySubsystems)
        {
            if(subsystem.running)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}

//Holds enum values for all possible scenes - used for inspector drop down menu
public enum GameState
{
    Menu, Lobby, Simulation, Results
}

//Enum to determine input type
public enum InputType
{
    Keyboard, VR
}