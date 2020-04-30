using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int maxHealth = 10;
	int health = 10;

	public GameObject deathParticle;

	private void Start()
	{
		health = maxHealth;
	}

	public void DealDamage(int damageAmt) {
		health -= damageAmt;
		if (health <= 0) {
			Die();
		}
	}

	void Die() {
		Instantiate(deathParticle, transform.position, transform.rotation);
		Destroy(this.gameObject);
	}
}
