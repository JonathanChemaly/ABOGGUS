using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlime : MonoBehaviour
{
    private GameObject player;
    private bool inRange = false;
    private bool dead = false;
    private float range = 20f;
    private float timer = 3.0f;
    private float health = 3f;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource fireballSound;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject fireBallToShoot;
    private float fireBallSpeed = 25.0f;

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
        else
        {
            inRange = false;
            timer = 3f;
            fireBall.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (inRange)
        {
            transform.LookAt(player.transform);
            timer -= Time.deltaTime;
            fireBall.transform.localScale += new Vector3(0.005f, 0.005f, 0.005f);
            if (timer < 0)
            {
                //shoot fireball
                ShootFireball();
                fireballSound.Play();
                timer = 3f;
                fireBall.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        if (health == 0 && dead == false)
        {
            dead = true;
            timer = 0.5f;
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

    private void ShootFireball()
    {
        var fireBallProjectile = Instantiate(fireBallToShoot, transform.position + transform.forward * 2 + transform.up / 2, Quaternion.identity).GetComponent<Rigidbody>();
        fireBallProjectile.GetComponent<Rigidbody>().velocity = transform.forward * fireBallSpeed;
    }

}
