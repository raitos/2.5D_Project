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

    GameObject animatedObj;

    public Animator anim;

    private float curSpeed;
    private float tarSpeed;
    private Vector2 amountToMove;

    public float DashCoolDown = 1;
    bool DashCD = false;
    float dashT = 0;

    PlayerPhysics playerPhysics;

    void Start()
    {
        animatedObj = GameObject.Find("ToonShader_SD_unitychan_humanoid");
        playerPhysics = GetComponent<PlayerPhysics>();
        anim = animatedObj.GetComponent<Animator>();
    }

    void Update()
    {
        //Animator things and direction detection
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


        //Dash-------------

        if (Input.GetKey(KeyCode.C) && !DashCD)
        {
            playerPhysics.Dash = true;
            DashCD = true;
            dashT = 0;
            playerPhysics.DashDirection = dir;
            amountToMove.y = 0;
        }
        if (DashCD)
        {
            dashT += Time.deltaTime;
            if (dashT > DashCoolDown)
            {
                DashCD = false;
            }

        }
        //-----------------


        tarSpeed = Input.GetAxis("Horizontal") * speed;
        //curSpeed = IncrementTowards(curSpeed, tarSpeed, acceleration);


        amountToMove.x = tarSpeed; //For acceleration use "curSpeed" instead of "tarSpeed"

        if (playerPhysics.Roofed)
        {
            amountToMove.y = 0;
        }

        if ((playerPhysics.Grounded || playerPhysics.sloped)) //!!!!!!!!!?????????
        {
            amountToMove.y = -0.01F;
            if (Input.GetKeyDown(KeyCode.X))
            {
                amountToMove.y = jumpHeight;
            }
        }
        else
        {
            amountToMove.y -= gravity * Time.deltaTime;
        }
        if (playerPhysics.FacingWall)
        {
            if (Input.GetKeyDown(KeyCode.X) && amountToMove.y < 0)
            {
                amountToMove.y = jumpHeight;
            }
            else if (amountToMove.y < 0)
            {
                amountToMove.y = -gravity * 6 * Time.deltaTime;
            }
            tarSpeed = 0;
            curSpeed = 0;
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
