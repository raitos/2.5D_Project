using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerPhysics : MonoBehaviour {

    private Vector3 s;
    private Vector3 c;

    public float deltaY;
    public float deltaX;

    public LayerMask colMask;
    public LayerMask slopeMask;

    public float timeScale = 1;

    Controller ctrl;

    BoxCollider Collider;
    Vector2 ColSize;
    Vector3 ColHalf; //To reduce needles calultions

    public bool Grounded;
    public bool Roofed = false;
    public bool FacingWall = false;
    public bool sloped = false;

    float angleDir;

    public float DashForce;
    public float DashDirection;
    public bool Dash;
    public float DashMax = 0;
    public bool DashJumping = false;
    public bool MidAirDashUsed = false;

    public float angle; //SLOPE


    void Start()
    {
        Collider = GetComponent<BoxCollider>();
        ColSize = Collider.size;
        ColHalf = transform.right / 2;
    }

    public void Move(Vector2 moveAmount)
    {
        
        Grounded = false;
        FacingWall = false;
        Roofed = false;
        sloped = false;

        //MOVEMENT
        deltaY = moveAmount.y;
        deltaX = moveAmount.x;
        float dirX = Mathf.Sign(moveAmount.x);
        float dirY = Mathf.Sign(moveAmount.y);

        //RAYCASTING FOR OBSTACLES

        if (Physics.Raycast(transform.position, Vector2.down, dirY * -0.15F, colMask) || Physics.Raycast(transform.position + ColHalf, Vector2.down, dirY * -0.15F, colMask) || Physics.Raycast(transform.position - ColHalf, Vector2.down, dirY * -0.15F, colMask))
        {
            Grounded = true;
            DashJumping = false;
            MidAirDashUsed = false;
            deltaY = 0;
        }
        else if (Physics.Raycast(transform.position, Vector2.down, 0.15F, slopeMask) || Physics.Raycast(transform.position + ColHalf, Vector2.down, 0.15F, slopeMask) || Physics.Raycast(transform.position - ColHalf, Vector2.down, 0.15F, slopeMask))
        {
            sloped = true;
            DashJumping = false;
            MidAirDashUsed = false;
            angle = CalculateAngle();
            angleDir = Mathf.Sign(angle);
            deltaY = 0.5F * -angleDir * moveAmount.x; //Slope angle must 26.5, because im bad at math
        }
        else if (Physics.Raycast(transform.position, Vector2.up, dirY * 1.2F, colMask) || Physics.Raycast(transform.position + ColHalf, Vector2.up, dirY * 1.2F, colMask) || Physics.Raycast(transform.position - ColHalf, Vector2.up, dirY * 1.2F, colMask))
        {
            Roofed = true;
            deltaY = 0;
        }

        if (Physics.Raycast(transform.position, dirX * Vector2.right, 0.5F, colMask) || Physics.Raycast(transform.position + transform.up, dirX * Vector2.right, 0.5F, colMask) || Physics.Raycast(transform.position - transform.up * 0.001F, dirX * Vector2.right, 0.5F, colMask))
        {
            MidAirDashUsed = false;
            FacingWall = true;
            DashJumping = false;
            deltaX = 0;
        }


        if (Dash) //Dash stuff
        {
            if (Input.GetKeyUp(KeyCode.C))
            {
                Dash = false;
            }
            else if (DashMax < 0.5F && !Physics.Raycast(transform.position, Vector2.right * DashDirection, 0.5F))
            {
                deltaX = DashForce * DashDirection * Time.deltaTime;
                deltaY = 0;
                DashMax += Time.deltaTime;
                if (sloped)
                {
                    deltaY = 0.5F * -angleDir * DashDirection * DashForce * Time.deltaTime;
                }
            }
            else
            {
                Dash = false;
                DashMax = 0;
            }
        }

        Vector2 finalTransform = new Vector2(deltaX, deltaY);
        transform.Translate(finalTransform * timeScale);
    }

    float CalculateAngle()//SLOPES ARE CALCULATED HERE
    {
        RaycastHit hit1;
        RaycastHit hit2;
        if (Physics.Raycast(transform.position, Vector2.down, out hit1) && Physics.Raycast(transform.position + Vector3.right * 0.1F, Vector2.down, out hit2))
        {
            float a = Mathf.Atan2(hit1.normal.x, hit2.normal.y) * Mathf.Rad2Deg;
            return a;
        }
        else
        {
            return 0;
        }
    }
}
