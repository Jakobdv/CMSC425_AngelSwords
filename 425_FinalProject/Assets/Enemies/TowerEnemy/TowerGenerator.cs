using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is for generating the tower enemies
public class TowerGenerator : MonoBehaviour
{
    public GameObject Tower;
    public Transform TowerSpawnPoint;
    public int arenaRadius = 50;
    
    private float spawnTime = 15f;
    private float startTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnTower", startTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTower() 
    {
        GameObject towerInstance;
        towerInstance = Instantiate(Tower, TowerSpawnPoint.position, TowerSpawnPoint.rotation);
        towerInstance.transform.position = new Vector3(Random.Range(-arenaRadius, arenaRadius), 0f, Random.Range(-arenaRadius, arenaRadius));
    }
}
