using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnityChanMovement : MonoBehaviour
{
    public CharacterController controller;
    private Animator anim;
    private bool stop;
    private float dashSpeed = 12000f;
    private short direction = 1;

    //---------------------------------------------------------------------
    private Vector3 velocity;
    private Vector3 moveVector;
    private Vector3 lastMove;
    public float speed = 8;
    public float jumpForce = 20;
    private float gravity = 25;
    public float verticalVelocity;
    private float currentDelay = 0;
    public float delay = 0.2f;
    private bool walljump = false;
    int dashDirection = 0; // 0 = no dash, 1 = right, -1 = left
    float dashTimer = 0;
    float dashDuration = 0.2f;
    //---------------------------------------------------------------------



    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveVector = Vector3.zero;

        // Run---------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.C) && dashDirection == 0)
        {
            dashTimer = -200;
            if (direction == 0)
                dashDirection = -1;
            if (direction == 1)
                dashDirection = 1;
            //dashDirection += dashSpeed * speed * 400;
        }
        else
        {
            if (dashDirection == 0)
            {
                moveVector.x += Input.GetAxis("Horizontal")/* * speed * 4*/;
            }
            else if (dashDirection == 1)
            {
                dashTimer += Time.deltaTime;
                moveVector.x += dashSpeed * speed * 1800;
                verticalVelocity = 0;
                lastMove = moveVector;
            }
            else if (dashDirection == -1)
            {
                dashTimer += Time.deltaTime;
                moveVector.x -= dashSpeed * speed * 1800;
                verticalVelocity = 0;
                lastMove = moveVector;
            }

            if (dashTimer > dashDuration)
            {
                dashDirection = 0;
            }
        }
        if (controller.isGrounded)
        {
            // Jump--------------------------------------------------------
            if (Input.GetKeyDown(KeyCode.X))
            {
                verticalVelocity = jumpForce;
                moveVector.x += Input.GetAxis("Horizontal")/* * speed * 4*/;
            }
            else
            {
                verticalVelocity = -5;  // Normalize
            }
        }
        
        // WallJump----------------------------------------------------
        else if (walljump == true)
        {
            verticalVelocity -= gravity * Time.deltaTime;
            moveVector = lastMove;
            currentDelay += Time.deltaTime;

            if (currentDelay > delay)
                walljump = false;
        }
        
        // Move for Jump on the way------------------------------------
        else
        {
            moveVector = lastMove;
            verticalVelocity -= gravity * Time.deltaTime;
            moveVector.x += Input.GetAxis("Horizontal") * speed * 4;
        }
      
        // Normalization-----------------------------------------------
        moveVector.y = 0;
        moveVector.Normalize();
        moveVector *= speed;
        moveVector.y = verticalVelocity;
        velocity += moveVector;
        velocity.Normalize();
        controller.Move(velocity * speed * Time.deltaTime);
        lastMove = moveVector;
        //-------------------------------------------------------------
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!controller.isGrounded && hit.normal.y < 0.1f)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {   // WallJump
                currentDelay = 0.0f;
                walljump = true;
                verticalVelocity = jumpForce;
                moveVector = hit.normal * speed;
                lastMove = moveVector;
                Debug.DrawRay(hit.point, hit.normal, Color.red, 1.25f);
                Debug.DrawRay(transform.position, hit.normal * 2, Color.green, 1);
            }
        }
    }


    void FixedUpdate()
    {
        if (!stop)
        {
            float move = Input.GetAxis("Horizontal");
            anim.SetFloat("Speed", Mathf.Abs(move));

            // Charcter direction
            if (move < 0)
            { // Left
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y + 270, 0));
                direction = 0;
            }
            if (move > 0)
            { // Right
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y - 270, 0));
                direction = 1;
            }
        }

        if (stop)
            anim.SetFloat("Speed", Mathf.Abs(0));
    }

    public void setStop(bool aa)
    {
        stop = aa;
    }

}
