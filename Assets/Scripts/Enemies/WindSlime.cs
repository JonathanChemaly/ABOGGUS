using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;
public class WindSlime : MonoBehaviour, IEnemy
{
    private GameObject player;
    private bool inRange = false;
    private bool dead = false;
    private float range = 15f;
    private float speed = 0.03f;
    private float pull = 0.025f;
    private float deathTimer = 1f;
    private float health = 60f;
    private bool takingDamage = false;
    private float invTime = 0.3f;
    private float damageTimer = 0f;
    public float damage = 10f;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource windSound;
    [SerializeField] private GameObject ElementalDrop;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        //Update invisibility timer while taking damage
        if(takingDamage)
        {
            damageTimer += Time.fixedDeltaTime;
            if (damageTimer > invTime)
            {
                takingDamage = false;
                damageTimer = 0;
            }
        }

        if (Vector3.Distance(transform.position, player.transform.position) == 5f)
        {
            windSound.Play();
        }
        if (Vector3.Distance(transform.position, player.transform.position) < 5f)
        {
            inRange = false;
            player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, pull);
            GameController.player.TakeDamage(1);
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
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z), speed);
        }

        if (health <= 0)
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
            pull = 0f;
            GameController.player.TakeDamage(damage);
        }
    }

    /*
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
    */

    public void TakeDamage(float damage, PlayerConstants.DamageSource damageSource)
    {
        if (!takingDamage && damageSource != PlayerConstants.DamageSource.Wind)
        {
            health -= damage;
            takingDamage = true;
        }
    }

    public void Push(Vector3 distance)
    {

    }
}
