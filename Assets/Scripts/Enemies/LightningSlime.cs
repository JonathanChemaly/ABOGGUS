using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSlime : MonoBehaviour
{
    private GameObject player;
    public GameObject lightningAOE;
    private bool inRange = false;
    private bool dead = false;
    private float range = 15f;
    private float speed = 1.5f;
    private float timer = 0.5f;
    private float health = 3f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            inRange = true;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < 0.5f)
        {
            timer -= Time.deltaTime;
            inRange = false;
            if (timer > 0)
            {
                lightningAOE.transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            }
            else
            {
                lightningAOE.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            //player take damage from lighting area turn off lightning area and freeze for a few seconds
        }
        else
        {
            inRange = false;
        }

        if (inRange)
        {
            transform.LookAt(player.transform);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);

        }

        if (health == 0)
        {
            dead = true;
        }

        if (dead)
        {
            timer -= Time.deltaTime;
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            if (timer < 0)
            {
                Destroy(gameObject);
            }
        }else if (timer < 0)
        {
            timer = 0.5f;
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // player take damage

        }
    }
}
