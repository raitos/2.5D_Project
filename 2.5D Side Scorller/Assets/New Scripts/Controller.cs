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

    GameObject animatedObj;
    public Animator anim;

    private float curSpeed;
    private float tarSpeed;
    private Vector2 amountToMove;

    PlayerPhysics playerPhysics;

    void Start()
    {
        animatedObj = GameObject.Find("ToonShader_SD_unitychan_humanoid");
        playerPhysics = GetComponent<PlayerPhysics>();
        anim = animatedObj.GetComponent<Animator>();
    }

    void Update()
    {

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

        tarSpeed = Input.GetAxis("Horizontal") * speed;
        curSpeed = IncrementTowards(curSpeed, tarSpeed, acceleration);

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

        if (playerPhysics.Roofed)
        {
            amountToMove.y = 0;
        }

        if (!playerPhysics.Grounded || !playerPhysics.sloped) //!!!!!!!!!!!?????????
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
