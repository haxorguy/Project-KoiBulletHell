using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float attackCooldown = 1.5f;
	public int attackDamage = 30;
	public float bulletCooldown = 3.0f;
	public int bulletDamage = 20;
	public float bulletSpeed = 10;
    public int health = 30;
	public int rounds = 3; //Number of times shot, 0 = infinite

	public GameObject player;
	PlayerHealth playerHealth;
	bool playerInRange;
	float timer;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();

	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject == player)
			playerInRange = true;
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject == player)
			playerInRange = false;
	}

	void Update() {
		timer += Time.deltaTime;

		if (timer >= attackCooldown && playerInRange) {
			Attack ();
		}

		if (playerHealth.curHealth <= 0) {
			//anim.SetTrigger ("PlayerDead");
		}

        if(health <= 0) {
            Die();
        }
	}

	void Attack() {
		timer = 0f;

		if (playerHealth.curHealth > 0)
			playerHealth.takeDamage(attackDamage);
	}

    void Die() {
        //Death animation
        Destroy(this);
    }
}
