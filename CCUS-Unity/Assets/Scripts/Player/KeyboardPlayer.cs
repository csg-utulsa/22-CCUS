using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

/**********
 * Created by: Coleton Wheeler
 * Created on: 7/17/22
 * 
 * Last edited by: Coleton Wheeler
 * Last edited on: 8/31/22
 * 
 * Description: Will manage all player input and controls when switched to keyboard and mouse mode.
 *****/

public class KeyboardPlayer : MonoBehaviour
{
    public PlayerInput playerInput;
    public CharacterController controller;

    [Space(10)]

    //Cursor and camera variables
    [Header("Camera and Cursor variables")]
    [SerializeField] private Camera PlayerCamera;
    [Range(0f, 100f)] public float mouseSensitivity = 50f;
    private float rotationY = 0f;
    public bool enableCameraRotate = false;

    [Space(10)]

    //Movement variables
    [Header("Movement variables")]
    public float speed = 15f;
    private InputAction horizontal;
    private InputAction vertical;

    [Space(10)]

    //Menu variables
    [Header("UI Variables")]
    [SerializeField] private GameObject CCUSMenu;
    [SerializeField] private GameObject PauseMenu;

    void Start()
    {
        horizontal = playerInput.actions.FindAction("Horizontal");
        vertical = playerInput.actions.FindAction("Vertical");
        LockCameraRotate();
    }


    void Update()
    {
        //Runs updates to check mouse position and translate that into camera movement
        MouseUpdate();
        MovementUpdate();
    }

    #region Mouse Updates
    private void MouseUpdate()
    {
        if (enableCameraRotate)
        {
            MoveCamera(); //Calculates mouse movement and alters the camera
        }



    }

    public void MoveCamera()
    {
        //Create variables to store the x and y movement
        float mouseX = 0;
        float mouseY = 0;

        //Run through different input types to detect and record x and y camera movements
        if (Mouse.current != null)
        {
            mouseX = Mouse.current.delta.ReadValue().x;
            mouseY = Mouse.current.delta.ReadValue().y;
        }
        else if (Gamepad.current != null) //Controller compatability for later use
        {
            mouseX = Gamepad.current.rightStick.ReadValue().x;
            mouseY = Gamepad.current.rightStick.ReadValue().y;
        }

        //Adjust mouse values by the sensitivity selected
        mouseX *= mouseSensitivity / 10;
        mouseY *= mouseSensitivity / 10;

        rotationY += mouseY * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -70f, 70f);
        PlayerCamera.transform.localEulerAngles = new Vector3(-rotationY, PlayerCamera.transform.localEulerAngles.y, 0);
        transform.Rotate(mouseX * Vector3.up * Time.deltaTime);




        //Rotates the horizontal direction
        //PlayerCamera.gameObject.transform.Translate(Vector3.up * mouseX * Time.deltaTime);
        //PlayerCamera.transform.Rotate(Vector3.up * mouseX * Time.deltaTime);
        //PlayerCamera.gameObject.Rotate(Vector3.up * mouseX * Time.deltaTime);

        //Alters and clamps the vertical rotation
        /*rotationX -= mouseY * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -70f, 70f);
        transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y, 0);*/

        
    }

    #endregion

    #region Character Movement
    private void MovementUpdate()
    {
        //Using Player Input actions, receive horizontal and vertical input axises
        float x = horizontal.ReadValue<float>();
        float z = vertical.ReadValue<float>();

        //Cram the values into a vector, orientate it in respect to the camera, and then set the Y to 0
        Vector3 move = new Vector3(x, 0, z);
        move = PlayerCamera.transform.TransformDirection(move);
        move.y = 0; //This can potentially cause issues later if we want vertical climbing i.e. stairs. For another day...

        controller.Move(move * speed * Time.deltaTime);
    }

    #endregion

    #region CameraLock methods
    //Unlocks the camera for UI navigation
    public void UnlockCameraRotate()
    {
        enableCameraRotate = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //Locks the camera for the FPS controller
    public void LockCameraRotate()
    {
        enableCameraRotate = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    #endregion

    //Open menu
    public void ToggleCCUSMenu(CallbackContext callbackContext)
    {
        //Check if the callback context is on performed
        if (callbackContext.phase != InputActionPhase.Performed)
        {
            return;
        }

        if (CCUSMenu.activeSelf == false)
        {
            //Check if pause menu is open
            if (PauseMenu.activeSelf == true)
            {
                LockCameraRotate();

                //If pause menu fleshed out, will need to save adjustments before closing

                PauseMenu.SetActive(false);
            }

            CCUSMenu.SetActive(true);
            UnlockCameraRotate();
        } 
        else if (CCUSMenu.activeSelf == true)
        {
            CCUSMenu.SetActive(false);
            LockCameraRotate();
        }

    }

    public void TogglePauseMenu(CallbackContext callbackContext)
    {
        //Check if the callback context is on performed
        if (callbackContext.phase != InputActionPhase.Performed)
        {
            return;
        }

        if (PauseMenu.activeSelf == false)
        {

            //Check if CCUS menu is open
            if (CCUSMenu.activeSelf == true)
            {
                LockCameraRotate();
                CCUSMenu.SetActive(false);
            }


            PauseMenu.SetActive(true);
            UnlockCameraRotate();
        }
        else if (PauseMenu.activeSelf == true)
        {
            PauseMenu.SetActive(false);
            LockCameraRotate();
        }

    }

}
