using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdEnemyGenerator : MonoBehaviour
{
    public GameObject CrowdEnemy;

    private float startTime = 1f;
    private float spawnTime = .1f;
    private int calls = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCrowd", startTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(calls == 15)
            CancelInvoke();
    }

    void SpawnCrowd() 
    {
        GameObject crowdInstance;
        crowdInstance = Instantiate(CrowdEnemy, transform.position, transform.rotation);
        calls++;
    }
}
