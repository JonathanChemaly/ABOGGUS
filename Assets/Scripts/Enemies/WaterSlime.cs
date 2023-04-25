using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;
public class WaterSlime : MonoBehaviour, IEnemy
{
    private GameObject player;
    private Animator animator;
    private bool inRange = false;
    private bool pop = false;
    private float range = 15f;
    private float speed = 0.1f;
    private float timer = 1.2f;
    private float health = 40f;
    private bool takingDamage = false;
    private float invTime = 0.3f;
    private float damageTimer = 0f;
    public float damage = 10f;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private GameObject ElementalDrop;
    private int damping = 2;
    private Transform target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        animator = GetComponent<Animator>();
        animator.fireEvents = false;
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

        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            inRange = false;
            pop = true;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            inRange = true;
            Vector3 lookPos = target.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        }
        else
        {
            inRange = false;
        }

        if (inRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0f, 1f, 0f), speed);
            if (!pop)
            {
                animator.Play("Jump");
            }
        }

        if (health <= 0)
        {
            pop = true;
        }

        if (pop)
        {
            timer -= Time.deltaTime;
            transform.localScale += new Vector3(0.03f, 0.02f, 0.03f);

            if (timer < 0)
            {
                Instantiate(ElementalDrop, transform.position, Quaternion.identity);
                deathSound.Play();
                Destroy(gameObject);
            }
        }

        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //slow the player
            GameController.player.TakeDamage(damage);
            Debug.Log("pop");
        }
        if (collision.gameObject.tag == "Slime" && pop){
            Destroy(collision.gameObject);
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
        if (!takingDamage && damageSource != PlayerConstants.DamageSource.Water)
        {
            health -= damage;
            if (damageSource == PlayerConstants.DamageSource.Wind)
            {
                health -= damage;
            }
            takingDamage = true;
        }
    }

    public void Push(Vector3 distance)
    {
        transform.position = transform.position + distance;
    }
}
