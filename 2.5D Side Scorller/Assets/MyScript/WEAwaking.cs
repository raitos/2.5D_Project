using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WEAwaking : MonoBehaviour {


    public GameObject[] TheEnemy;


	// Use this for initialization
	void Start ()
    {
       TheEnemy = GameObject.FindGameObjectsWithTag("Enemy");	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        
        foreach (GameObject E in TheEnemy)
        {
            if (E != null)
            {
                if (GeometryUtility.TestPlanesAABB(planes, E.GetComponent<Collider>().bounds) && E.GetComponent<WeakEnemy>() != null)
                {
                    E.GetComponent<WeakEnemy>().enabled = true;
                }
                else if (E.GetComponent<WeakEnemy>() != null)
                {
                    E.GetComponent<WeakEnemy>().enabled = false;
                }
            }
        }
       
    }
}
