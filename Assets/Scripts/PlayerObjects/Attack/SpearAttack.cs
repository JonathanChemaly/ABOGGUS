using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.BossObjects;

namespace ABOGGUS.PlayerObjects
{
    public class SpearAttack : MonoBehaviour
    {
        public float damage = WeaponDamageStats.spearDamage;

        private float knockback = 0.4f;
        private bool active;
        private bool attacking;

        private void Awake()
        {
            
        }

        public void Unequip()
        {
            active = false;
        }

        public void Equip()
        {
            active = true;
        }

        public bool GetStatus()
        {
            return active;
        }

        public void Attacking(bool attacking)
        {
            this.attacking = attacking;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (active && attacking && other.transform.CompareTag("Slime")) DamageObject(other.GetComponent<Rigidbody>(), PlayerConstants.CollidedWith.Enemy);
            else if (active && attacking && other.transform.CompareTag("Boss")) DamageObject(other.GetComponent<Rigidbody>(), PlayerConstants.CollidedWith.Boss);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (active && attacking && collision.transform.CompareTag("Enemy")) DamageObject(collision.rigidbody, PlayerConstants.CollidedWith.Enemy);
            else if (active && attacking && collision.transform.CompareTag("Boss")) DamageObject(collision.rigidbody, PlayerConstants.CollidedWith.Boss);
        }

        private void DamageObject(Rigidbody rb, PlayerConstants.CollidedWith collidedWith)
        {
            if (UpgradeStats.CanDealBonusDamAtMaxHealth())
            {
                damage = (int)(WeaponDamageStats.spearDamage * UpgradeStats.bonusDamMultiplier);
            }
            else
            {
                damage = WeaponDamageStats.spearDamage;
            }
            if (collidedWith == PlayerConstants.CollidedWith.Boss)
            {
                rb.GetComponent<Boss>().TakeDamage(damage);
            }
            else if (collidedWith == PlayerConstants.CollidedWith.Enemy)
            {
                rb.GetComponent<IEnemy>().TakeDamage(damage, PlayerConstants.DamageSource.Spear);
                rb.GetComponent<IEnemy>().Push(transform.root.forward * knockback);
            }
        }

        private void FixedUpdate()
        {
            gameObject.GetComponent<Renderer>().enabled = active;
        }
    }
}