using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bat : Enemy {

	Rigidbody2D rb2d;
	Rigidbody2D bulletrb2d;
	Animator anim;
	public GameObject bullet;
	float shotDelay;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		bulletrb2d = bullet.GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {
		
		//Movement
		if(rb2d.velocity.y < -9.0f) // && isOnScreen
			rb2d.velocity = new Vector2(rb2d.velocity.x, 10.0f); //Bounces up and down in the air

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
	}

	void Fire() {
		Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z); //aims at Player
		Instantiate (bullet, transform.position, Quaternion.LookRotation(playerPos));
	}
}

