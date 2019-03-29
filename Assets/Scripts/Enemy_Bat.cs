using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bat : Enemy {

	Rigidbody2D rb2d;
	Rigidbody2D bulletrb2d;
	Animator anim;
	public GameObject bullet;
	private float shotDelay;
    public float speed = 200.0f;
    public float maxSpeed = 2.0f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		bulletrb2d = bullet.GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {

        // ---Movement---
        float movementFreq = Random.Range(2.0f, 5.0f);
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;
        //Bounces up and down
        if (rb2d.velocity.y < -9.0f) // && isOnScreen
			rb2d.velocity = new Vector2(rb2d.velocity.x, 9.0f);
        //Occasionally moves left and right
        rb2d.velocity = easeVelocity;

        if (movementFreq <= 0) {
            flip();
            movementFreq = Random.Range(2.0f, 5.0f);
            movementFreq -= Time.deltaTime;
            rb2d.AddForce((Vector2.right * speed)); //Increases speed

            if (rb2d.velocity.x > maxSpeed)
                rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y); //Limits speed to maxSpeed from the right

            if (rb2d.velocity.x < -maxSpeed)
                rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);//Limits speed to maxSpeed from the left

        }


        //Attacks
        shotDelay += Time.deltaTime;

		if(rounds > 0){ //Limited shots
			if (shotDelay >= bulletCooldown && rounds > 0) {
				Fire ();
				rounds--;
				shotDelay = 0.0f;
			}
		}

		else if (rounds == 0) //Shoots indefinitely
			if(shotDelay >= bulletCooldown) {
				Fire();
				shotDelay = 0.0f;
			}
        //If rounds < 0, enemy doesn't shoot

        //Debug - Laser indicating where the bullet's being shot
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
        Debug.DrawLine(transform.position, playerPos, Color.red);
    }

	void Fire() {
		Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z); //aims at Player
		Instantiate (bullet, transform.position, Quaternion.LookRotation(playerPos));
	}

    void flip()
    {
        Vector3 currentFlip = transform.localScale = new Vector3();
        currentFlip.x *= -1;
    }
}

