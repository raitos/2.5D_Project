using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {


    public float Health;
    public float Damage;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Health <= 0)
        {
            Destroy(this.gameObject);
            Application.Quit();
        }
	}
}
