using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;
public class WaterSlime : MonoBehaviour
{
    private GameObject player;
    private bool inRange = false;
    private bool pop = false;
    private float range = 15f;
    private float speed = 0.1f;
    private float timer = 1.2f;
    private float health = 1f;
    public float damage = 10f;
    [SerializeField] private AudioSource deathSound;
    private int damping = 2;
    private Transform target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    void FixedUpdate()
    {
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
        }

        if (health == 0)
        {
            pop = true;
        }

        if (pop)
        {
            timer -= Time.deltaTime;
            transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);

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
            //slow the player
            GameController.player.TakeDamage(damage);
            Debug.Log("pop");
        }
        if (collision.gameObject.tag == "Slime" && pop){
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" || other.gameObject.tag == "MagicAttack")
        {
            Debug.Log("Grass Slime health:" + health);
            health -= 1;
        }
    }
}
