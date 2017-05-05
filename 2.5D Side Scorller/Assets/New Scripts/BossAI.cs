using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour {


    //Game objects--------------------
    GameObject player;
    GameObject bossArea;
    GameObject ReflectShield;

    //Vectors-------------------------
    Vector3 playerPos;
    Vector3 rightSide;
    Vector3 leftSide;
    Vector3 rightBottom;
    Vector3 leftBottom;
    Vector3 rightTop;
    Vector3 leftTop;
    Vector3 goalPos;
    Vector3 goalPos2;
    Vector3 goalPos3;
    Vector3 goalPos4;
    Vector3 startPos;


    //Player positions----------------
    public bool playerLeftWall;
    public bool playerRightWall;
    bool playerLeft;
    bool playerRight;
    bool bossLeft;
    bool bossRight;

    //Timers--------------------------
    float Slash1Timer = 0;
    float NextAttTimer = 0;
    public float AttTime = 1;
    float IdleTimer;

    //Boss abilities------------------
    bool SlashActive = false;
    bool Slash2Active = false;
    bool SlashDash = false;
    bool SlashDash2 = false;

    //Some variables------------------
    public float BossHP = 100;
    public TextMesh HPText;

    //Attack bools--------------------
    bool firstPoint = false;
    bool secondPoint = false;
    bool thirdPoint = false;
    //--------------------------------
    void Start ()
    {
        HPText = GameObject.Find("txtBossHealth").GetComponent<TextMesh>();
        player = GameObject.Find("Player");
        bossArea = GameObject.Find("BossArea");
        rightSide = GameObject.Find("RightSide").transform.position;
        leftSide = GameObject.Find("LeftSide").transform.position;
        ReflectShield = GameObject.Find("ReflectShield");
        ReflectShield.SetActive(false);
        leftBottom.x = leftSide.x;
        leftBottom.y = leftSide.y - 4;
        rightBottom.x = rightSide.x;
        rightBottom.y = rightSide.y - 4;
        leftTop.x = leftSide.x;
        leftTop.y = leftSide.y + 4;
        rightTop.x = rightSide.x;
        rightTop.y = rightSide.y + 4;
        goalPos = transform.position;
        startPos = transform.position;
	}

	void Update ()
    {
        playerPos = player.transform.position;
        if (playerPos.x > bossArea.transform.position.x)
        {
            playerRight = true;
            playerLeft = false;
        }
        else
        {
            playerLeft = true;
            playerRight = false;
        }
        if (transform.position.x < bossArea.transform.position.x)
        {
            bossLeft = true;
            bossRight = false;
        }
        else
        {
            bossLeft = false;
            bossRight = true;
        }

        //Health things:
        if (BossHP < 1)
        {
            Destroy(gameObject);
        }

        //Randomly choosing the next ability:
        System.Random rnd = new System.Random();
        if (!SlashActive && !Slash2Active && !SlashDash && !SlashDash2)
        {
            if (NextAttTimer < AttTime)
            {
                NextAttTimer += Time.deltaTime;
            }
            else
            {
                int nextMove = rnd.Next(1, 4);
                switch (nextMove)
                {
                    case 1:
                        ReflectShield.SetActive(false);
                        SlashActive = true;
                        NextAttTimer = 0;
                        break;
                    case 2:
                        ReflectShield.SetActive(false);
                        Slash2Active = true;
                        NextAttTimer = 0;
                        break;
                    case 3:
                        ReflectShield.SetActive(false);
                        if (playerLeft)
                        {
                            goalPos = leftBottom;
                        }
                        else
                        {
                            goalPos = rightBottom;
                        }
                        SlashDash = true;
                        NextAttTimer = 0;
                        break;
                    case 4:
                        ReflectShield.SetActive(false);
                        if (playerLeft)
                        {
                            goalPos = leftBottom;
                        }
                        else
                        {
                            goalPos = rightBottom;
                        }
                        SlashDash2 = true;
                        NextAttTimer = 0;
                        break;
                }
            }
        }

        //Check which ability is active:
        if (SlashActive)
        {
            SlashAtt();
        }

        else if (Slash2Active)
        {
            SlashAtt2();
        }
        else if (SlashDash)
        {
            DashAtt();
        }
        else if (SlashDash2)
        {
            DashAtt2();
        }


    }

    bool dashDown;

    public void AddDmg(float dmg)
    {
        BossHP -= dmg;
        HPText.text = BossHP.ToString();
    }

    void SlashAtt()
    {

        if (Slash1Timer < 0.2F)
        {
            if (!firstPoint)
            {
                if (playerLeft && (!playerRightWall && !playerLeftWall))
                {
                    goalPos = playerPos;
                    goalPos.y += 1F;
                    goalPos.x = playerPos.x - 1.5F;
                }
                else if (playerRight && (!playerRightWall && !playerLeftWall))
                {
                    goalPos = playerPos;
                    goalPos.y += 1F;
                    goalPos.x = playerPos.x + 1.5F;
                }
                else if (playerLeftWall)
                {
                    goalPos = leftTop;
                    dashDown = true;
                }
                else
                {
                    goalPos = rightTop;
                    dashDown = true;
                }
                goalPos2 = goalPos;
                goalPos2.y -= 2.5F;
                firstPoint = true;
            }
            transform.position = goalPos;
            Slash1Timer += Time.deltaTime;

        }

        else if (Slash1Timer < 2)
        {
            Slash1Timer += Time.deltaTime;
            if (dashDown && bossLeft)
            {
                transform.position = Vector3.MoveTowards(transform.position, leftBottom, 30 * Time.deltaTime);
            }
            else if (dashDown && bossRight)
            {
                transform.position = Vector3.MoveTowards(transform.position, rightBottom, 30 * Time.deltaTime);
            }
            else if (transform.position.y > bossArea.transform.position.y -3.5F)
            {
                transform.position = Vector3.MoveTowards(transform.position, goalPos2, 30 * Time.deltaTime);
            }
        }
        else
        {
            transform.position = startPos;
            SlashActive = false;
            firstPoint = false;
            dashDown = false;
            Slash1Timer = 0;
            ReflectShield.SetActive(true);
        }

    }

    void DashAtt()
    {
        if (Slash1Timer < 0.5F)
        {
            transform.position = goalPos;
            Slash1Timer += Time.deltaTime;

        }
        else if (Slash1Timer < 1.6F && goalPos == leftBottom)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightBottom, 60 * Time.deltaTime);
            Slash1Timer += Time.deltaTime;
        }
        else if (Slash1Timer < 1.6F && goalPos == rightBottom)
        {
            transform.position = Vector3.MoveTowards(transform.position, leftBottom, 60 * Time.deltaTime);
            Slash1Timer += Time.deltaTime;
        }
        else
        {
            transform.position = startPos;
            SlashDash = false;
            Slash1Timer = 0;
            ReflectShield.SetActive(true);
        }
    }


    void SlashAtt2()
    {
        if (Slash2Active)
        {
            if (Slash1Timer < 1)
            {
                if (!firstPoint)
                {
                    if (playerLeft && (!playerRightWall && !playerLeftWall))
                    {
                        goalPos = playerPos;
                        goalPos.y += 1F;
                        goalPos.x = playerPos.x - 1.5F;
                    }
                    else if (playerRight && (!playerRightWall && !playerLeftWall))
                    {
                        goalPos = playerPos;
                        goalPos.y += 1F;
                        goalPos.x = playerPos.x + 1.5F;
                    }
                    else
                    {
                        goalPos.x = player.transform.position.x;
                        goalPos.y = playerPos.y + 2;
                    }
                    firstPoint = true;
                }
                transform.position = goalPos;
                Slash1Timer += Time.deltaTime;

            }
            else if (Slash1Timer < 2)
            {
                if (!secondPoint)
                {
                    if (playerLeft && (!playerRightWall && !playerLeftWall))
                    {
                        goalPos = playerPos;
                        goalPos.y += 1F;
                        goalPos.x = playerPos.x - 1.5F;
                    }
                    else if (playerRight && (!playerRightWall && !playerLeftWall))
                    {
                        goalPos = playerPos;
                        goalPos.y += 1F;
                        goalPos.x = playerPos.x + 1.5F;
                    }
                    else
                    {
                        goalPos.x = player.transform.position.x;
                        goalPos.y = playerPos.y + 2;
                    }
                    secondPoint = true;
                }
                transform.position = goalPos;
                Slash1Timer += Time.deltaTime;

            }
            else if (Slash1Timer < 4)
            {
                if (!thirdPoint)
                {
                    if (playerLeft && (!playerRightWall && !playerLeftWall))
                    {
                        goalPos = playerPos;
                        goalPos.y += 1F;
                        goalPos.x = playerPos.x - 1.5F;
                    }
                    else if (playerRight && (!playerRightWall && !playerLeftWall))
                    {
                        goalPos = playerPos;
                        goalPos.y += 1F;
                        goalPos.x = playerPos.x + 1.5F;
                    }
                    else
                    {
                        goalPos.x = player.transform.position.x;
                        goalPos.y = playerPos.y + 2;
                    }
                    thirdPoint = true;
                }
                transform.position = goalPos;
                Slash1Timer += Time.deltaTime;

            }
            else
            {
                transform.position = startPos;
                Slash2Active = false;
                firstPoint = false;
                secondPoint = false;
                thirdPoint = false;
                Slash1Timer = 0;
                ReflectShield.SetActive(true);
            }
        }
    }

    void DashAtt2()
    {
        if (Slash1Timer < 0.5F)
        {
            transform.position = goalPos;
            Slash1Timer += Time.deltaTime;

        }
        else if (Slash1Timer < 1F && goalPos == leftBottom)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightTop, 60 * Time.deltaTime);
            Slash1Timer += Time.deltaTime;
        }
        else if (Slash1Timer < 1F && goalPos == rightBottom)
        {
            transform.position = Vector3.MoveTowards(transform.position, leftTop, 60 * Time.deltaTime);
            Slash1Timer += Time.deltaTime;
        }
        else if (Slash1Timer < 1.3F && goalPos == leftBottom)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightBottom, 60 * Time.deltaTime);
            Slash1Timer += Time.deltaTime;
        }
        else if (Slash1Timer < 1.3F && goalPos == rightBottom)
        {
            transform.position = Vector3.MoveTowards(transform.position, leftBottom, 60 * Time.deltaTime);
            Slash1Timer += Time.deltaTime;
        }
        else if (Slash1Timer < 1.8 && goalPos == leftBottom)
        {
            transform.position = Vector3.MoveTowards(transform.position, leftTop, 60 * Time.deltaTime);
            Slash1Timer += Time.deltaTime;
        }
        else if (Slash1Timer < 1.8 && goalPos == rightBottom)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightTop, 60 * Time.deltaTime);
            Slash1Timer += Time.deltaTime;
        }
        else if (Slash1Timer < 3 && goalPos == leftBottom)
        {
            transform.position = Vector3.MoveTowards(transform.position, leftBottom, 60 * Time.deltaTime);
            Slash1Timer += Time.deltaTime;
        }
        else if (Slash1Timer < 3 && goalPos == rightBottom)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightBottom, 60 * Time.deltaTime);
            Slash1Timer += Time.deltaTime;
        }
        else
        {
            transform.position = startPos;
            SlashDash2 = false;
            Slash1Timer = 0;
            ReflectShield.SetActive(true);
        }
    }


}
