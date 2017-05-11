using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {

    public float Health = 100;
    public TextMesh HPText;
    float InvulnerabilityTimer;

    // Use this for initialization
    void Start ()
    {
        HPText = GameObject.Find("txtPlayerHealth").GetComponent<TextMesh>();
    }

    void Update()
    {
        InvulnerabilityTimer += Time.deltaTime;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Boss" || col.gameObject.name == "Bullet(Clone)")
        {
            AddDmg(20);
            HPText.text = Health.ToString();
            if (Health < 1)
            {
                Destroy(gameObject);
            }
        }
    }

    public void AddDmg(float d)
    {
        if (InvulnerabilityTimer > 1)
        {
            Health -= d;
            InvulnerabilityTimer = 0;
        }
        if (Health < 1)
        {
            Destroy(gameObject);
        }
    }
}
