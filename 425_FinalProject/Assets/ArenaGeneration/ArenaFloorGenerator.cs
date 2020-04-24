using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaFloorGenerator : MonoBehaviour
{
	public GameObject floorPiece;
	public Vector3 pieceDimensions = new Vector3(10, 10, 10);

	//How big the floor itself should be
	public Vector2 floorDimensions = new Vector2(100, 100);

	List<GameObject> pieces;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.L)) {
			RandomizeHeights();
		}
	}

	public void Start()
	{
		transform.position = new Vector3(floorDimensions.x / -2, 0, floorDimensions.y / -2);

		int totalPieces = (int)((floorDimensions.x / pieceDimensions.x) * (floorDimensions.y / pieceDimensions.z));
		pieces = new List<GameObject>();
		GenerateFloor();
	}

	public void GenerateFloor() {
		GameObject newPiece;
		for (int x = 0; x < floorDimensions.x / pieceDimensions.x; x++) {
			for (int y = 0; y < floorDimensions.y / pieceDimensions.z; y++) {
				newPiece = GameObject.Instantiate(floorPiece, 
				transform.position + new Vector3(pieceDimensions.x * x, pieceDimensions.y/-2, pieceDimensions.z * y), 
				Quaternion.identity);

				newPiece.transform.localScale = pieceDimensions;
				pieces.Add(newPiece);
			}
		}
	}

	public void RandomizeHeights() {
		foreach (GameObject piece in pieces) {
			//piece.transform.position += Vector3.up * Random.Range(-2.5f, 2.5f);
//			piece.transform.position += Vector3.up * Random.Range(-pieceDimensions.y/2, pieceDimensions.y/2);
			piece.transform.position += Vector3.up * Random.Range(0, pieceDimensions.y);
		}
	}
}
