using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoor : MonoBehaviour {

    public GameObject DoorObject;
    float DoorStartPosition;
    public GameObject player;
    PlayerPhysics playerPhysics;

    public float speed = 3;

    public Animator anim;

    Vector3 MidPos;
    Vector3 Goal;
    bool enabled = true;

    public int dir = 1;

    void Start()
    {
        playerPhysics = GameObject.Find("Player").GetComponent<PlayerPhysics>();
        DoorObject = GameObject.Find("Lockdown door (1)");
        DoorStartPosition = DoorObject.transform.position.y;
        player = GameObject.Find("Player");
        MidPos = transform.position;
        MidPos.x += 1.5F * dir;
        Goal = transform.position;
        Goal.x -= 2 * dir;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player" && enabled && dir > 0)
        {
            float speedt = speed * Time.deltaTime;
            if (player.transform.position.x >= MidPos.x - 1 && DoorObject.transform.position.y < DoorStartPosition + 2)
            {
                playerPhysics.timeScale = 0;
                DoorObject.transform.Translate(Vector2.up * Time.deltaTime);
                player.transform.position = Vector3.MoveTowards(player.transform.position, MidPos, speedt);
            }
            else if (player.transform.position.x > Goal.x)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, Goal, speedt);
            }
            else if (DoorObject.transform.position.y > DoorStartPosition)
            {
                DoorObject.transform.Translate(Vector2.down * Time.deltaTime);
            }
            else
            {
                playerPhysics.timeScale = 1;
                enabled = false;
            }
        }
        else if (col.gameObject.tag == "Player" && enabled && dir < 0)
        {
            float speedt = speed * Time.deltaTime;
            if (player.transform.position.x <= MidPos.x + 1 && DoorObject.transform.position.y < DoorStartPosition + 2)
            {
                playerPhysics.timeScale = 0;
                DoorObject.transform.Translate(Vector2.up * Time.deltaTime);
                player.transform.position = Vector3.MoveTowards(player.transform.position, MidPos, speedt);
            }
            else if (player.transform.position.x < Goal.x)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, Goal, speedt);
            }
            else if (DoorObject.transform.position.y > DoorStartPosition)
            {
                DoorObject.transform.Translate(Vector2.down * Time.deltaTime);
            }
            else
            {
                playerPhysics.timeScale = 1;
                enabled = false;
            }
        }
    }
	
}
