using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {

    public GameObject Player;
    public GameObject thespawnpoint;
    public float time;
    public bool EnemyBullet;
    Collider[] playerCols;

    // Use this for initialization
    void Start ()
    {
        if (Player != null && EnemyBullet == false)
        {
            playerCols = Player.GetComponents<Collider>();

            for (int i = 0; i < playerCols.Length; i++)
            {
                Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), playerCols[i]);
            }
        }
        if (thespawnpoint != null)
        {
            Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), thespawnpoint.GetComponent<Collider>(), true);
        }
        Destroy(this.gameObject,time);
	}
	
    
	// Update is called once per frame
	void Update ()
    {
		
	}
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.transform.gameObject == Player)
        {
            Player.GetComponent<PlayerHealth>().Health = Player.GetComponent<PlayerHealth>().Health - Player.GetComponent<PlayerHealth>().Damage;
        }
        Destroy(this.gameObject);
    }
}
