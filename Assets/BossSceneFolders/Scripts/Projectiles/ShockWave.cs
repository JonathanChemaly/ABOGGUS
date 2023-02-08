using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    public LayerMask whatIsPlayer;

    public int explosionDamage;
    public float explosionRange;
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
            player[i].GetComponent<Player>().TakeDamage(explosionDamage);
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
