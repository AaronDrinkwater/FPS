using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mele : MonoBehaviour
{
    public int damage = 5;
    public float speed = 5f;
    public float RotAngleY = -90;

    private bool meleByPlayer = true;
    public bool MeleByPlayer { get { return meleByPlayer; } set { meleByPlayer = value; } }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            float rY = Mathf.SmoothStep(transform.position.x, RotAngleY, Mathf.PingPong(Time.time * speed, 2));
            transform.rotation = Quaternion.Euler(0, 0, rY);
        }
    }
}
