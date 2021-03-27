using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 5;
    public int damage = 5;

    public bool isKilled = false;
    public bool IsKilled { get => IsKilled; }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.GetComponent<Bullet>() != null)
        {
            Bullet bullet = otherCollider.GetComponent<Bullet>();

            if(bullet.ShotByPlayer == true)
            {
                health -= bullet.damage;

                bullet.gameObject.SetActive(false);

                if (health <= 0)
                {
                    if(isKilled == false)
                    {
                        isKilled = true;
                        OnKill();
                    }

                    //Destroy(gameObject);
                }
            }
        }
        if (otherCollider.GetComponent<Mele>() != null)
        {
            Mele mele = otherCollider.GetComponent<Mele>();

            if (mele.MeleByPlayer == true)
            {
                health -= mele.damage;

                if (health <= 0)
                {
                    if (isKilled == false)
                    {
                        isKilled = true;
                        OnKill();
                    }

                    //Destroy(gameObject);
                }
            }
        }

    }

    protected virtual void OnKill()
    {

    }
}
