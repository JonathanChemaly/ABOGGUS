using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class SwordAttack : MonoBehaviour
    {
        public int damage;
        private float knockback = 0.2f;
        private bool active = true;

        private void Awake()
        {
            damage = WeaponDamageStats.swordDamage;
        }

        public void Unequip()
        {
            active = false;
        }

        public void Equip()
        {
            active = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (active && other.transform.CompareTag("Enemy")) MoveObject(other.GetComponent<Rigidbody>(), true);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (active && collision.transform.CompareTag("Enemy")) MoveObject(collision.rigidbody, true);
        }

        private void MoveObject(Rigidbody rb, bool enemy)
        {
            if (enemy)
            {
                rb.GetComponent<Boss>().TakeDamage(damage);
            }
            rb.MovePosition(rb.position + transform.root.forward * knockback);
        }

        private void FixedUpdate()
        {
            gameObject.GetComponent<Renderer>().enabled = active;
        }
    }
}