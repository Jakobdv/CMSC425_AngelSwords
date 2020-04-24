using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTiling : MonoBehaviour
{

	public enum Direction {X, Y, Z };
	public Direction x_scale = Direction.X;
	public Direction y_scale = Direction.Y;

	public bool is_plane = false;

	Renderer rend;
	Vector2 scale;

	void Start()
	{
		rend = GetComponent<Renderer>();
		

		if (is_plane)
		{
			scale.x = transform.localScale.x;
			scale.y = transform.localScale.z;

			scale *= 10;
		}
		else {
			switch (x_scale) {
				case (Direction.X):
					scale.x = transform.localScale.x;
					break;
				case (Direction.Y):
					scale.x = transform.localScale.y;
					break;
				case (Direction.Z):
					scale.x = transform.localScale.z;
					break;
			}

			switch (y_scale)
			{
				case (Direction.X):
					scale.y = transform.localScale.x;
					break;
				case (Direction.Y):
					scale.y = transform.localScale.y;
					break;
				case (Direction.Z):
					scale.y = transform.localScale.z;
					break;
			}
		}

		scale /= 12;
		rend.material.SetTextureScale("_MainTex", scale);	
	}
}
