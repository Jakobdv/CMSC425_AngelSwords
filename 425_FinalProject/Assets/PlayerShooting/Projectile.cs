using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public int damage = 10;

	//Tags that don't cause the projectile to explode
	public List<string> safeTags;

	Collider[] overlaps;
	private void Update()
	{
		 overlaps = Physics.OverlapSphere(transform.position, transform.localScale.x);

		foreach (Collider overlap in overlaps) {
			if (overlap.transform.root.GetComponent<EnemyHealth>())
			{
				overlap.transform.root.GetComponent<EnemyHealth>().DealDamage(damage);
				Destroy(this.gameObject);
			}
			else if (!safeTags.Contains(overlap.transform.tag))
			{
				Debug.Log(overlap.transform.tag);
				Destroy(this.gameObject);
			}
		}
	}
}
