using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
	public Camera cam;

	Vector3 basePos;
	Vector3 targetPos;
	float step;

	public ParticleSystem speedLines;
	float defaultFOV;
	float currentFOV;
	float targetFOV;

	private void Start()
	{
		basePos = transform.localPosition;
		defaultFOV = cam.fieldOfView;
	}

	//Axis = X, Y or Z
	public void MoveOnAxis(string axes, float amount, float percentOfSecond)
	{
		step = Time.deltaTime / percentOfSecond;

		targetPos = Vector3.zero;
		if (axes.Contains("X"))
			targetPos.x = amount;
		if (axes.Contains("Y"))
			targetPos.y = amount;
		if (axes.Contains("Z"))
			targetPos.z = amount;
	}


	float interpFOV = 1;
	float interpSpeed = 1;

	public void ChangeFOV(int amount, float speed) {
		targetFOV = defaultFOV + amount;

		currentFOV = cam.fieldOfView;

		interpSpeed = speed;
		interpFOV = 0;
	}

	public void ResetFOV()
	{
		targetFOV = defaultFOV;

		currentFOV = cam.fieldOfView;

		interpSpeed = 1;
		interpFOV = 0;
	}

	private void Update()
	{
		//Moving
		if (Vector3.Distance(transform.localPosition, basePos + targetPos) > 0.001f)
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, basePos + targetPos, step);
		}
		else //Reset to original position
		{
			targetPos = Vector3.zero;
			step = 2 * Time.deltaTime;
		}

		//Field of view
		if (interpFOV < 1)
		{
			cam.fieldOfView = Mathf.Lerp(currentFOV, targetFOV, interpFOV);

			interpFOV += 3*Time.deltaTime * interpSpeed;
		}

		if (speedLines != null)
		{
			if (cam.fieldOfView > defaultFOV + 2)
			{
				speedLines.Play();
			}
			else speedLines.Stop();
		}

	}
}
