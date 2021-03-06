﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //change so that it only shows the laser when the player is visible
        lr.SetPosition(0, new Vector3(transform.position.x, transform.position.y + 1.45f, transform.position.z + .45f));
        RaycastHit hit;
        Vector3 direction = PlayerManager.instance.player.transform.position - transform.position;

        if(Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.collider) 
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else
            lr.SetPosition(1, transform.forward*5000);
    }
}
