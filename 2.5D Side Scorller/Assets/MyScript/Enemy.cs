using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject enemy;
    public float Health;
    public float hitdamage;

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
        this.gameObject.tag = "Enemy";
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Health <= 0)
        {
            DestroyImmediate(enemy.gameObject,true);
            enemy = null;
        }
        if (Health < 75 && Health >= 25)
        {
            enemy.transform.localScale = new Vector3(0.75f,0.75f,0.75f);
        }
        if (Health < 25 && enemy != null) 
        {
            enemy.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}
