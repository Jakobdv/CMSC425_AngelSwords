using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestruction : MonoBehaviour
{
	public float lifeTime = 2;

	private void Update()
	{
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0) {
			Destroy(this.gameObject);
		}
	}
}
