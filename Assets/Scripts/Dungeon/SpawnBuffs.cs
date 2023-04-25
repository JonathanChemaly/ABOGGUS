using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuffs : MonoBehaviour
{
    private GameObject player;
    private float range = 40f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {

        }
    }
}
