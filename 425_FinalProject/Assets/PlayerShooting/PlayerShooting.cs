using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	public GameObject projectile;
	public Transform barrel;
	public Transform mainCamera;
	public int fireSpeed = 1000;

	/*Average length of time between shots*/
	public float fireRate = 0.25f;
	/*How much the fire rate should vary with each shot*/
	public float fireRateVariance = 0.1f;

	float timer = 0;

	private void Update()
	{
		if (timer > 0)
			timer -= Time.deltaTime;

		if (Input.GetMouseButton(0) && timer <= 0) {
			Fire();
			timer = fireRate + Random.Range(-fireRateVariance, fireRateVariance);
		}
	}

	void Fire() {
		GameObject newProjectile = GameObject.Instantiate(projectile, barrel.position, barrel.rotation);
		
		RaycastHit hit;
		Vector3 projectileDirection;

		//Try to fire at the point the camera is currently pointing at, if possible
		if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, 1000, 1) && hit.transform != null)
		{
			projectileDirection = (hit.point - newProjectile.transform.position).normalized * fireSpeed;
		}
		else {
			projectileDirection = mainCamera.transform.forward * fireSpeed;
		}

		newProjectile.GetComponent<Rigidbody>().AddForce(projectileDirection);
	}
}
