using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
	public CameraBobbing camBob;

	//Movement
	public float speed = 10.0F;
	public float walkSpeed = 12.0F;
	public float runSpeed = 12.0F;
	bool running;

	public bool canMove = true;
	Vector3 fullTranslation;

	//Jumping
	[Range(1, 10)]
	public float jumpVelocity = 7;
	public float jumpMovementDrag = 1.25f; //Slows the players movemnet when jumping

	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;

	public Rigidbody rb;

	public bool canJump = true;
	public bool grounded;
	public bool jumping = false;
	public bool pressedJump;
	RaycastHit hit;

	static public bool canRotate = true;
	public bool rotateViewer;

	//Boost
	public int boostAmount = 7;
	Vector3 boostStart;
	public bool boosting = false;
	public int maxBoosts = 1;
	public int midairBoosts = 0;

//	bool DebugRBForce = false;
	RaycastHit floor;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;
	}

	
	void FixedUpdate()
	{
		#region Move + Rotate
		rotateViewer = canRotate;

		if (canMove)
		{
			Vector3 vMove = transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;
			Vector3 hMove = transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;

			fullTranslation = hMove + vMove;

			//Slower movement when airborne
			if (!grounded)
				fullTranslation /= jumpMovementDrag;

			rb.MovePosition(transform.position + fullTranslation);
		}
		#endregion


	}

	private void Update()
	{
		//Moved to update because input wasn't being properly registered
		#region Sprint
		//Toggle running when on the ground
		if (Input.GetButtonDown("Fire3") && grounded && !boosting)
		{
			if (!running)
				camBob.ChangeFOV(10, 1);
			else
				camBob.ResetFOV();

			running = true; //!running;
		}

		//If you stop moving forward, stop running
		if (Input.GetAxis("Vertical") <= 0 && running)
		{
			running = false;
			camBob.ResetFOV();
		}

		//Actual adjustment of running
		if (running)
		{
			speed = runSpeed;
		}
		else
		{
			speed = walkSpeed;
		}
		#endregion sprint

		#region Jump
		if (!pressedJump &&  Physics.Raycast(transform.position, -transform.up, out floor, 2, 1))
		{
			if (jumping && !pressedJump)
			{
				jumping = false;
				if (camBob)
					camBob.MoveOnAxis("Y", -0.5f, 0.5f);
			}
			grounded = true;
			ResetBoosts();
		}
		else
			//Coyote time coroutine
			StartCoroutine(CountdownToFalse(0.225f));//was 0.25f

		if (canJump)
		{
			//GetButtonDown = Normal, GetButton = Bunny Hop
			if (Input.GetButtonDown("Jump"))
				StartCoroutine(PressedRegisterGap(0.25f));

			if (grounded && pressedJump)
			{
				rb.velocity = Vector3.up * jumpVelocity;
				grounded = false;
				jumping = true;
			}

			else 
			{
				if (rb.velocity.y < 0)
						rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
				else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
					rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
			}

		}
		#endregion

		#region Boost

		if (Input.GetButtonDown("Fire3") && !grounded && !boosting && midairBoosts < maxBoosts)
		{
			boostStart = transform.position;
			//Player doesn't fall while boosting
			rb.useGravity = false;
			if (fullTranslation != Vector3.zero)
				rb.AddForce(fullTranslation.normalized * boostAmount * 500 * Time.deltaTime, ForceMode.Impulse);

			//If the player isn;t going any particular direction, just boost forward
			else
				rb.AddForce(new Vector3(0, 0, 0.2f).normalized * boostAmount * 500 * Time.deltaTime, ForceMode.Impulse);

			midairBoosts++;
			boosting = true;

			camBob.ChangeFOV(6, 10);
		}
		//If the player has boosted the provided amount, stop them
		if (boosting && Vector3.Distance(transform.position, boostStart) > boostAmount) {
			rb.useGravity = true;
			rb.velocity = Vector3.ClampMagnitude(rb.velocity, 0);
			boosting = false;
			camBob.ResetFOV();
		}
		#endregion

		#region Cursor
		if (Input.GetKeyDown("escape")) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		if (Input.GetButtonDown("Fire1")) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		#endregion
	}

	//Stop the boost on collision
	private void OnCollisionEnter(Collision collision)
	{
		if (boosting)
		{
			rb.useGravity = true;
			rb.velocity = Vector3.ClampMagnitude(rb.velocity, 0);
			boosting = false;

			camBob.ResetFOV();
		}

		if (floor.transform != null)
			rb.velocity = Vector3.zero;

	}

	//Coyote-time coroutine
	IEnumerator CountdownToFalse(float timer) {
		yield return new WaitForSeconds(timer);
		if (grounded) //only do this if the player was just grounded
			grounded = false;
	}

	//Used to make it so that any point within x seconds of pressing jump is registered. Not quite bunnyhop, not quite... not 
	IEnumerator PressedRegisterGap(float x)
	{
		pressedJump = true;

		yield return new WaitForSeconds(x);
		pressedJump = false;
	}

	public void ResetBoosts() {
		midairBoosts = 0;
	}
}
