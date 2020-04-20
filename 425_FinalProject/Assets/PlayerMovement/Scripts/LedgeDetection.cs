using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetection : MonoBehaviour {

	RaycastHit upperHit;
	RaycastHit lowerHit;

	FPSController fpc;

	public Vector3 startPos;
	public Vector3 targetPos;
	public float interp = 1;

	bool climbing = false;

	private void Start()
	{
		fpc = GetComponent<FPSController>();
	}

	void Update()
	{

		Physics.Raycast(transform.position + Vector3.up * 1.5f, transform.forward / 2, out upperHit, 1, 1);
		Physics.Raycast(transform.position - Vector3.up * 0.5f, transform.forward / 2, out lowerHit, 1, 1);
//		Physics.BoxCast(transform.position - new Vector3(0, 0.5f, 0), Vector3.one, transform.forward / 2, out lowerHit, Quaternion.identity, 1, 1);


		//If the raycast checks worked
		if (!upperHit.transform && lowerHit.transform)
		{
			//Player isn't grounded and the ledge is climbable
			if (!fpc.grounded && lowerHit.transform.tag == "Climbable" && !climbing)
			{
				fpc.canJump = false;
				fpc.rb.velocity = Vector3.zero;

				startPos = transform.position;
				targetPos = transform.position +  transform.forward  + transform.up;
				fpc.canMove = false;
				fpc.canJump = false; ;

				fpc.rb.constraints = RigidbodyConstraints.FreezeAll;
				interp = 0;
				climbing = true;
			}
		}


		if (climbing && interp < 1)
		{
			transform.position = Vector3.Lerp(startPos, targetPos, interp);
			interp += fpc.speed/3 * Time.deltaTime;
		}
		else if (climbing){
			fpc.rb.constraints = RigidbodyConstraints.FreezeRotation;
			fpc.canMove = true;
			climbing = false;

			fpc.jumping = false;
			fpc.grounded = false;
			fpc.canJump = true;
		}
	}
}
