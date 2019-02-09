using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Text HPText;
	public Image HPOrb, HPFill;
	public Sprite Orb1, Orb2, Orb3; //Green > Yellow > Red
	public Sprite Fill1, Fill2, Fill3; //Green > Yellow > Red
	public Sprite DamagedOrb, DamagedFill;

	PlayerHealth playerHealth;
	GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
	}


	void Update () {

		//Health Bar
		HPText.text = "" + playerHealth.curHealth;

		if (playerHealth.curHealth <= playerHealth.maxHealth / 2) { //if HP half, turn yellow
			HPOrb.sprite = Orb2;
			HPFill.sprite = Fill2;

			if (playerHealth.curHealth <= playerHealth.maxHealth / 3.8) { //turn red
				HPOrb.sprite = Orb3;
				HPFill.sprite = Fill3;
			}
		} else {
			HPOrb.sprite = Orb1; // green
			HPFill.sprite = Fill1;
		}

		if (!playerHealth.damageable) { //Try to make it flash a couple times
			HPOrb.sprite = DamagedOrb;
			HPFill.sprite = DamagedFill;

		}
	}


}

