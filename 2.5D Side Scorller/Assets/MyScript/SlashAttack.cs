using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : MonoBehaviour {


    public GameObject Player;
    public GameObject enemySpawner;
    public Animator Animator;
    GameObject[] TheEnemys;

    public Vector3 hitboxOffset;
    public float AnimSpeed;
    public float Damage;
    public float TimeToStrike;
    float ResetTime;
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

        TheEnemys = enemySpawner.GetComponent<EnemySpawner>().ListOfEnemies;
        enemyCount = TheEnemys.Length;
        EnemyOnZone = false;
        boxSet = false;
        Animator.SetFloat("Speed", AnimSpeed);
        chosenEnemy = -1;
        ObjectSend = false;
        FoundObject = false;
        DamageArea = Instantiate(new GameObject(), Player.transform);
        DamageArea.AddComponent<BoxCollider>().size = new Vector3(0.5f,1,1);
        DamageArea.transform.position = Player.transform.position + new Vector3(0,1,0);
        DamageArea.GetComponent<BoxCollider>().isTrigger = true;
        DamageArea.name = "Damage Area";



        ResetTime = TimeToStrike;
    }

    void FixedUpdate()
    {

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
                Debug.Log("osuma: " + FoundObjects[i].name);
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
                Debug.Log("osuma: " + FoundObjects[i].name);
            }

        }

        if (ObjectSend == false && FoundObjects != null && FoundObjects[0] != null)
        {
            enemyCount = TheEnemys.Length;
            ObjectCount = FoundObjects.Length - 1;
            CurrentObjects = FoundObjects;

            ObjectSend = true;
        }

        /*
        //check area for the enemy
        if (ObjectCount > -1)
        {
            enemyCount--;
            Debug.Log("tänne päästiin");
            Debug.Log("Vihollisten määrä: " + TheEnemys.Length);
            if(enemyCount > -1)
            {
                if (FoundObjects != null && TheEnemys != null && FoundObjects[0] != null)
                {
                    Debug.Log("Palikoita etsitään");
                    if (CurrentObjects[ObjectCount] == TheEnemys[enemyCount])
                    {
                        Debug.Log("Object Found on zone");
                        chosenEnemy = enemyCount;
                        EnemyOnZone = true;
                        FoundObject = true;
                    }
                    else if (FoundObject == false)
                    {
                        Debug.Log("Object notFound");
                        chosenEnemy = -1;
                        EnemyOnZone = false;
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
        */
    }
	
	// Update is called once per frame
	void Update ()
    {
        
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
            Debug.Log("tänne päästiin");
            Debug.Log("Vihollisten määrä: " + TheEnemys.Length);
            if (enemyCount > -1)
            {
                if (FoundObjects != null && TheEnemys != null && FoundObjects[0] != null)
                {
                    Debug.Log("Palikoita etsitään");
                    if (CurrentObjects[ObjectCount] == TheEnemys[enemyCount])
                    {
                        Debug.Log("Object Found on zone");
                        chosenEnemy = enemyCount;
                        EnemyOnZone = true;
                        FoundObject = true;
                    }
                    else if (FoundObject == false)
                    {
                        Debug.Log("Object notFound");
                        chosenEnemy = -1;
                        EnemyOnZone = false;
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
        if (Input.GetAxis("SlashAtc") == 1)
        {
            if (TimeToStrike < 0)
            {
                Animator.SetFloat("Slash", 2);

                if (chosenEnemy != -1)
                {
                    Debug.Log("Lyötiin");
                    if (TheEnemys[chosenEnemy] != null)
                    {
                        Debug.Log("Lyötiin myös");
                        if (EnemyOnZone)
                        {
                            TheEnemys[chosenEnemy].GetComponent<Enemy>().Health = TheEnemys[chosenEnemy].GetComponent<Enemy>().Health - Damage;
                        }
                    }
                }
                
                TimeToStrike = ResetTime;
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
}
