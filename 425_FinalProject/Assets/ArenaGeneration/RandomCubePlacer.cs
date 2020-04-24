using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCubePlacer : MonoBehaviour
{
	public GameObject prefab;

	public int amount = 20;
	public int arenaRadius = 50;

	public Vector3 minDimensions = new Vector3(10, 10, 10);
	public Vector3 maxDimensions = new Vector3(50, 50, 50);

	private void Start()
	{
		GameObject newCube;
		Vector3 newScale, newPos;
		for (int i = 0; i < amount; i++) {
			newPos = new Vector3(
				Random.Range(-arenaRadius, arenaRadius), 
				Random.Range(0, arenaRadius), //Keeps all the cubes above the floor`
				Random.Range(-arenaRadius, arenaRadius));

			newScale = new Vector3(
				Random.Range(minDimensions.x, maxDimensions.x),
				Random.Range(minDimensions.y, maxDimensions.y),
				Random.Range(minDimensions.z, maxDimensions.z)
			);

			newCube = GameObject.Instantiate(prefab, newPos, Quaternion.identity);
			newCube.transform.localScale = newScale;
		}
	}
}
