using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 13f;
    public float crouchSpeed = 3f;
    public float crouchHeight = 2f;
    public float gravity = -9.81f;
    public float jumpHeight = 3.0f;

    public Vector3 velocity;
    //public Vector3 move;


    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    void Start()
    {
        
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y <= 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            controller.Move(move * speed * 1.5f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftControl) && isGrounded)
        {
            controller.height = 1.9f;
        }
        else
        {
            controller.height = 3.8f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //apply gravity

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
