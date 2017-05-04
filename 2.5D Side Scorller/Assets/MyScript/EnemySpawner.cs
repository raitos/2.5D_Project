using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// EDITOR SCRIPT

public class EnemySpawner : MonoBehaviour {

    public GameObject EnemyTemplate;
    public int AmountOfEnemies;
    public Vector3[] PositionsOfEnemies;
    public GameObject[] ListOfEnemies;
    
    GameObject[] currentList;
   
    bool once;

	// Use this for initialization
    void Awake()
    {
        
        
       

       

    }
	void Start ()
    {
       
        
    }
    

    
	// Update is called once per frame
    
	public void Update ()
    {
        if (ListOfEnemies != null)
        {

            for (int i = 0; i < ListOfEnemies.Length; i++)
            {
                if (ListOfEnemies[i] != null)
                {
                    PositionsOfEnemies[i] = ListOfEnemies[i].transform.position;
                }
                
            }
        }
        if (!EditorApplication.isPlaying)
        {

            /*  if (once == false)
              {
                  ListOfEnemies = new GameObject[AmountOfEnemies];
                  PositionsOfEnemies = new Vector3[AmountOfEnemies];

                  once = true;
              }*/
            
            
                for (int i = 0; i < ListOfEnemies.Length; i++)
                {
                    if (ListOfEnemies[i] == null)
                    {
                        ListOfEnemies[i] = currentList[i];
                    }
                }
            

            if (AmountOfEnemies < ListOfEnemies.Length && (AmountOfEnemies != ListOfEnemies.Length || AmountOfEnemies != PositionsOfEnemies.Length))
            {
                for (int i = ListOfEnemies.Length - 1; i > AmountOfEnemies - 1; i--)
                {
                    if (ListOfEnemies[i] != null)
                    {
                        DestroyImmediate(ListOfEnemies[i].gameObject);
                    }
                    else
                    {
                        DestroyImmediate(ListOfEnemies[i]);
                    }

                }
                GameObject[] curreEnemies = new GameObject[AmountOfEnemies];
                Vector3[] currPositions = new Vector3[AmountOfEnemies];
                for (int i = 0; i < AmountOfEnemies; i++)
                {
                    currPositions[i] = PositionsOfEnemies[i];
                    curreEnemies[i] = ListOfEnemies[i];
                }
                ListOfEnemies = new GameObject[AmountOfEnemies];
                ListOfEnemies = curreEnemies;
                PositionsOfEnemies = new Vector3[AmountOfEnemies];
                PositionsOfEnemies = currPositions;

                currentList = ListOfEnemies;
            }
            else if (AmountOfEnemies > ListOfEnemies.Length && (AmountOfEnemies != ListOfEnemies.Length || AmountOfEnemies != PositionsOfEnemies.Length))
            {

                GameObject[] CurrentEnemys = new GameObject[AmountOfEnemies];
                Vector3[] currentPositions = new Vector3[AmountOfEnemies];
                if (ListOfEnemies != null && PositionsOfEnemies != null)
                {
                    for (int i = 0; i < ListOfEnemies.Length; i++)
                    {
                        currentPositions[i] = PositionsOfEnemies[i];
                        CurrentEnemys[i] = ListOfEnemies[i];
                    }
                    for (int i = 0; i < ListOfEnemies.Length; i++)
                    {
                        DestroyImmediate(ListOfEnemies[i].gameObject);
                    }

                    PositionsOfEnemies = new Vector3[AmountOfEnemies];
                    ListOfEnemies = new GameObject[AmountOfEnemies];
                    PositionsOfEnemies = currentPositions;
                    ListOfEnemies = CurrentEnemys;
                }


               

                for (int i = 0; i < AmountOfEnemies; i++)
                {
                    if (ListOfEnemies[i] == null)
                    {
                        ListOfEnemies[i] = Instantiate(EnemyTemplate);
                        ListOfEnemies[i].transform.position = PositionsOfEnemies[i];
                        ListOfEnemies[i].GetComponent<CapsuleCollider>().enabled = true;
                        ListOfEnemies[i].GetComponent<MeshRenderer>().enabled = true;
                        ListOfEnemies[i].GetComponent<Enemy>().enabled = true;
                        ListOfEnemies[i].GetComponent<Rigidbody>().isKinematic = false;
                        ListOfEnemies[i].gameObject.tag = "Enemy";
                    }


                }
                for (int i = CurrentEnemys.Length; i < AmountOfEnemies; i++)
                {
                    PositionsOfEnemies[i] = new Vector3(i, 2, 0);
                }
                currentList = ListOfEnemies;
            }
           /* if (ListOfEnemies != null)
            {
                for (int i = 0; i < AmountOfEnemies; i++)
                {
                    if (ListOfEnemies[i] == null)
                    {
                        once = false;
                    }
                }
                currentList = ListOfEnemies;
            }
            for (int i = 0; i < ListOfEnemies.Length; i++)
            {
                if (ListOfEnemies[i] == null)
                {
                    once = false;
                }

            }*/
           
        }
    }

}
