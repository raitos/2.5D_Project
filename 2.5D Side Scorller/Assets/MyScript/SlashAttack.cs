using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : MonoBehaviour {


    public GameObject Player;
    public Animator Animator;
    GameObject[] TheEnemys;

    public Vector3 hitboxOffset;
    public float AnimSpeed;
    public float Damage;
    public float TimeToStrike;
    public float ResetTime;
    public int comboCount;

    GameObject DamageArea;
    float time;
    bool boxSet;
    bool EnemyOnZone;
    bool ObjectSend;
    int chosenEnemy;
    int enemyCount;
    int ObjectCount;
    bool FoundObject;
    Quaternion PlayerRotation;
    GameObject[] FoundObjects;
    GameObject[] CurrentObjects;
    


    // Use this for initialization
    void Start ()
    {

        TheEnemys = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        EnemyOnZone = false;
        boxSet = false;
        //Animator.SetFloat("Speed", AnimSpeed);
        chosenEnemy = -1;
        ObjectSend = false;
        FoundObject = false;
        DamageArea = Instantiate(new GameObject(), Player.transform);
        DamageArea.AddComponent<BoxCollider>().size = new Vector3(0.5f,1,1);
        DamageArea.transform.position = Player.transform.position + new Vector3(0,1,0);
        DamageArea.GetComponent<BoxCollider>().isTrigger = true;
        DamageArea.name = "Damage Area";
        DamageArea.layer = 2; //sets the area to ignore raycasts so it doesn't f up jumping.
        Physics.IgnoreCollision(DamageArea.GetComponent<BoxCollider>(), this.gameObject.GetComponent<Collider>());



        //ResetTime = TimeToStrike;
    }

    void FixedUpdate()
    {

        //Let's Find objects from the boxcolliders area
        /*if (Player.transform.rotation.y < 0)
        {
            RaycastHit[] hits = Physics.BoxCastAll(DamageArea.GetComponent<BoxCollider>().transform.position,
      new Vector3(DamageArea.GetComponent<BoxCollider>().size.x / 2, DamageArea.GetComponent<BoxCollider>().size.y / 2, DamageArea.GetComponent<BoxCollider>().size.z / 2), new Vector3(-1, 0, 0), DamageArea.transform.rotation, DamageArea.GetComponent<BoxCollider>().size.x / 2);

            FoundObjects = new GameObject[hits.Length];
            Debug.Log("Hits Length: " + hits.Length);
            for (int i = 0; i < hits.Length; i++)
            {
                FoundObjects[i] = hits[i].transform.gameObject;
                Debug.Log("Hit: " + FoundObjects[i].name);
            }
        }
        else if (Player.transform.rotation.y > 0)
        {
            RaycastHit[] hits = Physics.BoxCastAll(DamageArea.GetComponent<BoxCollider>().transform.position,
      new Vector3(DamageArea.GetComponent<BoxCollider>().size.x / 2, DamageArea.GetComponent<BoxCollider>().size.y / 2, DamageArea.GetComponent<BoxCollider>().size.z / 2), new Vector3(1, 0, 0), DamageArea.transform.rotation, DamageArea.GetComponent<BoxCollider>().size.x / 2);

            FoundObjects = new GameObject[hits.Length];
            Debug.Log("Hits Length: " + hits.Length);

            for (int i = 0; i < hits.Length; i++)
            {
                FoundObjects[i] = hits[i].transform.gameObject;
                Debug.Log("Hit: " + FoundObjects[i].name);
            }

        }

        if (ObjectSend == false && FoundObjects != null && FoundObjects[0] != null)
        {
            enemyCount = TheEnemys.Length;
            ObjectCount = FoundObjects.Length - 1;
            CurrentObjects = FoundObjects;

            ObjectSend = true;
        }*/
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Player.GetComponent<PlayerPhysics>().Grounded) { }

        //Let's Find objects from the boxcolliders area
        if (Player.transform.rotation.y < 0)
        {
            RaycastHit[] hits = Physics.BoxCastAll(DamageArea.GetComponent<BoxCollider>().transform.position,
      new Vector3(DamageArea.GetComponent<BoxCollider>().size.x / 2, DamageArea.GetComponent<BoxCollider>().size.y / 2, DamageArea.GetComponent<BoxCollider>().size.z / 2), new Vector3(-1, 0, 0), DamageArea.transform.rotation, DamageArea.GetComponent<BoxCollider>().size.x / 2);

            FoundObjects = new GameObject[hits.Length];
            Debug.Log("Hits Length: " + hits.Length);
            for (int i = 0; i < hits.Length; i++)
            {
                FoundObjects[i] = hits[i].transform.gameObject;
                Debug.Log("Hit: " + FoundObjects[i].name);
            }
        }
        else if (Player.transform.rotation.y > 0)
        {
            RaycastHit[] hits = Physics.BoxCastAll(DamageArea.GetComponent<BoxCollider>().transform.position,
      new Vector3(DamageArea.GetComponent<BoxCollider>().size.x / 2, DamageArea.GetComponent<BoxCollider>().size.y / 2, DamageArea.GetComponent<BoxCollider>().size.z / 2), new Vector3(1, 0, 0), DamageArea.transform.rotation, DamageArea.GetComponent<BoxCollider>().size.x / 2);

            FoundObjects = new GameObject[hits.Length];
            Debug.Log("Hits Length: " + hits.Length);

            for (int i = 0; i < hits.Length; i++)
            {
                FoundObjects[i] = hits[i].transform.gameObject;
                Debug.Log("Hit: " + FoundObjects[i].name);
            }

        }

        if (ObjectSend == false && FoundObjects != null && FoundObjects[0] != null)
        {
            enemyCount = TheEnemys.Length;
            ObjectCount = FoundObjects.Length - 1;
            CurrentObjects = FoundObjects;

            ObjectSend = true;
        }

        time = Time.deltaTime;
        TimeToStrike = TimeToStrike - time;
        Debug.DrawRay(DamageArea.GetComponent<BoxCollider>().transform.position, new Vector3(DamageArea.GetComponent<BoxCollider>().size.x / 2, 0, 0), Color.green);
        Debug.DrawRay(DamageArea.GetComponent<BoxCollider>().transform.position, new Vector3(0, DamageArea.GetComponent<BoxCollider>().size.y / 2, 0), Color.red);
        Debug.DrawRay(DamageArea.GetComponent<BoxCollider>().transform.position, new Vector3(0, 0, DamageArea.GetComponent<BoxCollider>().size.z / 2), Color.blue);
        //Set The Boxcollider to face the player's rotation
        if (Player.transform.rotation != PlayerRotation)
        {
            boxSet = false;
        }
        if (!boxSet)
        {
            if (Player.transform.rotation.y < 0)
            {
                    PlayerRotation = Player.transform.rotation;
                DamageArea.transform.position = Player.transform.position + new Vector3(hitboxOffset.x, hitboxOffset.y, hitboxOffset.z);
                
            }
            else if (Player.transform.rotation.y > 0)
            {
                    PlayerRotation = Player.transform.rotation;
                DamageArea.transform.position = Player.transform.position - new Vector3(hitboxOffset.x, -hitboxOffset.y, hitboxOffset.z);
                
            }
            boxSet = true;
        }
       

        //check area for the enemy
        if (ObjectCount > -1 && ObjectSend == true)
        {
            enemyCount--;
            Debug.Log("we got here");
            
            if (enemyCount > -1)
            {
                if (FoundObjects != null && TheEnemys != null && FoundObjects[0] != null)
                {
                    Debug.Log("Looking for blocks");
                    for (int i = 0; i < CurrentObjects.Length; i++)
                    {
                        if (CurrentObjects[i] == TheEnemys[enemyCount])
                        {
                            Debug.Log("Object Found on zone");
                            chosenEnemy = enemyCount;
                            EnemyOnZone = true;
                            FoundObject = true;
                            break;
                        }
                        else if (FoundObject == false)
                        {
                            Debug.Log("Object notFound");
                            chosenEnemy = -1;
                            EnemyOnZone = false;
                        }
                    }
                }
            }
            else
            {
                ObjectCount--;
                enemyCount = TheEnemys.Length;
            }
        }
        else
        {
            FoundObject = false;
            ObjectSend = false;
        }


        //look for slash attack input
        if (Input.GetButtonDown("SlashAtc"))
        {
            if (TimeToStrike < -1) comboCount = 1;
            if (TimeToStrike < 0)
            {
                combo();
                Animator.SetFloat("Slash", 2);
                if (chosenEnemy != -1)
                {
                    Debug.Log("Hit");
                    if (TheEnemys[chosenEnemy] != null)
                    {
                        Debug.Log("Another hit");
                        if (EnemyOnZone)
                        {
                            TheEnemys[chosenEnemy].GetComponent<Enemy>().Health = TheEnemys[chosenEnemy].GetComponent<Enemy>().Health - Damage;
                        }
                    }
                }
                
                TimeToStrike = ResetTime;
                comboCount++;
                if(comboCount > 3)
                {
                    comboCount = 1;
                }
            }
            else
            {
                Animator.SetFloat("Slash", 0);
            }

        }
        else
        {
            Animator.SetFloat("Slash", 0);
        }
       

	}
    void combo()
    {
        switch (comboCount)
        {
            case 0:
                comboCount++;
                goto case 1;
            case 1:
                ResetTime = 0.25F;
                Damage = 1;
                break;
            case 2:
                ResetTime = 0.4F;
                Damage = 1;
                break;
            case 3:
                ResetTime = 0.25F;
                Damage = 2;
                break;
            default:
                Debug.Log("This should not happen. But it should be fixed after this.");
                comboCount = 1;
                break;
        }
    }
}
