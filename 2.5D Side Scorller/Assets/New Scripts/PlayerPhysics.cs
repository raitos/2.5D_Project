using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerPhysics : MonoBehaviour {

    private BoxCollider collider;
    private Vector3 s;
    private Vector3 c;

    public float deltaY;
    public float deltaX;

    public LayerMask colMask;
    public LayerMask slopeMask;

    public bool Grounded;
    public bool Roofed = false;
    public bool FacingWall = false;
    public bool sloped = false;

    public float angle; //SLOPE

    Ray ray;


    void Start()
    {
        collider = GetComponent<BoxCollider>();
        s = collider.size;
        c = collider.center;
    }

    public void Move(Vector2 moveAmount)
    {

        Grounded = false;
        FacingWall = false;
        Roofed = false;
        sloped = false;

        //SLOPES ARE CALCULATED HERE
        RaycastHit hit1;
        RaycastHit hit2;
        if (Physics.Raycast(transform.position, Vector2.down, out hit1) && Physics.Raycast(transform.position +  Vector3.right * 0.1F, Vector2.down, out hit2))
        {
            angle = Mathf.Atan2(hit1.normal.x, hit2.normal.y) * Mathf.Rad2Deg;
        }

        //MOVEMENT
        deltaY = moveAmount.y;
        deltaX = moveAmount.x;
        Vector2 p = transform.position;
        float dirX = Mathf.Sign(moveAmount.x);
        float dirY = Mathf.Sign(moveAmount.y);


        //RAYCASTING FOR OBSTACLES

        if (Physics.Raycast(transform.position, Vector2.down, -dirY * 0.15F, colMask) || Physics.Raycast(transform.position + transform.right / 2, Vector2.down, -dirY * 0.15F, colMask) || Physics.Raycast(transform.position - transform.right / 2, Vector2.down, -dirY * 0.15F, colMask))
        {
            Grounded = true;
            deltaY = 0;
        }
        else if (Physics.Raycast(transform.position, Vector2.down, -dirY * 0.15F, slopeMask) || Physics.Raycast(transform.position + transform.right / 2, Vector2.down, -dirY * 0.15F, slopeMask) || Physics.Raycast(transform.position - transform.right / 2, Vector2.down, -dirY * 0.15F, slopeMask))
        {
            sloped = true;
            deltaY = 0;
        }
        else if (Physics.Raycast(transform.position, Vector2.up, dirY * 1.2F, colMask) || Physics.Raycast(transform.position + transform.right / 2, Vector2.up, dirY * 1.2F, colMask) || Physics.Raycast(transform.position - transform.right / 2, Vector2.up, dirY * 1.2F, colMask))
        {
            Roofed = true;
            deltaY = 0;
        }

        if (Physics.Raycast(transform.position, Vector2.right, dirX / 2, colMask) || Physics.Raycast(transform.position + transform.up, Vector2.right, dirX / 2, colMask) || Physics.Raycast(transform.position - transform.up * 0.001F, Vector2.right, dirX / 2, colMask))
        {
            FacingWall = true;
            deltaX = 0;
        }
        else if (Physics.Raycast(transform.position, Vector2.left, -dirX / 2, colMask) || Physics.Raycast(transform.position + transform.up, Vector2.left, -dirX / 2, colMask) || Physics.Raycast(transform.position - transform.up * 0.001F, Vector2.left, -dirX / 2, colMask))
        {
            FacingWall = true;
            deltaX = 0;
        }
        
        if ((deltaX > 0 || deltaX < 0) && sloped)
        {
            float angleDir = Mathf.Sign(angle);
            deltaY = 0.5F * -angleDir * moveAmount.x;
        }
        


        Vector2 finalTransform = new Vector2(deltaX, deltaY);
        transform.Translate(finalTransform);
    }
}
