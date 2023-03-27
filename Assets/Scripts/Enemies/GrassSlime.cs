using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;
public class GrassSlime : MonoBehaviour, IEnemy
{
    private GameObject player;
    public GameObject vine;
    private bool inRange = false;
    private bool dead = false;
    private float range = 15f;
    private float speed = 0.075f;
    private float timer = 1.5f;
    private float deathTimer = 1f;
    private float health = 60f;
    private bool takingDamage = false;
    private float invTime = 0.3f;
    private float damageTimer = 0f;
    public float damage = 10f;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource vineSound;
    [SerializeField] private GameObject ElementalDrop;
    private int damping = 2;
    private Transform target;
    private bool attacking = false;
    private float attackDelay = 1.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
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

        if (Vector3.Distance(transform.position, player.transform.position) < 2.5f)
        {
            attacking = true;
            if (timer == 1.5f)
            {
                VineAttack();
                vineSound.Play();
            }
            inRange = false;
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 1.5f;
                
            }
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            inRange = true;
            if (attacking) Invoke("AttackDelay", attackDelay);
        }
        else
        {
            inRange = false;
        }

        if (inRange && !attacking)
        {
            Vector3 lookPos = target.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
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
        if (collision.gameObject.tag == "Sword" || collision.gameObject.tag == "MagicAttack")
        {
            Debug.Log("Grass Slime health:" + health);
            health -= 1;
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" || other.gameObject.tag == "MagicAttack")
        {
            health -= 1;
            if (other.GetComponent<WindAttack>() != null)
            {
                other.GetComponent<WindAttack>().Destroy();

            }
        }

    }
    */

    private void AttackDelay()
    {
        attacking = false;
    }
    private void VineAttack()
    {
        var VineProjectile = Instantiate(vine, transform.position + transform.forward*1.5f, Quaternion.identity).GetComponent<Rigidbody>();
    }

    public void TakeDamage(float damage, PlayerConstants.DamageSource damageSource)
    {
        if (!takingDamage && damageSource != PlayerConstants.DamageSource.Nature)
        {
            health -= damage;
            takingDamage = true;
            if (damageSource == PlayerConstants.DamageSource.Fire)
            {
                health -= damage;
            }
        }
    }

    public void Push(Vector3 distance)
    {
        transform.position = transform.position + distance;
    }
}
