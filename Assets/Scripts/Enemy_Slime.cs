using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime : Enemy
{
    Rigidbody2D rb2d;
    Animator anim;
    public float jumpPower = 300.0f;
    public float jumpOffset = 50.0f;
    public float speed = 200.0f;
    private float maxSpeed = 2.0f;
    public Vector3 jumpDirection = new Vector3(1f, 0f, 1f);

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //Faces Player
            transform.LookAt(player.transform);

            //Movement
            if (Ready(2.0f, 4.0f))
            {
                Jump();
            }

        }
        

    }

    //jump function
    void Jump()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        float jump = Random.Range(jumpPower - jumpOffset, jumpPower + jumpOffset);

        rb2d.AddForce(jumpDirection.normalized * jump);
    }

    //timer function
    bool Ready(float x, float y)
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        float delay = Random.Range(x, y);
        while (delay > 0f)
        {
            delay -= Time.deltaTime;
            return false;
        }
        if (delay <= 0f)
        {
            delay = Random.Range(x, y);
            return true;
        }

        return false;

    }
}
