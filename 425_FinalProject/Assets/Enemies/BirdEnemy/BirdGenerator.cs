using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGenerator : MonoBehaviour
{

    public float startTime = 5f;
    public float spawnTime = 30f;
    public GameObject Bird;

    private float arenaRadius = 50;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBird", startTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBird()
    {
        GameObject birdInstance;
        //rotation should be facing the player
        Transform BirdSpawnPoint = new GameObject().transform;
        BirdSpawnPoint.rotation = Quaternion.Euler(0, 0, 0);
        BirdSpawnPoint.position = new Vector3(0,0,0);
        birdInstance = Instantiate(Bird, BirdSpawnPoint.position, BirdSpawnPoint.rotation);
        //want to spawn on a cube
        birdInstance.transform.position = new Vector3(Random.Range(-arenaRadius, arenaRadius), 0f, Random.Range(-arenaRadius, arenaRadius));
    }
}
