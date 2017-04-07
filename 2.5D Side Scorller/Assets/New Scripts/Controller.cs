using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class Controller : MonoBehaviour {

    public float gravity;
    public float speed;
    public float acceleration;
    public float jumpHeight;
    int dir = 1;
    int MoveNull = 1;

    GameObject animatedObj;

    public float DashForce;
    public float dashEnergy = 1;
    public Animator anim;

    private float curSpeed;
    private float tarSpeed;
    private Vector2 amountToMove;

    bool Dashing = false;
    bool DashAvailable = true;
    bool DashCD = false;

    PlayerPhysics playerPhysics;

    void Start()
    {
        animatedObj = GameObject.Find("ToonShader_SD_unitychan_humanoid");
        playerPhysics = GetComponent<PlayerPhysics>();
        anim = animatedObj.GetComponent<Animator>();
    }

    void Update()
    {
        //Animator things
        anim.SetFloat("Speed", Mathf.Abs(tarSpeed));
        if (tarSpeed < 0)
        { // Left
            animatedObj.transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y - 90, 0));
            dir = -1;
        }
        else if (tarSpeed > 0)
        { // Right
            animatedObj.transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y + 90, 0));
            dir = 1;
        }
        //---------------

        if (playerPhysics.Grounded || playerPhysics.sloped)
        {
            amountToMove.y = 0;
            if (Input.GetKeyDown(KeyCode.X))
            {
                amountToMove.y = jumpHeight;
            }
        }

        if (playerPhysics.FacingWall)
        {
            tarSpeed = 0;
            curSpeed = 0;
        }


        //Dashing things----------

        if ((Input.GetKey(KeyCode.C) && DashAvailable))
        {
            playerPhysics.DashForce = DashForce * Time.deltaTime;
            playerPhysics.DashDirection = dir;
            playerPhysics.MovementNullifier = 0;
            MoveNull = 0;
            Dashing = true;
            amountToMove.y = 0;
            dashEnergy -= Time.deltaTime;
            if (dashEnergy < 1)
            {
                DashAvailable = false;
            }
        }
        else if (dashEnergy < 1.32F)
        {
            playerPhysics.DashForce = 0;
            playerPhysics.MovementNullifier = 1;
            MoveNull = 1;
            Dashing = false;
            DashCD = false;
            dashEnergy += Time.deltaTime;
            if (dashEnergy > 1.2)
            {
                DashCD = true;
            }
        }
        if (DashCD && (playerPhysics.Grounded || playerPhysics.sloped))
        {
            DashAvailable = true;
        }
        //------------------------


        tarSpeed = Input.GetAxis("Horizontal") * speed;
        curSpeed = IncrementTowards(curSpeed, tarSpeed, acceleration);

        amountToMove.x = tarSpeed * MoveNull; //For acceleration use "curSpeed" instead of "tarSpeed"

        amountToMove.x = tarSpeed;

        if (playerPhysics.Roofed)
        {
            amountToMove.y = 0;
        }

        if ((!playerPhysics.Grounded || !playerPhysics.sloped) && !Dashing) //!!!!!!!!!?????????
        {
            amountToMove.y -= gravity * Time.deltaTime;
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
}
