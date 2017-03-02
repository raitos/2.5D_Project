using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnityChanMovement : MonoBehaviour
{
    public CharacterController controller;
    private Animator anim;
    private bool stop;
    private float dushSpeed = 1200f;
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
    int dushDirection = 0; // 0 = no dush, 1 = right, -1 = left
    float dushTimer = 0;
    float dushDuration = 0.2f;
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
        if (Input.GetKeyDown(KeyCode.C) && dushDirection == 0)
        {
            dushTimer = 0;
            if (direction == 0)
                dushDirection = -1;
            if (direction == 1)
                dushDirection = 1;
            //dushDirection += dushSpeed * speed * 400;
        }
        else
        {
            if (dushDirection == 0)
            {
                moveVector.x += Input.GetAxis("Horizontal")/* * speed * 4*/;
            }
            else if (dushDirection == 1)
            {
                dushTimer += Time.deltaTime;
                moveVector.x += dushSpeed * speed * 800;
                verticalVelocity = 0;
                lastMove = moveVector;
            }
            else if (dushDirection == -1)
            {
                dushTimer += Time.deltaTime;
                moveVector.x -= dushSpeed * speed * 800;
                verticalVelocity = 0;
                lastMove = moveVector;
            }

            if (dushTimer > dushDuration)
            {
                dushDirection = 0;
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
                verticalVelocity = -1;  // Normalize
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
