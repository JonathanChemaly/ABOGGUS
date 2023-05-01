using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;

public class Vine : MonoBehaviour
{

    public float damage;
    private float destroyDelay = 1.0f;

    private void Start()
    {
        Invoke("Delay", destroyDelay);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            GameController.player.TakeDamage(damage, true);
            Invoke("Delay", destroyDelay);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
