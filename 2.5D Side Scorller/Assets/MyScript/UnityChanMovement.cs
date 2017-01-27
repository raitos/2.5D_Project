using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanMovement : MonoBehaviour {

    private Animator anim;
    private Rigidbody rgb;

    public float playerWalkSpeed = 5.0f;
    bool stop;

    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHieght;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rgb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {

        if (grounded && Input.GetKey(KeyCode.X))
        {
            grounded = false;
            anim.SetBool("grounded", false);
            rgb.AddForce(new Vector3(0, jumpHieght, 0));
        }

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0) grounded = true;
        else grounded = false;

        anim.SetBool("grounded", grounded);

        if (!stop)
        {
            float move = Input.GetAxis("Horizontal");
            anim.SetFloat("Speed", Mathf.Abs(move));
            GetComponent<Rigidbody>().velocity = new Vector2(move * playerWalkSpeed, GetComponent<Rigidbody>().velocity.y);
            if (move < 0)
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y + 270, 0));
            if (move > 0)
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y - 270, 0));
        }
        if (stop)
        {
            anim.SetFloat("Speed", Mathf.Abs(0));
        }

    }


    public void setStop(bool aa)
    {
        stop = aa;
    }

}
