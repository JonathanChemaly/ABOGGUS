using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSlime : MonoBehaviour
{
    private GameObject player;
    public GameObject vine;
    private bool inRange = false;
    private bool dead = false;
    private float range = 15f;
    private float speed = 0.075f;
    private float timer = 1f;
    private float health = 3f;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource vineSound;
    private int damping = 2;
    private Transform target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2.5f)
        {
            if (timer == 1f)
            {
                //use vine attack
                vineSound.Play();
            }
            inRange = false;
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 1f;
                
            }
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
            Vector3 lookPos = target.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
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
                deathSound.Play();
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
