using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : Enemy
{
    public Player player;

    public float shootingInterval = 4f;
    public float chasingInterval = 2f;

    private float shootingTimer;
    private float chasingTimer;

    public float shootingDistance = 3f;
    public float chasingDistance = 12f;

    private NavMeshAgent agent;
    public AudioSource deathSound;
    void Start()
    {
        GameObject.Find("FirstPersonPlayer").GetComponent<Player>();
        shootingTimer = Random.Range(0, shootingInterval);
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(player.transform.position);
    }

    void Update()
    {
        //checks if the player is dead
        if(player.PlayerKilled == true)
        {
            agent.enabled = false;
            this.enabled = false;

        }
        //shooting logic 
        shootingTimer -= Time.deltaTime;
        if(shootingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= shootingDistance)
        {
            shootingTimer = shootingInterval;
            GameObject bullet = ObjectPooling.Instance.GetBullet(false);
            bullet.transform.position = transform.position;
            bullet.transform.forward = (player.transform.position - transform.position).normalized;
        }

        //chasing player logic
        chasingTimer -= Time.deltaTime;
        if(chasingTimer <=0 && Vector3.Distance(transform.position, player.transform.position) <= chasingDistance)
        {
            chasingTimer = chasingInterval;
            agent.SetDestination(player.transform.position);
        }
    }

    protected override void OnKill()
    {
        base.OnKill();

        deathSound.Play();

        agent.enabled = false;
        this.enabled = false;
        transform.localEulerAngles = new Vector3(10f, transform.localEulerAngles.y + 100, transform.localEulerAngles.z);
        
    }
}
