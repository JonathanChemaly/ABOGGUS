using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsPlayer;

    [Range(0f,1f)]
    public float bounciness;
    public bool useGravity;

    public float explosionDamage;
    public float explosionRange;

    public bool explodeOnTouch = true;

    PhysicMaterial physics_mat;

    private void Start()
    {
        Setup();
    }

    private void Explode()
    {
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        Collider[] player = Physics.OverlapSphere(transform.position, explosionRange, whatIsPlayer);

        for (int i = 0; i < player.Length; i++)
        {
            player[i].GetComponent<Player>().TakeDamage(explosionDamage);
        }
        Invoke("Delay", 0.05f);
        Invoke("ExplosionDelay", 1f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void ExplosionDelay()
    {
        var clones = GameObject.FindGameObjectsWithTag("Explosion");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }
    private void Setup()
    {
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<SphereCollider>().material = physics_mat;
        rb.useGravity = useGravity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Enemy") Explode();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
