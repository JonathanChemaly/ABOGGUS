using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;
public class LightningSlime : MonoBehaviour, IEnemy
{
    private GameObject player;
    public GameObject lightningAOE;
    private bool inRange = false;
    private bool dead = false;
    private float range = 15f;
    private float speed = 0.09f;
    private float timer = 2f;
    private float deathTimer = 1f;
    private float health = 60f;
    private bool takingDamage = false;
    private float invTime = 0.3f;
    private float damageTimer = 0f;
    public float damage = 10f;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource zapSound;
    [SerializeField] private GameObject ElementalDrop;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        //Update invisibility timer while taking damage
        if (takingDamage)
        {
            damageTimer += Time.fixedDeltaTime;
            if (damageTimer > invTime)
            {
                takingDamage = false;
                damageTimer = 0;
            }
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 3f)
        {
            if (timer == 2f)
            {
                zapSound.Play();
                GameController.player.TakeDamage(damage);
            }
            
            timer -= Time.deltaTime;
            inRange = false;

            if (timer > 0)
            {
                lightningAOE.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
            }
            else if (timer < 0 && timer > -2)
            {
                lightningAOE.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            }
            else if (timer <= -2)
            {
                timer = 2f;
                inRange = true;
            }
            //player take damage from lighting area turn off lightning area and freeze for a few seconds
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
            transform.LookAt(player.transform.position + new Vector3(0, 0.3f, 0f));
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
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
        if (!takingDamage && damageSource != PlayerConstants.DamageSource.Lightning)
        {
            health -= damage;
            takingDamage = true;
        }
    }

    public void Push(Vector3 distance)
    {
        transform.position = transform.position + distance;
    }
}
