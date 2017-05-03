using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject enemy;
    public float Health;
    public float hitdamage;
    float theTime;
    float TimeToDie;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.Equals(GameObject.Find("Bullet(Clone)")))
        {
            Debug.Log("Enemy Hit");
            Health = Health - hitdamage;
        }
    }

    // Use this for initialization
    void Start ()
    {
        theTime = Time.deltaTime;
        TimeToDie = 0.5f;
        this.gameObject.tag = "Enemy";
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Health <= 0)
        {
            if(enemy.GetComponent<WeakEnemy>() != null)
            {
                enemy.GetComponent<WeakEnemy>().EnemyAnimator.SetFloat("Death", 3);
            }
            if (enemy.GetComponent<Attacker>() != null)
            {
                enemy.GetComponent<Attacker>().EnemyAnimator.SetFloat("Death", 3);
            }
            if (enemy.GetComponent<Defender>() != null)
            {
                enemy.GetComponent<Defender>().EnemyAnimator.SetFloat("Death", 3);
            }
            TimeToDie = TimeToDie - theTime;
            if(TimeToDie < 0)
            {
                DestroyImmediate(enemy.gameObject, true);
                enemy = null;
            }
        }
        /*
        if (Health < 75 && Health >= 25)
        {
            enemy.transform.localScale = new Vector3(0.75f,0.75f,0.75f);
        }
        if (Health < 25 && enemy != null) 
        {
            enemy.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }*/
    }
}
