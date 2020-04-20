using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLines : MonoBehaviour {

	public Camera cam;
	public FPSController fpc;
	public ParticleSystem lines;

	//How fast the player must be going to turn the lines on
	public int speedForLines = 14;

	float minFov, maxFov, targetFov;

	private void Start()
	{
		fpc = GetComponent<FPSController>();
		minFov = cam.fieldOfView;
		maxFov = minFov + 10;
	}

	float currentSpeed;
	Vector3 lastPos;
	private void FixedUpdate()
	{
		currentSpeed = (transform.position - lastPos).magnitude / Time.deltaTime;
		lastPos = transform.position;

		if (currentSpeed > speedForLines)
		{
			lines.Play();
//			lines.emission.rateOverTime = Mathf.Clamp(00 * currentSpeed/30;
		}
		else lines.Stop();

		targetFov = Mathf.Clamp(minFov * currentSpeed / 12, minFov, maxFov);

		if (cam.fieldOfView != targetFov) {
			cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, targetFov, 0.1f);
		}
	}
}
