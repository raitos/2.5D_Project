using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRightDetect : MonoBehaviour {

    GameObject player;

    public BossAI bossAI;

    void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            bossAI.playerRightWall = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            bossAI.playerRightWall = false;
        }
    }
}
