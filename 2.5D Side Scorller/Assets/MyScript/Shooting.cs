using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    public GameObject bullet;
    public GameObject Arm;
  //  public ConfigurableJoint playerBullet;
    Transform bullettransf;
    Transform playertransf;

    GameObject BulletShot;
    
    public float AimingSpeed;
    public float BulletSpeed;
    public float bulletLenght;
    
    bool shoot;
    public float timeToShoot;
    float resetTime;
    
    
    // Use this for initialization
    void Start ()
    {
        resetTime = timeToShoot;
        shoot = false;
        
    
        bullettransf = bullet.GetComponent<Transform>();
        playertransf = Arm.GetComponent<Transform>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
       
        Vector3 targetDir = (-Arm.transform.position + bullet.transform.position).normalized;
        float rotzi = Mathf.Acos(targetDir.x / targetDir.magnitude) * Mathf.Rad2Deg;


        //Rotate Arm
        if(Input.GetAxis("MoveAim") == 1)
        {
            Debug.Log("Nappi1");
            bullet.GetComponent<Rigidbody>().isKinematic = false;
            Arm.GetComponent<Rigidbody>().isKinematic = false;
            

            Arm.transform.rotation = Quaternion.Euler(0f, 0f, rotzi - AimingSpeed);
              

        }
        else if(Input.GetAxis("MoveAim") == -1)
        {
            Debug.Log("Nappi0");
            bullet.GetComponent<Rigidbody>().isKinematic = false;
            Arm.GetComponent<Rigidbody>().isKinematic = false;
            

            Arm.transform.rotation = Quaternion.Euler(0f, 0f, rotzi + AimingSpeed);
            
        }
        else
        {
            bullet.GetComponent<Rigidbody>().isKinematic = true;
            Arm.GetComponent<Rigidbody>().isKinematic = true;
          
        }

        timeToShoot -= Time.deltaTime;
        //Shoot
        if (Input.GetAxis("Attack") == 1)
        {
            

            if (timeToShoot < 0)
            {
                shoot = true;
                timeToShoot = resetTime;
               
            }
            else
            {
                shoot = false;
            }

        }
       //Set Bullet and speed it up
        if (shoot)
        {
            BulletShot = Instantiate(bullet);

            BulletShot.GetComponent<Rigidbody>().isKinematic = false;
            BulletShot.GetComponent<Rigidbody>().AddForce(targetDir * BulletSpeed, ForceMode.VelocityChange);
            BulletShot.AddComponent<DestroyBullet>().time = bulletLenght;
            BulletShot.GetComponent<DestroyBullet>().thespawnpoint = bullet;
            BulletShot.GetComponent<CapsuleCollider>().enabled = true;
        }
       
        shoot = false;
        
    }
    
}
