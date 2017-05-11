using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerPhysics))]
public class Controller : MonoBehaviour {

    public float gravity;
    public float speed;
    public float acceleration;
    public float jumpHeight;
    public int dir = 1;

    GameObject animatedObj;

    public Animator anim;

    private float curSpeed;
    private float tarSpeed;
    private Vector2 amountToMove;

    ShootingTwo shoot;

    public float DashCoolDown = 1;
    bool DashCD = false;
    float dashT = 0;

    PlayerPhysics playerPhysics;

    void Start()
    {
        shoot = gameObject.GetComponent<ShootingTwo>();
        animatedObj = GameObject.Find("model_character_main_05_03_animation_all");
        playerPhysics = GetComponent<PlayerPhysics>();
        anim = animatedObj.GetComponent<Animator>();
    }

    void Update()
    {
        /*Animator bools:
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsJump", false);
            anim.SetBool("IsDash", false);
            anim.SetBool("IsShoot", false);
        */
        //Animator things and direction detection
        anim.SetFloat("WalkSpeed", Mathf.Abs(tarSpeed));
        if (tarSpeed < 0 && !playerPhysics.Dash)
        { // Left
            animatedObj.transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y - 90, 0));
            dir = -1;
            if (!playerPhysics.Dash && !shoot.shoot)
            {
                anim.SetBool("IsWalk", true);
                anim.SetBool("IsIdle", false);
            }
        }
        else if (tarSpeed > 0 && !playerPhysics.Dash)
        { // Right
            animatedObj.transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y + 90, 0));
            dir = 1;
            if (!playerPhysics.Dash && !shoot.shoot)
            {
                anim.SetBool("IsWalk", true);
                anim.SetBool("IsIdle", false);
            }
        }
        else
        {
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsIdle", true);
        }
        //---------------

        if (Input.GetKey(KeyCode.Z))
        {
            anim.SetBool("IsShoot", true);
        }

        //Dash-------------

        if (DashCD)
        {
            dashT += Time.deltaTime;
            if (dashT > DashCoolDown)
            {
                DashCD = false;
                playerPhysics.DashMax = 0;
            }

        }
        else if ((Input.GetKeyDown(KeyCode.C) && !DashCD && !playerPhysics.FacingWall) && !playerPhysics.MidAirDashUsed)
        {
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsJump", false);
            anim.SetBool("IsDash", true);
            playerPhysics.Dash = true;
            DashCD = true;
            playerPhysics.MidAirDashUsed = true;
            dashT = 0;
            playerPhysics.DashDirection = dir;
            amountToMove.y = 0;
        }
        if (!playerPhysics.Dash)
        {
            anim.SetBool("IsDash", false);
        }
        //-----------------


        tarSpeed = Input.GetAxis("Horizontal") * speed;
        //curSpeed = IncrementTowards(curSpeed, tarSpeed, acceleration);


        amountToMove.x = tarSpeed; //For acceleration use "curSpeed" instead of "tarSpeed"

        if (playerPhysics.Roofed)
        {
            amountToMove.y = 0;
        }

        if ((playerPhysics.Grounded || playerPhysics.sloped)) //Wierd things happen here
        {
            anim.SetBool("IsJump", false);
            amountToMove.y = -0.01F;
            if (Input.GetKeyDown(KeyCode.X) && !playerPhysics.Dash)
            {
                transform.Translate(Vector2.up * 0.2F * playerPhysics.timeScale);
                amountToMove.y = jumpHeight;
                anim.SetBool("IsJump", true);
                anim.SetBool("IsWalk", false);
                anim.SetBool("IsDash", false);
            }
        }
        else if (!playerPhysics.Dash)
        {
            amountToMove.y -= gravity * Time.deltaTime;
        }
        if (playerPhysics.FacingWall)
        {
            if ((Input.GetKeyDown(KeyCode.X) && amountToMove.y < 0) && (tarSpeed > 0 || tarSpeed < 0)) //Walljump
            {
                amountToMove.y = jumpHeight * 0.75F;
            }
            else if (amountToMove.y < 0 && (tarSpeed > 0 || tarSpeed < 0))
            {
                amountToMove.y = -gravity * 6 * Time.deltaTime;
            }
            tarSpeed = 0;
            curSpeed = 0;
        }
        else if (playerPhysics.DashJumping)
        {
            anim.SetBool("IsJump", true);
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsDash", false);
            if (dir != playerPhysics.DashDirection)
            {
                playerPhysics.DashJumping = false;
            }
            amountToMove.x = playerPhysics.DashForce * playerPhysics.DashDirection;
        }
        else if (Input.GetKeyDown(KeyCode.X) && (playerPhysics.Grounded || playerPhysics.sloped) && playerPhysics.Dash)
        {
            transform.Translate(Vector2.up * 0.2F);
            playerPhysics.Dash = false;
            playerPhysics.DashJumping = true;
            playerPhysics.MidAirDashUsed = true;
            amountToMove.y = jumpHeight;
        }


        playerPhysics.Move(amountToMove * Time.deltaTime);
    }

    private float IncrementTowards(float n, float target, float a)
    {
        if (n == target)
        {
            return n;
        }
        else
        {
            float dir = Mathf.Sign(target - n);
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Wall")
        {
            gameObject.transform.Translate(Vector3.up * 3 * Time.deltaTime);
            playerPhysics.Grounded = true;
        }
    }
}
