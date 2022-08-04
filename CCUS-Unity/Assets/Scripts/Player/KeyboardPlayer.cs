using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

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
    private Input playerInput;

    //Cursor and camera variables
    [SerializeField] private Camera PlayerCamera;
    public float mouseSensitivity = 3.5f;
    private float rotationY;
    private bool lockCursor = true;

    void Start()
    {
        
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
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            //Rework with new input system.

            //Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            //cameraPitch -= targetMouseDelta.y * mouseSensitivity;
            //cameraPitch = Mathf.Clamp(cameraPitch, -85.0f, 85.0f);

            //PlayerCamera.transform.localEulerAngles = Vector3.right * cameraPitch; // I dont know how local Euler Angles works ------ research later
            //transform.Rotate(Vector3.up * targetMouseDelta.x * mouseSensitivity);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        ////Move camera based on cursor movement
        //rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;
        //rotationY = Mathf.Clamp(rotationY, -70f, 70f);
        //PlayerCamera.transform.localEulerAngles = new Vector3(-rotationY, PlayerCamera.transform.localEulerAngles.y, 0);
        //transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0));


    }

    public void MoveCamera(CallbackContext context)
    {
        Debug.Log(context);
    }

    #endregion

    private void MovementUpdate()
    {

    }

    public void ToggleCursor()
    {
        lockCursor = !lockCursor;
        Cursor.visible = lockCursor;
    }

}
