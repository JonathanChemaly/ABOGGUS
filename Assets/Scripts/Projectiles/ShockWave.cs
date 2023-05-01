using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;

public class ShockWave : MonoBehaviour
{
    public LayerMask whatIsPlayer;

    public int explosionDamage;
    public float explosionRange;

    public bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Explode()
    {

        Collider[] player = Physics.OverlapSphere(transform.position, explosionRange, whatIsPlayer);

        for (int i = 0; i < player.Length; i++)
        {
            if (active)
            {
                //player[i].GetComponent<Player>().TakeDamage(explosionDamage);
                GameController.player.TakeDamage(explosionDamage, true);
                active = false;
            }
        }
        Invoke("Delay", 2f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Enemy") Explode();
        if (collision.transform.name == "Player") gameObject.GetComponent<SphereCollider>().enabled = false;
    }
}
