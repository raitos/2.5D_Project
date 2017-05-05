using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {


    public GameObject thespawnpoint;
    
    public float time;
    public bool isEnemyBullet;
    ShootingTwo shoot;
    BossAI boss;

    // Use this for initialization
    void Start ()
    {
        shoot = GameObject.Find("Player").GetComponent<ShootingTwo>();
        boss = GameObject.Find("Boss").GetComponent<BossAI>();

        Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), thespawnpoint.GetComponent<Collider>(), true);

        

        Destroy(this.gameObject,time);
	}
	
    
	// Update is called once per frame
	void Update ()
    {
        
	}
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject == thespawnpoint.gameObject)
        {

        }
        else if (col.gameObject.name == "Player")
        {

        }
        else if (col.gameObject.name == "ReflectShield")
        {
            Debug.Log("osu kilpee");
            if (shoot.shotRight)
            {

                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * shoot.BulletSpeed + new Vector3(Random.Range(shoot.ReflectSpread, -shoot.ReflectSpread), Random.Range(shoot.ReflectSpread, -shoot.ReflectSpread), 0), ForceMode.VelocityChange);
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * shoot.BulletSpeed + new Vector3(Random.Range(shoot.ReflectSpread, -shoot.ReflectSpread), Random.Range(shoot.ReflectSpread, -shoot.ReflectSpread), 0), ForceMode.VelocityChange);
            }
        }
        else if (col.gameObject.name == "Boss")
        {
            Debug.Log("Osui bossiin");
            boss.AddDmg(10);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
}
