using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLeftDetect : MonoBehaviour {

    GameObject player;

    public BossAI bossAI;

	void Start ()
    {

	}
	
	void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            bossAI.playerLeftWall = true;
        }
	}
    void OnTriggerExit (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            bossAI.playerLeftWall = false;
        }
    }
}
