using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTwo : MonoBehaviour {

    public GameObject bullet;
    public GameObject Player;
    GameObject BulletShot;

    public bool shoot;
    public float timeToShoot;
    float resetTime;
    public float ShootYOffset;
    public float ShootXOffset;
    float time;
    public float BulletSpeed;
    public float bulletLenght;
    public float BulletSpreadLimit;
    Vector3 targetDir;


    GameObject animatedObj;
    public Animator anim;
    Controller ctrl;
    public bool shotRight;
    public float ReflectSpread;

    // Use this for initialization
    void Start ()
    {
        ctrl = Player.GetComponent<Controller>();
        resetTime = timeToShoot;
        shoot = false;
        time = Time.deltaTime;

        //Animations
        animatedObj = GameObject.Find("model_character_main_05_03_animation_all");
        anim = animatedObj.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update ()
    {

        timeToShoot = timeToShoot - time;

        if (ctrl.dir < 0)
        {
            targetDir = new Vector3(-1, 0, 0);
        }
        else if(ctrl.dir > 0)
        {
            targetDir = new Vector3(1, 0, 0);
        }

        if(Input.GetAxis("Attack") == 1)
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

        if(shoot)
        {
            anim.SetBool("IsShoot", true);
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsJump", false);
            anim.SetBool("IsDash", false);
            if (ctrl.dir < 0)
            {
                shotRight = false;
                BulletShot = Instantiate(bullet, new Vector3(Player.transform.position.x - ShootXOffset, Player.transform.position.y + ShootYOffset, Player.transform.position.z), Quaternion.Euler(0, 0, 90));
                Physics.IgnoreCollision(Player.GetComponent<Collider>(), BulletShot.GetComponent<Collider>(), true);
                BulletShot.GetComponent<MeshRenderer>().enabled = true;
                BulletShot.GetComponent<Rigidbody>().AddForce((targetDir + new Vector3(Random.Range(BulletSpreadLimit, -BulletSpreadLimit), Random.Range(BulletSpreadLimit, -BulletSpreadLimit), 0)) * BulletSpeed, ForceMode.VelocityChange);
                BulletShot.AddComponent<DestroyBullet>().time = bulletLenght;
                BulletShot.GetComponent<DestroyBullet>().thespawnpoint = Player;
                BulletShot.GetComponent<DestroyBullet>().isEnemyBullet = false;
            }
            else if (ctrl.dir > 0)
            {
                shotRight = true;
                BulletShot = Instantiate(bullet, new Vector3(Player.transform.position.x + ShootXOffset, Player.transform.position.y + ShootYOffset, Player.transform.position.z), Quaternion.Euler(0, 0, 90));
                Physics.IgnoreCollision(Player.GetComponent<Collider>(), BulletShot.GetComponent<Collider>(), true);
                BulletShot.GetComponent<MeshRenderer>().enabled = true;
                BulletShot.GetComponent<Rigidbody>().AddForce((targetDir + new Vector3(Random.Range(BulletSpreadLimit, -BulletSpreadLimit), Random.Range(BulletSpreadLimit, -BulletSpreadLimit), 0)) * BulletSpeed, ForceMode.VelocityChange);
                BulletShot.AddComponent<DestroyBullet>().time = bulletLenght;
                BulletShot.GetComponent<DestroyBullet>().thespawnpoint = Player;
                BulletShot.GetComponent<DestroyBullet>().isEnemyBullet = false;
            }
            shoot = false;
        }
        else
        {
            anim.SetBool("IsShoot", false);
        }







    }
}
