using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{

    CharacterController controller;
    public float gravity = 1f;
    public float maxVelocity = 10f;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!controller.isGrounded)
        {
            velocity.y -= gravity;
            velocity.y = Mathf.Clamp(velocity.y, (-1) * maxVelocity, 0);
        } 
        else 
        {
            velocity = Vector3.zero;
        }
        Debug.Log(velocity.y);
        controller.Move(velocity * Time.deltaTime);
    }
}
