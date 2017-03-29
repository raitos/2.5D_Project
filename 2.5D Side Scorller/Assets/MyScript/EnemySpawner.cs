using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour {

    public GameObject EnemyTemplate;
    public int AmountOfEnemies;
    public Vector3[] PositionsOfEnemies;
    public GameObject[] ListOfEnemies;
    Transform[] EnemyTransforms;
    bool once;

	// Use this for initialization
    void Awake()
    {
        ListOfEnemies = new GameObject[AmountOfEnemies];
        EnemyTransforms = new Transform[AmountOfEnemies];



        for (int i = 0; i < AmountOfEnemies; i++)
        {

            ListOfEnemies[i] = Instantiate(EnemyTemplate);
            ListOfEnemies[i].transform.position = PositionsOfEnemies[i];
            ListOfEnemies[i].GetComponent<CapsuleCollider>().enabled = true;
            ListOfEnemies[i].GetComponent<MeshRenderer>().enabled = true;
            ListOfEnemies[i].GetComponent<Enemy>().enabled = true;
            ListOfEnemies[i].GetComponent<Rigidbody>().isKinematic = false;


        }
        this.gameObject.GetComponent<EnemySpawner>().enabled = false;
    }
	void Start ()
    {
      
       
    }
	
	// Update is called once per frame
	void Update ()
    {
      
    }
}
