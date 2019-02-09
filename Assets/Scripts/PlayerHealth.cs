using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int maxHealth = 500;
	public int curHealth;
	public float iFrames = 1.5f; // CONSTANT - Invincibility frames
	public float damageLag = 0.25f; // CONSTANT - Time before player can move again
	float iframeCD;
	float damageLagCD;
	public float knockback = 5;

	public Slider healthSlider;

	public bool damaged = false;
	public bool damageable = true; // True = player is capable of being damaged or is not in "IFrame" state
	bool isDead = false;

	Animator anim;
	AudioSource playerAudio;
	PlayerMovement playerMovement;
	Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		playerMovement = GetComponent<PlayerMovement> ();
		rb2d = GetComponent<Rigidbody2D> ();

		curHealth = maxHealth;
		iframeCD = iFrames;
		damageLagCD = damageLag;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (damaged) {
			rb2d.velocity = new Vector2 (knockback * -transform.localScale.x, rb2d.velocity.y); //(x * backwards push * 0)

			if (damageLagCD >= 0.0f) {	//Activates first
				damageLagCD -= Time.deltaTime;
			}

			if (damageLagCD <= 0.0f) {
				playerMovement.enabled = true;
				damageLagCD = damageLag;
				Debug.Log ("Damage Cooldown reached zero");
				damaged = false;
			}
		}


		if (!damageable) {
			if (iframeCD >= 0.0f) {	//Activates after damageLagCD reaches 0
				iframeCD -= Time.deltaTime;
			}

			if (iframeCD <= 0.0f) {
				iframeCD = iFrames;
				damageable = true;
				Debug.Log ("IFrame Cooldown reached zero");
			}

		}


	}

	public void takeDamage(int amount) {
		if (damageable) {
			damageable = false;
			damaged = true;
			playerMovement.enabled = false;
			curHealth -= amount;
			healthSlider.value = curHealth;
			//Damage Sound Effect

			if (curHealth <= 0 && !isDead) {
				Death ();
			}
		}
	}

	void Death() {
		isDead = true;
		//anim.SetTrigger("Die"); //death animation

		playerMovement.enabled = false; //Disable movement
	}



}
