using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : MonoBehaviour {

    public Animator anim;
    BossAI boss;

	void Start ()
    {
        anim = gameObject.GetComponent<Animator>();
        boss = GameObject.Find("Boss").GetComponent<BossAI>();
	}

	void Update ()
    {

        if (boss.SlashActive || boss.Slash2Active || boss.SlashDash)
        {
                anim.SetBool("IsIdle", false);
                anim.SetBool("IsSlash", true);
        }
        else
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsReflect", false);
            anim.SetBool("IsSlash", false);
        }
	}
}
