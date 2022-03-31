using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{

    CharacterController controller;
    public float gravity = 1f;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.isGrounded)
        {
            velocity.y -= gravity;
        } 
        else 
        {
            velocity = Vector3.zero;
        }
        controller.Move(velocity * Time.deltaTime);
    }
}
