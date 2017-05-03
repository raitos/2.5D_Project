using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroMovement : MonoBehaviour {

    public float maxSpeed;
    bool facingRight = true;

    private Rigidbody2D rb2d;

    Animator anim;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent < Rigidbody2D >();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        


        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));

        rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);

        if (move >0 && !facingRight)
        {
            flip();
        }
        else if (move <0 && facingRight)
        {
            flip();
        }
    }

    private void Update()
    {
        
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
