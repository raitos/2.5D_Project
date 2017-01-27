using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CaracterMovement : MonoBehaviour
{

    public Text countText;
    public Text winText;
    private Rigidbody rb;


    // 前進速度
    public const float forwardSpeed       = 1.0f;
    // 前方ダッシュ速度
    public const float forwardDushSpeed   = 3.0f;
    // 後退速度
    public const float backwardSpeed      = -0.5f;
    // ジャンプ威力
    public const float jumpPower          = 0.6f;
    // 移動速度
    public float speed;
    // 当たる度カウントする
    private int collisionCount;
    public float x, y, z;
    // ダッシュキーが押された時のフラグ
    private int dushCount;
    // ジャンプ回数制限
    private int jumpCount;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collisionCount = 0;
        SetCountText();
        winText.text = "";

        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

        dushCount = 0;
        jumpCount = 0;
    }

    void FixedUpdate()
    {// キーボード操作は書かないほうが良い
             
    }

    void Update()
    {// Rigigdbodyの処理は書かないほうが良い
     //float moveHorizontal = Input.GetAxis("Horizontal");
     //float moveVertical = Input.GetAxis("Vertical"); 
     //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
     //rb.AddForce(movement * speed);
        MoveMent();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            collisionCount = collisionCount + 1;
            SetCountText();
        }
    }


    void SetCountText()
    {
        countText.text = "Count: " + collisionCount.ToString();
        if (collisionCount >= 4)
            winText.text = "You Win!";
    }
    

    void MoveMent()
    {
        Vector3 pos = this.transform.position;

   
        // 通常移動
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //rb.GetComponent<Rigidbody2D>().gravityScale = 15;
            Vector3 forwardMovement = new Vector3(forwardSpeed, 0.0f, 0.0f);
            rb.velocity = forwardMovement * speed;
            dushCount++;
        }
        // ダッシュ
        if (dushCount>=2)
        {
            Vector3 forwardMovement = new Vector3(0.0f, 0.0f, 0.0f);
            rb.velocity = forwardMovement * speed;
            dushCount = 0;
        }

        // ジャンプ
        if (Input.GetKey(KeyCode.UpArrow) || jumpCount>=2)
        {
            //rb.GetComponent<Rigidbody2D>().gravityScale = 15;
            Vector3 forwardMovement = new Vector3(0.0f, jumpPower, 0.0f);
            rb.velocity = forwardMovement * speed;
            jumpCount++;
        }
           

        // 後退
        if (Input.GetKey(KeyCode.LeftArrow))
        {
           //rb.GetComponent<Rigidbody2D>().gravityScale = 15;
            Vector3 backwardMovement = new Vector3(backwardSpeed, 0.0f, 0.0f);
            rb.velocity = backwardMovement * speed;
        }
      


        // キーを離したとき
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            //Debug.Log("キーを離した");
            //dushCount = false;
            rb.velocity = Vector3.zero;
            //rb.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
   
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rb.velocity = Vector3.zero;
        }

        this.transform.position = pos;

        if (jumpCount >= 2)
            jumpCount = 0;

    }
}