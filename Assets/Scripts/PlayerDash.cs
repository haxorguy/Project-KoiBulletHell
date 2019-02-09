using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour {

	public float dashSpeed = 10f;
	public float maxDash = 0.5f;
	private float dashTimer = 0.0f;
	//public float positionOffsetX = 3; //Default at center
	//public float positionOffsetY = 3; //Default at bottom

	public DashState dashState;
	public Vector2 savedVelocity;

	private Rigidbody2D rb2d;
	private Animator anim;

	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
	}
	

	void FixedUpdate () {

		switch (dashState)
		{
		case DashState.Ready:
			if (canDash()) 
			{
				savedVelocity = new Vector2 (rb2d.velocity.x, 0);
				rb2d.velocity = new Vector2 (rb2d.velocity.x * dashSpeed, 0);
				dashState = DashState.Dashing;
			}
			break;

		case DashState.Dashing:
			dashTimer += Time.deltaTime;
			if (dashTimer >= maxDash)
			{
				dashTimer = maxDash;
				rb2d.velocity = savedVelocity;
				dashState = DashState.Cooldown;
			}
			break;

		case DashState.Cooldown:
			dashTimer -= Time.deltaTime;
			if (dashTimer <= 0) 
			{
				dashTimer = 0;
				dashState = DashState.Ready;
			}
			break;
		}

	}

	public bool canDash(){
		float doubleTapTimer = 0.0f; //when the user taps left/right twice

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			doubleTapTimer += Time.deltaTime;

			if (doubleTapTimer <= 0.1f && Input.GetKeyDown (KeyCode.RightArrow)){
				doubleTapTimer = 0.0f;
				return true;
			}
			else {
				doubleTapTimer = 0.0f;
				return false;
			}
		}
			
		else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			doubleTapTimer += Time.deltaTime;

			if (doubleTapTimer <= 0.1f && Input.GetKeyDown (KeyCode.LeftArrow)) {
				doubleTapTimer = 0.0f;
				return true;
			}
			else {
				doubleTapTimer = 0.0f;
				return false;
			}
		}

		else if (Input.GetKeyDown (KeyCode.LeftShift)) // For testing purposes
			return true;
		return false;
	}
}



public enum DashState {
	Ready,
	Dashing,
	Cooldown
}
