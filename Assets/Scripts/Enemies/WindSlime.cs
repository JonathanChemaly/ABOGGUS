using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSlime : MonoBehaviour
{
    private GameObject player;
    private bool inRange = false;
    private bool dead = false;
    private float range = 15f;
    private float speed = 0.03f;
    private float pull = 0.025f;
    private float deathTimer = 1f;
    private float health = 3f;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource windSound;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 5f)
        {
            inRange = false;
            windSound.Play();
            player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, pull);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            windSound.Stop();
            inRange = true;
            pull = 0.025f;
        }
        else
        {
            inRange = false;
        }

        if (inRange)
        {
            transform.LookAt(player.transform.position + new Vector3(0, 1f, 0f));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z), speed);
        }

        if (health == 0)
        {
            dead = true;
        }

        if (dead)
        {
            if (deathTimer == 1f)
            {
                deathSound.Play();
            }
 
            deathTimer -= Time.deltaTime;
            transform.localScale -= new Vector3(0.02f, 0.02f, 0.02f);
            if (deathTimer < 0)
            {
                Destroy(gameObject);
            }
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            health = 0;
            pull = 0f;
        }
    }
}
