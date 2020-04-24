using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
	public int maxHealth = 30;
	public int currentHealth;

	public GameObject deathSpawn;

	private void Start()
	{
		currentHealth = maxHealth;
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		if (currentHealth <= 0) {
			Die();
		}
	}

	public void Die() {
		Debug.Log("Object destroyed");

		Instantiate(deathSpawn, transform.position, transform.rotation);
		Destroy(this.gameObject);
	}

}
