using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;
public class ElementalDrop : MonoBehaviour
{
    private GameObject player;
    private bool inRange = false;
    private float range = 10f;
    private float speed = 0.2f;
    [SerializeField] private AudioSource deathSound;
    private float timer = 0.3f;
    private Vector3 createdPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        createdPos = transform.position;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range && timer > 0)
        {
            inRange = true;
        }
        if (inRange)
        {
            timer -= Time.deltaTime;
            if (timer > 0)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                transform.position = Vector3.MoveTowards(transform.position, createdPos + new Vector3(0f, 1.5f, 0f), speed);
            }
            else if (timer < 0)
            {
                speed = 0.2f;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0f, 1.5f, 0f), speed);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameController.player.updateMana(1);
            deathSound.Play();
            Destroy(gameObject);
        }
    }
}
