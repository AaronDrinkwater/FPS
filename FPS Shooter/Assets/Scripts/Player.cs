using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public GameObject bulletPrefab;
    public Camera playerCamera;

    public Enemy enemy;

    public GameObject meleWeapon;

    public int initialAmmo = 12;
    public int initialHealth = 100;

    private int ammo;
    private int health;
    public int Ammo { get { return ammo; } }
    public int Health { get { return health; } }

    private bool playerKilled;
    public bool PlayerKilled { get => playerKilled; }

    public float knockbackForce = 10f;
    public float damagedDuration = 0.5f;
    public int meleDamage = 5;

    private bool isDamaged = false;

    private float bulletDrop = 2.5f;
 
    void Start()
    {
        health = initialHealth;
        ammo = initialAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (ammo > 0 && playerKilled == false)
            {
                ammo--;
                //GameObject bulletObject = Instantiate(bulletPrefab);
                GameObject bulletObject = ObjectPooling.Instance.GetBullet(true);
                bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward /*new Vector3(0, transform.position.y - bulletDrop , 0)*/; //sets the bullet posotion to where the player is standing (transform.position) + one unit in front of him (transfrom.foward)
                bulletObject.transform.forward = playerCamera.transform.forward /*- new Vector3(0, playerCamera.transform.u, 0)*/; //same foward vector as camera, it points to what the plaer is looking at
                //bulletObject.transform.position -
            }
        }

        //if(Input.GetMouseButtonUp(1))
        //{
        //    float speed = 5;
        //    float RotAngleY = -90;

        //    if(playerKilled == false)
        //    {
        //        float rY = Mathf.SmoothStep(meleWeapon.transform.position.x, RotAngleY, Mathf.PingPong(Time.time * speed, 2));
        //        meleWeapon.transform.rotation = Quaternion.Euler(0, 0, rY);
        //    }
        //}
    }
    private void OnTriggerEnter(Collider otherCollider)
    {
        //check collision
        //Debug.Log(hit.collider.name);
        //collect ammo crates
        if (otherCollider.GetComponent<AmmoCrate>() != null)
        {
            AmmoCrate ammoCrate = otherCollider.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;

            Destroy(ammoCrate.gameObject);
        }

        else if (otherCollider.GetComponent<HealthCrate>() != null)
        {
            HealthCrate healthCrate = otherCollider.GetComponent<HealthCrate>();
            health += healthCrate.health;

            Destroy(healthCrate.gameObject);
        }

        if (isDamaged == false)
        {
            GameObject hazard = null;

            if (otherCollider.GetComponent<Enemy>() != null)
            {
                Enemy enemy = otherCollider.GetComponent<Enemy>();

                if(enemy.isKilled == false)
                {
                    hazard = enemy.gameObject;
                    health -= enemy.damage;
                }
            }
            //enemy bullet damage to player
            else if(otherCollider.GetComponent<Bullet>() != null)
            {
                Bullet bullet = otherCollider.GetComponent<Bullet>();
                
                if(bullet.ShotByPlayer == false)
                {
                    hazard = bullet.gameObject;
                    //take damage from ranged enemy
                    health -= bullet.damage;
                    bullet.gameObject.SetActive(false);
                }
            }
            else if(otherCollider.GetComponent<Mele>() != null)
            {
                Mele mele = otherCollider.GetComponent<Mele>();

                if (mele.MeleByPlayer == false)
                {
                    hazard = mele.gameObject;
                    //take damage from ranged enemy
                    health -= mele.damage;
                    mele.gameObject.SetActive(false);
                }
            }
            if(hazard != null)
            {
                isDamaged = true;

                Vector3 damagedDirection = (transform.position - hazard.transform.position).normalized;
                Vector3 knockbackDirecton = (damagedDirection + Vector3.up).normalized;
                GetComponent<ForceReciver>().AddForce(knockbackDirecton, knockbackForce);


                StartCoroutine(DamagedRoutine());
            }
            if(health <= 0)
            {
                if(playerKilled == false)
                {
                    playerKilled = true;
                    OnKill();
                }
            }
        } 
    }

    IEnumerator DamagedRoutine()
    {
        yield return new WaitForSeconds(damagedDuration);
        isDamaged = false;
    }

    private void OnKill()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        //GetComponent<Player>().enabled = false;
    }
}
 