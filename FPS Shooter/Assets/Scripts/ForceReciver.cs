using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReciver : MonoBehaviour
{
    public float deceleration = 5;
    public float mass = 3;

    private Vector3 intensity;
    private PlayerMovement player;
    void Start()
    {
        intensity = Vector3.zero;
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (intensity.magnitude > 0.2)
        {
            x = player.velocity.x;
            //x += intensity * Time.deltaTime;
            Vector3 localIntensity = intensity;
            player.controller.Move(localIntensity * Time.deltaTime);

        }

        intensity = Vector3.Lerp(intensity, Vector3.zero, deceleration * Time.deltaTime); //knockback
        
    }

    public void AddForce(Vector3 direction, float force)
    {
        intensity += direction.normalized * force / mass; 
    }
}
