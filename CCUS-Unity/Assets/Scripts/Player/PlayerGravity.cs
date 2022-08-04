using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    public bool gravityEnabled = true;
    
    [Space(10)]

    public float gravity = 1f;
    public float maxVelocity = 10f;
    public float velocityScaling = 2f;
    
    private Vector3 velocity = Vector3.zero;
    CharacterController controller;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void FixedUpdate()
    {
        if (!controller.isGrounded && gravityEnabled)
        {
            velocity.y -= gravity * Time.deltaTime * velocityScaling;
            velocity.y = Mathf.Clamp(velocity.y, (-1) * maxVelocity, 0);
            controller.Move(velocity * Time.deltaTime);
        } 
        else 
        {
            velocity = Vector3.zero;
        }
    }
}