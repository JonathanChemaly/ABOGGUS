using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;
using ABOGGUS.BossObjects;

namespace ABOGGUS.PlayerObjects
{
    public class FireAOEAttack : MonoBehaviour, IMagicAttack
    {
        public float damage = WeaponDamageStats.defaultFireAOEDamage;
        private float totalTime = 10f;
        private float activeTime = 1.5f;
        private float time = 0f;
        private int manaCost = (int)(WeaponDamageStats.defaultFireAOECost * UpgradeStats.manaEfficiency);

        private void Start()
        {
            damage = WeaponDamageStats.fireAOEDamage;
            if (UpgradeStats.CanDealBonusDamAtMaxHealth())
            {
                damage = damage * UpgradeStats.bonusDamMultiplier;
            }

            if (GameController.PuzzleScene())
            {
                //Do nothing
            }
            else if (GameController.player.inventory.HasMana(manaCost))
            {
                GameController.player.inventory.UseMana(manaCost);
                StartCoroutine(ActivateAfterDelay());
            }
            else
            {
                Destroy();
            }
        }

        IEnumerator ActivateAfterDelay()
        {
            yield return new WaitForSeconds(activeTime);
        }
        void FixedUpdate()
        {
            if (Time.deltaTime + time >= totalTime)
            {
                Destroy(gameObject);
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
            else if (other.transform.CompareTag("Player")) GameController.player.TakeDamage(damage);
        }

        private void DamageObject(Rigidbody rb, PlayerConstants.CollidedWith collidedWith)
        {
            if (collidedWith == PlayerConstants.CollidedWith.Boss)
            {
                rb.GetComponent<Boss>().TakeDamage(damage);
            }
            else if (collidedWith == PlayerConstants.CollidedWith.Enemy)
            {
                rb.GetComponent<IEnemy>().TakeDamage(damage, PlayerConstants.DamageSource.Fire);
            }
        }
    }
}
