using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**********
 * Created by: Coleton Wheeler
 * Created on: 7/17/22
 * 
 * Last edited by: Coleton Wheeler
 * Last edited on: 1/30/23
 * 
 * Description: Handles all logic according to the player specifically. I.E. input handling
 *****/


public class PlayerManager : MonoBehaviour
{

    #region PlayerManager Singleton
    static private PlayerManager pm; //refence PlayerManager
    static public PlayerManager PM { get { return pm; } } //public access to read only pm 

    //Check to make sure only one gm of the PlayerManager is in the scene
    void CheckPlayerManagerIsInScene()
    {

        //Check if instnace is null
        if (pm == null)
        {
            pm = this; //set pm to this pm of the game object
            Debug.Log(pm + " Loaded");
        }
        else //else if pm is not null a Player Manager must already exsist
        {
            Destroy(this.gameObject); //In this case you need to delete this pm
            Debug.Log("Player Manager exists. Deleting...");
        }
    }
    #endregion


    //Gameobjects
    [Space(10)]
    public GameObject VR_Prefab;
    public GameObject Keyboard_Prefab;
    public GameObject player { get; private set; }


    //Player variables
    private Vector3 respawnLocation;
    private bool respawnHasBeenSet = false; //Variable to detect if a respawn has been set. Since Vector3 defaults as Vector3.zero when uninstantiated,
                                            //I would like to keep track of when there is no spawn point in the world
    [HideInInspector] public float playerHeight;
    public float defaultPlayerHeight = 2f; //Sets to 2 as default. Eventually would want to use VR to detect the users height

    //Input mode
    private InputType localInputMode;

    //Singletons
    GameManager gm;

    void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void Awake()
    {
        CheckPlayerManagerIsInScene();
        gm = GameManager.GM;
        playerHeight = defaultPlayerHeight;
        Debug.Log("Input Mode: " + gm.InputMode + " (PlayerManager)");
        InstantiatePlayer();
    }

    //Allows other scripts to change the respawn location of the player
    public void SetPlayerRespawn(Vector3 newSpawn)
    {
        respawnHasBeenSet = true;
        respawnLocation = newSpawn;
    }

    //Respawn the player to its' designated location
    public void RespawnPlayer()
    {
        player.transform.position = respawnLocation;
    }


    #region Player Instantiation

    public void InstantiatePlayer()
    {
        InputType inputMode = GameManager.GM.InputMode;
        //Instantiate player model
        if (inputMode == InputType.Keyboard)
        {
            player = Instantiate(Keyboard_Prefab);
            player.name = "Player (Keyboard)";
        }
        else if (inputMode == InputType.VR)
        {
            player = Instantiate(VR_Prefab);
            player.name = "Player (VR)";
        }
        else
        {
            Debug.LogWarning("Unexpected result found. Instantiating VR player as default");
            inputMode = InputType.VR;
            player = Instantiate(VR_Prefab);
            player.name = "Player (VR)";
        }
        SetPlayerRespawn(transform.position);
        RespawnPlayer();
    }

    //When a new scene loads, instantiate the player model again.
    void ChangedActiveScene(Scene current, Scene next)
    {
        InstantiatePlayer();
    }

    #endregion

}