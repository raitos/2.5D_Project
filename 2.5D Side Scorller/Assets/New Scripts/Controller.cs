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
    int i = 1;

    GameObject animatedObj;
<<<<<<< HEAD
    Animator anim;

    public float DashForce;
    public float dashEnergy = 1;
=======
    public Animator anim;
>>>>>>> 08fbf77c3ff2f8a509225eab2b668ec65c1123be

    private float curSpeed;
    private float tarSpeed;
    private Vector2 amountToMove;

    bool Dashing = false;

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

        //Dashing things
        if (Input.GetKeyDown(KeyCode.C) && !playerPhysics.FacingWall && dashEnergy > 1)
        {
            if (Input.GetKey(KeyCode.C))
            {
                playerPhysics.f = DashForce * Time.deltaTime;
                playerPhysics.d = dir;
                playerPhysics.t = 0;
                i = 0;
                Dashing = true;
                amountToMove.y = 0;
            }
            dashEnergy--;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            playerPhysics.f = 0;
            playerPhysics.t = 1;
            i = 1;
            Dashing = false;
        }
        if (!Dashing && dashEnergy < 2)
        {
            dashEnergy += Time.deltaTime;
        }
        //--------------
        tarSpeed = Input.GetAxis("Horizontal") * speed;
        curSpeed = IncrementTowards(curSpeed, tarSpeed, acceleration);

<<<<<<< HEAD
        amountToMove.x = tarSpeed * i; //For acceleration use "curSpeed" instead of "tarSpeed"
=======
        //Animator things
        anim.SetFloat("Speed", Mathf.Abs(tarSpeed));
        if (Mathf.Sign(tarSpeed) < 0)
        { // Left
            animatedObj.transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y + 270, 0));
        }
        if (Mathf.Sign(tarSpeed) > 0)
        { // Right
            animatedObj.transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y - 270, 0));
        }
        //---------------

        amountToMove.x = tarSpeed;
>>>>>>> 08fbf77c3ff2f8a509225eab2b668ec65c1123be

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
