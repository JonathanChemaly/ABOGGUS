using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;

namespace ABOGGUS.PlayerObjects
{
    public class WaterAttack : MonoBehaviour, IMagicAttack
    {
        public int damage = WeaponDamageStats.waterDamage;
        private float speed = 0.05f;
        private float totalTime = 1f;
        private float time = 0f;
        private int manaCost = (int)(WeaponDamageStats.defaultWaterCost * UpgradeStats.manaEfficiency);

        private void Start()
        {
            damage = WeaponDamageStats.waterDamage;
            if (UpgradeStats.CanDealBonusDamAtMaxHealth())
            {
                damage = (int)(damage * UpgradeStats.bonusDamMultiplier);
            }

            if (GameController.PuzzleScene())
            {
                //Do nothing
            }
            else if (GameController.player.inventory.HasMana(manaCost))
            {
                GameController.player.inventory.UseMana(manaCost);
            }
            else
            {
                Destroy();
            }
        }

        void FixedUpdate()
        {
            Vector3 target = transform.position + transform.forward * speed;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, speed);
            if (Time.deltaTime + time >= totalTime)
            {
                Destroy();
                time = 0;
            }
            time += Time.deltaTime;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log(other.tag);
            if (other.transform.CompareTag("Slime")) DamageObject(other.GetComponent<Rigidbody>(), PlayerConstants.CollidedWith.Enemy);
            else if (other.transform.CompareTag("Boss")) DamageObject(other.GetComponent<Rigidbody>(), PlayerConstants.CollidedWith.Boss);
        }

        private void DamageObject(Rigidbody rb, PlayerConstants.CollidedWith collidedWith)
        {
            if (collidedWith == PlayerConstants.CollidedWith.Boss)
            {
                rb.GetComponent<Boss>().TakeDamage(damage);
            }
            else if (collidedWith == PlayerConstants.CollidedWith.Enemy)
            {
                rb.GetComponent<IEnemy>().TakeDamage(damage, PlayerConstants.DamageSource.Water);
                rb.GetComponent<IEnemy>().Push(transform.forward);
            }
        }
    }
}