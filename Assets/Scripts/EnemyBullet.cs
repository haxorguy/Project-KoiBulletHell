using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	private float speed; // Taken from the Enemy class
	private int damage; //Taken from the Enemy Class
	public bool aimAtPlayer;
	GameObject player;
	public GameObject enemy;

	PlayerHealth playerHealth;
	Rigidbody2D rb2d;
	GameObject refBullet; //referencing other clones


	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		enemy = GameObject.Find("tempEnemyBat");
		refBullet = GameObject.Find("tempEnemyBullet(Clone)");

		damage = enemy.GetComponent<Enemy> ().bulletDamage; //gets bulletDamage from Enemy superclass
		speed = enemy.GetComponent<Enemy>().bulletSpeed;
		Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);

		if(aimAtPlayer)
			transform.LookAt(playerPos);
	}


	void Update () {
		transform.position += transform.forward * speed * Time.deltaTime;
	}
		

	void Attack(){
		if (playerHealth.curHealth > 0)
			playerHealth.takeDamage (damage);
	}


	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "PlayerHitBox" && playerHealth.damageable) {
			Attack ();
			Destroy (this.gameObject);
		}


		if (col.tag == "Ground" || col.tag == "Wall") {
			Destroy (this.gameObject);
		}

	}




}

