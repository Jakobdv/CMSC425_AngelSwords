using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour {

	public int forceMult = 10;

	//Prevents the player from jumping from the most recent wall
	public Transform forbiddenWall;

	FPSController fpc;

	RaycastHit Wall_L;
	RaycastHit Wall_R;
	RaycastHit Wall_F;
	RaycastHit Wall_B;

	private void Start()
	{
		fpc = GetComponent<FPSController>();
	}

	void Update()
	{
		if (fpc.pressedJump && !fpc.grounded && fpc.canJump)
		{
			//Left
			if (Physics.Raycast(transform.position, transform.right, out Wall_L, 1))
				if (Wall_L.transform.tag == "Climbable")
					Propel(-transform.right, Wall_L.transform);
			//Right
			if (Physics.Raycast(transform.position, -transform.right, out Wall_R, 1))
				if (Wall_R.transform.tag == "Climbable")
					Propel(transform.right, Wall_R.transform);
			//Forward
			if (Physics.Raycast(transform.position, transform.forward, out Wall_F, 1))
				if (Wall_F.transform.tag == "Climbable")
					Propel(-transform.forward, Wall_F.transform);
			//Backward
			if (Physics.Raycast(transform.position, -transform.forward, out Wall_B, 1))
				if (Wall_B.transform.tag == "Climbable")
					Propel(transform.forward, Wall_B.transform);
		}
		else if (fpc.grounded || fpc.boosting) {
			forbiddenWall = null;
		}
	}

	Vector3 force;
	void Propel(Vector3 direction, Transform wall) {
		if (forbiddenWall == null || wall != forbiddenWall)
		{
			fpc.grounded = true;
			force = direction * forceMult;
			fpc.rb.AddForceAtPosition(force, transform.position, ForceMode.Impulse);
			fpc.ResetBoosts();
			forbiddenWall = wall;
		}
	}
}
