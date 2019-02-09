using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMovement : MonoBehaviour {

	public float distance = 10f;
	public float speed = 80f;

	private Rigidbody2D rb2d;

	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();	
	}

	void Update () {
		if(transform.rotation.z == 0)
			transform.Translate (Vector2.right * speed * Time.deltaTime);

	}
}
