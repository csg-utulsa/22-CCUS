using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**********
 * Created by: Coleton Wheeler
 * Created on: 7/17/22
 * 
 * Last edited by: 
 * Last edited on: 
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
    public float mouseSensitivity = 35f;
    private float rotationX = 0f;
    public bool enableCameraRotate = false;

    [Space(10)]

    //Movement variables
    [Header("Movement variables")]
    public float speed = 15f;
    private InputAction horizontal;
    private InputAction vertical;

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
        mouseX *= mouseSensitivity;
        mouseY *= mouseSensitivity;

        //Rotates the horizontal direction
        transform.Rotate(Vector3.up * mouseX * Time.deltaTime);

        //Alters and clamps the vertical rotation
        rotationX -= mouseY * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -70f, 70f);
        transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y, 0);

        
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
    public void UnlockCameraRotate()
    {
        enableCameraRotate = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LockCameraRotate()
    {
        enableCameraRotate = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    #endregion

}
