using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed = 3f;
	public float speed = 300f;
	public float jumpPower = 200f;

	private int jumpsLeft = 1;      //Double Jump
	private int holdJump = 2;       //Prevents Hovering
	public int wallJumpsLeft = 2;
	public float wallJumpCD = 0.25f;
	public float wallJumpTimer;

	public bool grounded;           //Checks for contact with ground
	public bool doubleJump = true;  //Enable or Disable double jump
	public bool onWall;             //Checks for contact with a wall
	public bool movement = true;    // Used to enable or Disable movement
	private bool buttonTimer = false;

	private Rigidbody2D rb2d;
	private Animator anim;

	void Start()
	{
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		wallJumpTimer = wallJumpCD;
	}

	void Update () {
		//Animation check ---------------------------------------------------

		anim.SetBool ("Grounded", grounded); //Check for grounded
		anim.SetFloat ("Speed", Mathf.Abs (rb2d.velocity.x)); //Checks player speed

		if (Input.GetAxis ("Horizontal") < -0.1f) //If Player is moving Left
			transform.localScale = new Vector3 (-1, 1, 1); //Sprite faces left

		if (Input.GetAxis ("Horizontal") >= 0.1f) //If Player is moving right
			transform.localScale = new Vector3 (1, 1, 1); //Sprite faces right

		//Movement -------------------------------------------------------------

		//Jump
		if (Input.GetKeyDown (KeyCode.Z) && grounded && !doubleJump) {
			rb2d.AddForce (Vector2.up * jumpPower);
		}

		//Double Jump
		if (Input.GetKeyDown (KeyCode.Z) && !Input.GetKey (KeyCode.DownArrow) && jumpsLeft != 0 && doubleJump && (!onWall || grounded || wallJumpsLeft == 0)) {
			rb2d.velocity = new Vector2 (rb2d.velocity.x, 0); //Stops y-axis momentum
			rb2d.AddForce (Vector2.up * jumpPower);
			jumpsLeft--;
		}

		//Control Jump Height
		if (Input.GetKeyUp (KeyCode.Z) && holdJump != 0 && rb2d.velocity.y >= 0.1) {
			rb2d.velocity = new Vector2 (rb2d.velocity.x, rb2d.velocity.y / 2); //Slows down y-axis momentum
			holdJump--;
		}

		//Reset jump counter
		if (grounded) {
			jumpsLeft = 1;
			holdJump = 2;
			wallJumpsLeft = 2;
		}

		//Disables movement while wall jumping for a brief time
		if(wallJumpTimer >= 0 && buttonTimer)
			wallJumpTimer -= Time.deltaTime;

		if (wallJumpTimer <= 0) {
			rb2d.velocity = new Vector2 (rb2d.velocity.x, rb2d.velocity.y);
			movement = true;
			wallJumpTimer = wallJumpCD;
			buttonTimer = false;
		}

        //Slows down movement
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            speed = speed / 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = speed * 2;
        }

        //Wall Slide
        if (onWall && !grounded && rb2d.velocity.y <= 0.0f) {
			rb2d.velocity = new Vector2 (0, -3.5f);

			//Wall Jump
			if (Input.GetKeyDown (KeyCode.Z) && ((!Input.GetKey(KeyCode.UpArrow) && (!Input.GetKey(KeyCode.RightArrow)) && (!Input.GetKey(KeyCode.LeftArrow))))) {
                if (wallJumpsLeft > 0)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    rb2d.velocity = new Vector2(7 * transform.localScale.x, 14);
                    wallJumpsLeft--;
                    buttonTimer = true;
                    movement = false;
                }
			}

			else if(Input.GetKeyDown (KeyCode.Z) && ((!Input.GetKeyDown(KeyCode.UpArrow) || (!Input.GetKeyDown(KeyCode.RightArrow)) || (!Input.GetKeyDown(KeyCode.LeftArrow))))) {
				if (wallJumpsLeft > 0) {
					rb2d.velocity = new Vector2 (transform.localScale.x, 14);
					wallJumpsLeft--;
				}
			}
		}

        //Aim when grounded
        if (Input.GetKeyDown(KeyCode.A) && grounded)
        {
            rb2d.velocity = new Vector3(0, 0, 0);
            movement = false;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(Input.GetKeyDown(KeyCode.LeftArrow)) // NW
                {
                    Debug.DrawLine(transform.position, new Vector3(transform.position.x - 100, transform.position.y + 100, transform.position.z));  
                }
                else if(Input.GetKeyDown(KeyCode.RightArrow)) // NE
                {
                    Debug.DrawLine(transform.position, new Vector3(transform.position.x + 100, transform.position.y + 100, transform.position.z));
                }
                else //N
                    Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 100, transform.position.z));
            }
        }

        if(Input.GetKeyUp(KeyCode.A))
        {
            movement = true;
        }
	}




	void FixedUpdate() //More Physics-based movement
	{
		if (movement) {
			Vector3 easeVelocity = rb2d.velocity;
			easeVelocity.y = rb2d.velocity.y;
			easeVelocity.z = 0.0f;
			easeVelocity.x *= 0.75f;

			float h = Input.GetAxis ("Horizontal"); // Direction (Left/Right)

			if (grounded)
				rb2d.velocity = easeVelocity;

			rb2d.AddForce ((Vector2.right * speed) * h); //Increases speed

			if (rb2d.velocity.x > maxSpeed)
				rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y); //Limits speed to maxSpeed from the right

			if (rb2d.velocity.x < -maxSpeed)
				rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);//Limits speed to maxSpeed from the left

		}
			
	}
}

