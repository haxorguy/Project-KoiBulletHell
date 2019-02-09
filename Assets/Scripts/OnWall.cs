using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWall : MonoBehaviour {

	private PlayerMovement player;

	void Start () {
		player = gameObject.GetComponentInParent<PlayerMovement> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Wall")
			player.onWall = true;
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if(col.gameObject.tag == "Wall")
			player.onWall = true;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.tag == "Wall")
			player.onWall = false;
	}
}
