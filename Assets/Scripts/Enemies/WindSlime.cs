using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSlime : MonoBehaviour
{
    private GameObject player;
    private bool inRange = false;
    private bool dead = false;
    private float range = 15f;
    private float speed = 0.01f;
    private float timer = 0.5f;
    private float health = 3f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 3f)
        {
            inRange = false;

            player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, 0.005f);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        if (inRange)
        {
            transform.LookAt(player.transform);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z), speed);
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
