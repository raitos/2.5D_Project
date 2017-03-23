using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {


    public GameObject thespawnpoint;
    public float time;

    // Use this for initialization
    void Start ()
    {

        Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), thespawnpoint.GetComponent<Collider>(), true);

        Destroy(this.gameObject,time);
	}
	
    
	// Update is called once per frame
	void Update ()
    {
		
	}
    void OnCollisionEnter(Collision col)
    {

        Destroy(this.gameObject);
    }
}
