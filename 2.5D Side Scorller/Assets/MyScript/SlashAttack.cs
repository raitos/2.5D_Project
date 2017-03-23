using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : MonoBehaviour {


    public GameObject Player;
    public GameObject Enemy;
    public Animator Animator;
   
    public Vector3 hitboxOffset;
    public float AnimSpeed;
    public float Damage;
    public float TimeToStrike;
    float ResetTime;
    GameObject DamageArea;
    float time;
    bool boxSet;
    Quaternion PlayerRotation;
    


    // Use this for initialization
    void Start ()
    {
        boxSet = false;
        Animator.SetFloat("Speed", AnimSpeed);
       
        DamageArea = Instantiate(new GameObject(), Player.transform);
        DamageArea.AddComponent<BoxCollider>().size = new Vector3(0.5f,1.5f,1);
        DamageArea.transform.position = Player.transform.position;
        DamageArea.GetComponent<BoxCollider>().isTrigger = true;
        ResetTime = TimeToStrike;
    }
	
	// Update is called once per frame
	void Update () {

        time = Time.deltaTime;
        TimeToStrike = TimeToStrike - time;

        if(Player.transform.rotation != PlayerRotation)
        {
            boxSet = false;
        }
        if (!boxSet)
        {
            if (Player.transform.rotation.y > 0)
            {
                    PlayerRotation = Player.transform.rotation;
                DamageArea.transform.position = DamageArea.transform.position + hitboxOffset;
            }
            else if (Player.transform.rotation.y < 0)
            {
                    PlayerRotation = Player.transform.rotation;
                DamageArea.transform.position = DamageArea.transform.position - hitboxOffset;
                
            }
            boxSet = true;
        }


        if (Input.GetAxis("SlashAtc") == 1)
        {
            if (TimeToStrike < 0)
            {

                Animator.SetFloat("Slash", 2);
                if (Enemy != null)
                {
                    Enemy.GetComponent<Enemy>().Health = Enemy.GetComponent<Enemy>().Health - Damage;
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
