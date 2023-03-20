using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;

public class FireBall : MonoBehaviour
{

    public float damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            GameController.player.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MagicAttack")
        {
            if (other.GetComponent<WindAttack>() != null)
            {
                other.GetComponent<WindAttack>().Destroy();
            }
        }
    }

}
