using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public float bulletDuration = 2f;
    public int damage = 5;
    public float gravity = -9.81f;

    public Vector3 velocity;

    private float lifeTimer;

    private bool shotByPlayer;
    public bool ShotByPlayer { get { return shotByPlayer; } set { shotByPlayer = value; } }
    void OnEnable()
    {
        lifeTimer = bulletDuration;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        lifeTimer -= Time.deltaTime;

        if(lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }

        if (velocity.y <= 0)
        {
            velocity.y = -2f;
        }

        velocity.y = Mathf.Sqrt(bulletDuration * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;

        //transform.position -= -velocity * Time.deltaTime;
    }
}
