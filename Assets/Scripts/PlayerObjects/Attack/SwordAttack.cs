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
        private bool attacking = false;

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

        public void Attacking(bool attacking)
        {
            this.attacking = attacking;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.tag);
            if (active && attacking && other.transform.CompareTag("Slime")) DamageObject(other.GetComponent<Rigidbody>(), PlayerConstants.CollidedWith.Enemy);
            else if (active && attacking && other.transform.CompareTag("Boss")) DamageObject(other.GetComponent<Rigidbody>(), PlayerConstants.CollidedWith.Boss);
        }

        private void DamageObject(Rigidbody rb, PlayerConstants.CollidedWith collidedWith)
        {
            if (collidedWith == PlayerConstants.CollidedWith.Boss)
            {
                rb.GetComponent<Boss>().TakeDamage(damage);
            }
            else if (collidedWith == PlayerConstants.CollidedWith.Enemy)
            {
                rb.GetComponent<IEnemy>().TakeDamage(damage, PlayerConstants.DamageSource.Sword);
                rb.GetComponent<IEnemy>().Push(transform.root.forward * knockback);
            }
        }

        private void FixedUpdate()
        {
            gameObject.GetComponent<Renderer>().enabled = active;
        }
    }
}