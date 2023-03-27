using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;
public class FireSlime : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private bool inRange = false;
    private bool dead = false;
    private float range = 20f;
    private float timer = 3.0f;
    private float deathTimer = 1f;
    private float health = 3f;
    public float damage = 10f;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource fireballSound;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject fireBallToShoot;
    [SerializeField] private GameObject ElementalDrop;
    private float fireBallSpeed = 25.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        animator.fireEvents = false;
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
            Debug.Log(timer);
            transform.LookAt(player.transform);
            timer -= Time.deltaTime;
            fireBall.transform.localScale += new Vector3(0.005f, 0.005f, 0.005f);
            
        }
        if (timer < 0)
        {
            ShootFireball();
            fireballSound.Play();
            timer = 3f;
            fireBall.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (health == 0 && dead == false)
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
                Instantiate(ElementalDrop, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameController.player.TakeDamage(damage);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" || other.gameObject.tag == "MagicAttack")
        {
            Debug.Log("Grass Slime health:" + health);
            health -= 1;
            if (other.GetComponent<WindAttack>() != null)
            {
                other.GetComponent<WindAttack>().Destroy();

            }
        }

    }
    private void ShootFireball()
    {
        var fireBallProjectile = Instantiate(fireBallToShoot, transform.position + transform.forward * 2 + transform.up / 2, Quaternion.identity).GetComponent<Rigidbody>();
        fireBallProjectile.GetComponent<Rigidbody>().velocity = transform.forward * fireBallSpeed;
    }

}
