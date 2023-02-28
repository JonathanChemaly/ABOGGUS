using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class SwordAttack : MonoBehaviour
    {
        private int damage = 1;
        private float knockback = 0.2f;

        private void Awake()
        {
            
        }

        public void Unequip()
        {
            gameObject.SetActive(false);
        }

        public void Equip()
        {
            gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag.Equals("Enemy")) MoveObject(other.GetComponent<Rigidbody>(), true);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag.Equals("Enemy")) MoveObject(collision.rigidbody, true);
        }

        private void MoveObject(Rigidbody rb, bool enemy)
        {
            if (enemy)
            {
                rb.GetComponent<Boss>().TakeDamage(damage);
            }
            rb.MovePosition(rb.position + transform.root.forward * knockback);
        }
    }
}