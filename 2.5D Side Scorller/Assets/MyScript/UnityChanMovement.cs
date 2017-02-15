using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnityChanMovement : MonoBehaviour
{

    private Animator anim;
    private Rigidbody rgb;

    bool stop;

    bool grounded = false;
    bool walled = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHieght;

    // ダッシュ速度
    public const float forwardRightDushSpeed = 14.0f;
    public const float forwardLeftDushSpeed = -14.0f;
    // 移動速度
    public float speed;

    // 歩く時のフラグ
    private bool walkFlag;
    // ダッシュするときのフラグ
    private bool dushFlag;
    // ジャンプフラグ
    private bool jumpFlag;
    // 壁とのあたり判定フラグ
    private bool collisionWallFlag;

    private int direction;

    //  private bool notLeftWalkFlag;
    //  private bool notRightWalkFlag;


   // private CharacterController controller;
   // private float gravity = 25f;



    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rgb = GetComponent<Rigidbody>();
        //    moveState     = 0;
        //    timeer        = 0;
        walkFlag = false;
        dushFlag = false;
        jumpFlag = false;
        direction = 0;
        collisionWallFlag = false;

        //     notLeftWalkFlag  = false;
        //     notRightWalkFlag = false;

     //   controller = GetComponent<CharacterController>();
    }





    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)/* && !notLeftWalkFlag*/)
        { 
            walkFlag = true;
            Debug.Log("左向き");
        }
        if (Input.GetKey(KeyCode.RightArrow)/* && !notRightWalkFlag*/)
        {
            walkFlag = true;
            Debug.Log("右向き");
        }

       // if (collisionWallFlag && direction == 1/*left*/)
       //     notLeftWalkFlag = true;
       //
       // if (collisionWallFlag && direction == 0/*right*/)
       //     notRightWalkFlag = true;




        // ダッシュ
        if (Input.GetKey(KeyCode.C))
        {
            if (direction == 0)
            {
                rgb.AddForce(Vector3.right * 15, ForceMode.Impulse);
                Debug.Log("RightDush");
            }
            if (direction == 1)
            {
                rgb.AddForce(Vector3.left * 15, ForceMode.Impulse);
                Debug.Log("LeftDush");
            }
        }

        // ジャンプ
        if (Input.GetKey(KeyCode.X) && grounded)
        {
            grounded = false;
            jumpFlag = true;
            anim.SetBool("grounded", false);
            //rgb.AddForce(new Vector3(0, jumpHieght, 0));
            rgb.AddForce(Vector3.up * 2.5f, ForceMode.Impulse);
            Debug.Log("Normal_Jump");
        }

        // 壁ジャンプ
        // 左入力右飛び
        if (Input.GetKey(KeyCode.X) && collisionWallFlag && Input.GetKey(KeyCode.LeftArrow))
        {
            grounded = false;
            jumpFlag = true;
            anim.SetBool("grounded", false);
            //rgb.AddForce(Vector3.up * 100, ForceMode.Impulse);
            //rgb.AddForce(Vector3.right * 70, ForceMode.Impulse);
            Debug.Log("wall jump    Left -> Right");

            Vector3 forwardMovement = new Vector3(-100.0f, 0f, 0.0f);
            rgb.velocity = forwardMovement * speed;
        }
        // 右入力左飛び
        if (Input.GetKey(KeyCode.X) && collisionWallFlag && Input.GetKey(KeyCode.RightArrow))
        {
            grounded = false;
            jumpFlag = true;
            anim.SetBool("grounded", false);
            //rgb.AddForce(Vector3.up * 100, ForceMode.Impulse);
            //rgb.AddForce(Vector3.left * 70, ForceMode.Impulse);

            Debug.Log("wall jump    Left <- Right");

            Vector3 forwardMovement = new Vector3(100.0f, 0f, 0.0f);
            rgb.velocity = forwardMovement * speed;


            //    GetComponent<Rigidbody2D>().velocity = new Vector2(speed * hit.normal.x, speed);
            //    movement.BaseSpeed = speed * hit.normal.x;
            //    transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;
            //}
            //else if (hit.collider != null && walljumpFlag)
            //    walljumpFlag=false;
        }

        // 歩くのをやめる
        if (Input.GetKeyUp(KeyCode.LeftArrow) && Input.GetKeyUp(KeyCode.RightArrow))
        {
            walkFlag = false;
            dushFlag = false;
        }
        // ダッシュやめる
        if (Input.GetKeyUp(KeyCode.C))
        {
            walkFlag = false;
            dushFlag = false;
        }


        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0) grounded = true;
        else grounded = false;



    }


    // FixedUpdate is called once per frame
    void FixedUpdate()
    {

        if (!stop)
        {
            float move = Input.GetAxis("Horizontal");
            anim.SetFloat("Speed", Mathf.Abs(move));

            // 通常移動
            if (walkFlag /*&& !collisionWallFlag*/)
            {
                GetComponent<Rigidbody>().velocity = new Vector2(move * speed, GetComponent<Rigidbody>().velocity.y);
                Debug.Log("Walk");
            }

            // Charcter direction
            if (move < 0)
            { // Left
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y + 270, 0));
                direction = 1;
            }
            if (move > 0)
            { // Right
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y - 270, 0));
                direction = 0;
            }
        }


        if (stop)
            anim.SetFloat("Speed", Mathf.Abs(0));


        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0)
        {
            grounded = true;
            jumpFlag = false;
        }
        else grounded = false;


        anim.SetBool("grounded", grounded);

    }



    public void setStop(bool aa)
    {
        stop = aa;
    }


    /*-------------------------------------------------------------------------*/
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
       // Debug.Log("CollisionEnter2_1");
        if (collision.gameObject.tag == "Wall")
        {
            collisionWallFlag = true;
            jumpFlag          = false;
       //     Debug.Log("CollisionEnter2_2");
        }
    }
    void OnCollisionExit(UnityEngine.Collision collision)
    {
        //Debug.Log("CollisionExit2_1");

        if (collision.gameObject.tag == "Wall")
        {
            collisionWallFlag = false;
            //   Debug.Log("CollisionExit2_2");
            //     notLeftWalkFlag = false;
            //     notRightWalkFlag = false;
            // 重力を上書き
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
    }
    void OnCollisionStay(UnityEngine.Collision collision)
    {
        //Debug.Log("CollisionStay_1");

        if (collision.gameObject.tag == "Wall")
        {
            collisionWallFlag = true;

            // 物体の停止
            //rgb.velocity = Vector3.zero; // 3Dの場合
            // 重力の影響も受けない
            //rgb.isKinematic = true;

            // 落下速度を遅くする
            // Vector3 forwardMovement = new Vector3(0.0f, -1.5f, 0.0f);
            // rgb.velocity = forwardMovement * speed;

            // 重力を上書き
            Physics.gravity = new Vector3(0, -2.81f, 0);

            //   Debug.Log("CollisionStay_2");


        }
    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    Debug.DrawRay(hit.point, hit.normal,Color.red, 1.25f);
    //}
}
