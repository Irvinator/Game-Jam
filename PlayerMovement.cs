using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    private int stamina = 100;
    private float speed = 6.9f;
    private float crouchSpeed = 3.9f;
    private float sprintSpeed = 15f;
    private float gravity = -22f;
    private float jumpHeight = 3f;

    public Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;


    Vector3 velocity;
    private bool isGrounded;

    private bool m_Crouch = false;

    private float m_OriginalHeight;
    

    private float m_CrouchHeight = 0.9f;

    private KeyCode crouchKey = KeyCode.C;


    void Start()
    {
        stamina = 100;

        m_OriginalHeight = controller.height;

    }
  
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(m_Crouch == true)
        {
            isGrounded = !isGrounded;
        }


        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown("space") && isGrounded && m_Crouch == false)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        
        Sprint();

        
        if (Input.GetKeyDown(crouchKey) && isGrounded == true && move.z == 0 && move.x == 0)
        {
            
            m_Crouch = !m_Crouch;

            CheckCrouch();
               
        }


    }

    
    void CheckCrouch()
    {
        if (m_Crouch == true)
        {
            controller.height = m_CrouchHeight;
            speed = crouchSpeed;
        }
        else
        {
            controller.height = m_OriginalHeight;
            
        }
    }


    
    void Sprint()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift) && m_Crouch == false)
            {
                speed = sprintSpeed;
            }
            else
            {
                if(m_Crouch == false)
                {
                   speed = 6.9f;
                }
                else
                {
                    speed = crouchSpeed;
                }
               
            }
        }
    }




}
