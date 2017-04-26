using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Defender : Enemy {

    GameObject TheEnemy;
    GameObject Player;
    public float DamageTaken;
    public Animator EnemyAnimator;
    public GameObject ObstacleBox;
    public GameObject ObstacleWall;
    public EnemySpawner EnemyList;
    GameObject[] TheListOfEnemies;




    // 0 behind, 1 front, 2 down , 3 Player Directions. 
    Ray[] Directions;
    RaycastHit[] Hits0;
    RaycastHit[] Hits1;
    RaycastHit[] Hits3;
    RaycastHit[] CurrentHits0;
    RaycastHit[] CurrentHits1;
    RaycastHit[] CurrentHits3;

    public bool flipped;
    bool moveTowardsPlayerPos;
    bool moveTowardsPlayerNeg;
    public bool Autoconfigure = false;
    public bool move;
    bool jumpRight;
    bool PlayerRight;
    bool PlayerLeft;
    bool turn;
    bool grounded;
    bool jumpLeft;
    bool shoot;
    bool ValuesNotNull;
    bool SetDirectionHits;
    bool Hits0Run;
    bool Hits1Run;
    bool Hits3Run;
    bool[] EnemiesDestroyed;



    public float TimeToTurn;
    public float TimeToFall;
    public float TimeToShoot;
    float ResetTurn;
    float ResetFall;
    float ResetShoot;
    float time;
    public float EnemySpeed;
    public float JumpSpeed;
    public float BulletSpeed;
    public float bulletLength;
    public float SpaceToFire;
    float[] Values;
    int hitCounter0;
    int hitCounter1;
    int hitCounter3;

    public void Awake()
    {
#if UNITY_EDITOR

        if (!EditorApplication.isPlaying && Autoconfigure == true)
        {
            EnemyList = GameObject.Find("Spawner").GetComponent<EnemySpawner>();

            TimeToTurn = 0.25f;
            TimeToFall = 1f;
            TimeToShoot = 1f;
            EnemySpeed = 0.02f;
            JumpSpeed = 0.12f;
            BulletSpeed = 20f;
            bulletLength = 0.5f;
            SpaceToFire = 5;


        }

#endif
    }
    // Use this for initialization
    void Start () {
        Hits0Run = false;
        Hits1Run = false;
        Hits3Run = false;
        hitCounter0 = 0;
        hitCounter1 = 0;
        hitCounter3 = 0;
        SetDirectionHits = true;
        ValuesNotNull = false;
        TheListOfEnemies = EnemyList.ListOfEnemies;
        ResetFall = TimeToFall;
        ResetTurn = TimeToTurn;
        ResetShoot = TimeToShoot;
        time = Time.deltaTime;
        shoot = false;
        flipped = false;
        jumpLeft = false;
        jumpRight = false;
        grounded = false;
        moveTowardsPlayerPos = false;
        moveTowardsPlayerNeg = false;
        TheEnemy = this.gameObject;
        Directions = new Ray[4];
        Values = new float[8];
        Player = GameObject.FindWithTag("Player");
        hitdamage = DamageTaken;
    }
	
	// Update is called once per frame
	void Update () {
        if (EditorApplication.isPlaying)
        {


            Hits0 = Physics.RaycastAll(Directions[0], 2f);
            Hits3 = Physics.RaycastAll(Directions[3], 5f);
            Hits1 = Physics.RaycastAll(Directions[1], 1.5f);

            if (SetDirectionHits)
            {

                CurrentHits0 = Hits0;
                CurrentHits1 = Hits1;
                CurrentHits3 = Hits3;
                Hits0Run = false;
                Hits1Run = false;
                Hits3Run = false;
                hitCounter0 = Hits0.Length;
                hitCounter1 = Hits1.Length;
                hitCounter3 = Hits3.Length;
                SetDirectionHits = false;
            }
            if (flipped == false)
            {

                Directions[0].origin = TheEnemy.transform.position;
                Directions[0].direction = new Vector3(-1, 0, 0);
                Directions[1].origin = TheEnemy.transform.position;
                Directions[1].direction = new Vector3(1, 0, 0);
                Directions[2].origin = new Vector3(TheEnemy.transform.position.x, TheEnemy.transform.position.y - TheEnemy.GetComponent<CapsuleCollider>().bounds.extents.y, TheEnemy.transform.position.z);
                Directions[2].direction = new Vector3(0, -1, 0);
                Directions[3].origin = TheEnemy.transform.position;
                Directions[3].direction = (-TheEnemy.transform.position + Player.transform.position).normalized;

            }
            else if (flipped == true)
            {
                Directions[0].origin = TheEnemy.transform.position;
                Directions[0].direction = new Vector3(1, 0, 0);
                Directions[1].origin = TheEnemy.transform.position;
                Directions[1].direction = new Vector3(-1, 0, 0);
                Directions[2].origin = new Vector3(TheEnemy.transform.position.x, TheEnemy.transform.position.y - TheEnemy.GetComponent<CapsuleCollider>().bounds.extents.y, TheEnemy.transform.position.z);
                Directions[2].direction = new Vector3(0, -1, 0);
                Directions[3].origin = TheEnemy.transform.position;
                Directions[3].direction = (-TheEnemy.transform.position + Player.transform.position).normalized;

            }

            if (SetDirectionHits == false && !Hits0Run && !Hits1Run && !Hits3Run)
            {
                //Hits Direction 0
                Debug.Log(" Hits0: " + Hits0.Length);
                hitCounter0--;

                if (hitCounter0 > -1 && CurrentHits0 != null && TheEnemy != null && CurrentHits0[hitCounter0].transform.gameObject != TheEnemy)
                {


                    if (CurrentHits0[hitCounter0].transform.gameObject != null)
                    {

                        if (CurrentHits0[hitCounter0].transform.gameObject == Player && flipped == false)
                        {
                            TheEnemy.transform.rotation = Quaternion.Euler(new Vector3(0, Player.transform.rotation.eulerAngles.y - 270, 0));
                            flipped = true;
                            move = false;
                            Hits0Run = true;

                        }
                        else if (CurrentHits0[hitCounter0].transform.gameObject == Player && flipped == true)
                        {
                            TheEnemy.transform.rotation = Quaternion.Euler(new Vector3(0, Player.transform.rotation.eulerAngles.y + 270, 0));
                            flipped = false;
                            move = false;
                            Hits0Run = true;

                        }
                        else
                        {
                            Hits0Run = true;
                        }
                    }
                    else
                    {
                        Hits0Run = true;
                    }


                }
                else
                {

                    Hits0Run = true;
                }



                Debug.Log(" Hits3: " + CurrentHits3.Length);
                //Hits Direction 3

                hitCounter3--;
                if (hitCounter3 > -1 && CurrentHits3 != null && TheEnemy != null)
                {



                    if (Player.transform.position.x <= TheEnemy.transform.position.x + SpaceToFire && Player.transform.position.x >= TheEnemy.transform.position.x && flipped == false && TheEnemy != null)
                    {
                        move = false;
                        moveTowardsPlayerPos = false;
                        PlayerLeft = false;
                        PlayerRight = true;
                        TimeToShoot = TimeToShoot - time;

                        if (TimeToShoot < 0)
                        {
                            shoot = true;
                            TimeToShoot = ResetShoot;
                        }
                        Hits3Run = true;
                    }
                    else if (Player.transform.position.x <= TheEnemy.transform.position.x && Player.transform.position.x >= TheEnemy.transform.position.x - SpaceToFire && flipped == true && TheEnemy != null)
                    {
                        move = false;
                        moveTowardsPlayerNeg = false;
                        PlayerRight = false;
                        PlayerLeft = true;
                        TimeToShoot = TimeToShoot - time;

                        if (TimeToShoot < 0)
                        {
                            shoot = true;
                            TimeToShoot = ResetShoot;
                        }
                        Hits3Run = true;
                    }
                    else
                    {

                        Hits3Run = true;
                        PlayerLeft = false;
                        PlayerRight = false;
                        moveTowardsPlayerNeg = false;
                        moveTowardsPlayerPos = false;
                        move = true;

                    }


                }
                else
                {
                    Hits3Run = true;
                    PlayerLeft = false;
                    PlayerRight = false;
                    moveTowardsPlayerNeg = false;
                    moveTowardsPlayerPos = false;

                }

                if (Physics.OverlapSphere(Directions[2].origin, 0.2f).Length > 1 && !jumpLeft && !jumpRight)
                {

                    grounded = true;

                }
                else
                {
                    Debug.Log("grounded False");
                    grounded = false;
                }

                Debug.Log(" Hits1: " + CurrentHits1.Length);
                //Hits direction 1
                hitCounter1--;
                if (hitCounter1 > -1 && CurrentHits1.Length != 0 && TheEnemy != null && ObstacleBox != null)
                {
                    if (Hits1 != null && Hits1[hitCounter1].collider != null)
                    {

                        if (CurrentHits1[hitCounter1].transform.gameObject == ObstacleBox && !jumpLeft && !jumpRight)// && move == true)
                        {
                            Debug.Log("Boxi havaittu");
                            if (flipped == true && grounded)
                            {
                                Debug.Log("hypätty");
                                jumpLeft = true;
                                move = false;
                                moveTowardsPlayerNeg = false;
                                moveTowardsPlayerPos = false;
                            }
                            else if (flipped == false && grounded)
                            {
                                jumpRight = true;
                                move = false;
                                moveTowardsPlayerNeg = false;
                                moveTowardsPlayerPos = false;
                            }

                        }




                        GameObject[] findEnemy = GameObject.FindGameObjectsWithTag("Enemy");

                        for (int j = 0; j < findEnemy.Length; j++)
                        {
                            if (CurrentHits1[hitCounter1].transform.gameObject == findEnemy[j] && findEnemy[j] != TheEnemy)
                            {
                                Debug.Log("Turn");
                                move = false;
                                turn = true;
                                Hits1Run = true;
                            }
                        }



                        if (CurrentHits1[hitCounter1].transform.gameObject == ObstacleWall && flipped == true && ObstacleWall != null)
                        {
                            move = false;
                            turn = true;
                            Hits1Run = true;
                        }
                        else if ((PlayerLeft || PlayerRight) && grounded)
                        {
                            move = false;
                            Hits1Run = true;
                        }
                        else
                        {
                            move = true;
                            Hits1Run = true;
                        }
                    }

                }
                else
                {

                    Hits1Run = true;
                }

                Debug.Log("Hits RUN: " + Hits0Run + " , " + Hits1Run + " , " + Hits3Run);
                if (Hits0Run && Hits1Run && Hits3Run)
                {
                    Debug.Log("Directions reset");
                    SetDirectionHits = true;
                }

                Debug.Log("Hitscount: " + hitCounter0 + " , " + hitCounter1 + " , " + hitCounter3);
                Debug.Log("JR: " + jumpRight + " JL: " + jumpLeft + " GR: " + grounded + " PR: " + PlayerRight + " PL: " + PlayerLeft);
                /* if (!jumpRight && !jumpLeft && grounded && !PlayerRight && !PlayerLeft && !turn)
                 {

                     Debug.Log("MovE");
                     move = true;
                 }*/


                if (jumpRight || jumpLeft)
                {
                    TimeToFall = TimeToFall - time;
                    if (TimeToFall < 0)
                    {
                        jumpLeft = false;
                        jumpRight = false;
                        TimeToFall = ResetFall;
                    }
                    /* else if(grounded)
                     {
                         jumpRight = false;
                         jumpLeft = false;
                         TimeToFall = ResetFall;

                     }*/
                }
            }

            //------------------------------ Execute Part ---------------------------------
            if (shoot)
            {
                GameObject Bullet;


                Vector3 TargetDir = (-TheEnemy.transform.position + Player.transform.position).normalized;
                float rotzi = Mathf.Acos(TargetDir.x / TargetDir.magnitude) * Mathf.Rad2Deg;
                Bullet = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                Bullet.AddComponent<Rigidbody>().isKinematic = false;
                Bullet.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

                Bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotzi + 90));
                Bullet.transform.position = TheEnemy.transform.position;
                Bullet.AddComponent<DestroyBullet>();
                Bullet.GetComponent<DestroyBullet>().thespawnpoint = TheEnemy;
                Bullet.GetComponent<DestroyBullet>().time = bulletLength;
                Bullet.GetComponent<DestroyBullet>().EnemyBullet = true;

                for (int i = 0; i < TheListOfEnemies.Length; i++)
                {
                    if (TheListOfEnemies[i] != null)
                    {
                        Physics.IgnoreCollision(Bullet.GetComponent<CapsuleCollider>(), TheListOfEnemies[i].GetComponent<CapsuleCollider>());
                    }
                }

                Bullet.GetComponent<Rigidbody>().AddForce((TargetDir + new Vector3(0, 0.25f, 0)) * BulletSpeed, ForceMode.VelocityChange);

                shoot = false;
            }

            if (move)
            {
                if (flipped == false)
                {
                    TheEnemy.transform.position = new Vector3(TheEnemy.transform.position.x + EnemySpeed, TheEnemy.transform.position.y, TheEnemy.transform.position.z);
                }
                else if (flipped == true)
                {
                    TheEnemy.transform.position = new Vector3(TheEnemy.transform.position.x - EnemySpeed, TheEnemy.transform.position.y, TheEnemy.transform.position.z);
                }

            }
            if (moveTowardsPlayerNeg)
            {
                TheEnemy.transform.position = new Vector3(TheEnemy.transform.position.x - EnemySpeed, TheEnemy.transform.position.y, TheEnemy.transform.position.z);
            }
            if (moveTowardsPlayerPos)
            {

                TheEnemy.transform.position = new Vector3(TheEnemy.transform.position.x + EnemySpeed, TheEnemy.transform.position.y, TheEnemy.transform.position.z);

            }
            if (jumpLeft)
            {

                TheEnemy.transform.position = new Vector3(TheEnemy.transform.position.x - EnemySpeed, TheEnemy.transform.position.y + JumpSpeed, TheEnemy.transform.position.z);

            }
            if (jumpRight)
            {

                TheEnemy.transform.position = new Vector3(TheEnemy.transform.position.x + EnemySpeed, TheEnemy.transform.position.y + JumpSpeed, TheEnemy.transform.position.z);

            }
            if (turn)
            {
                if (flipped == true)
                {
                    TimeToTurn = TimeToTurn - time;

                    if (TimeToTurn < 0)
                    {
                        TheEnemy.transform.rotation = Quaternion.Euler(new Vector3(0, TheEnemy.transform.rotation.eulerAngles.y + 270, 0));
                        flipped = false;
                        turn = false;
                        move = true;
                        TimeToTurn = ResetTurn;
                    }

                }
                else
                if (flipped == false)
                {
                    TimeToTurn = TimeToTurn - time;

                    if (TimeToTurn < 0)
                    {
                        TheEnemy.transform.rotation = Quaternion.Euler(new Vector3(0, TheEnemy.transform.rotation.eulerAngles.y - 270, 0));
                        flipped = true;
                        turn = false;
                        move = true;
                        TimeToTurn = ResetTurn;
                    }

                }
                turn = false;
            }
        }
    }
}
