using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;

namespace ABOGGUS.PlayerObjects
{
    public class WaterAOEAttack : MonoBehaviour, IMagicAttack
    {
        public int damage = WeaponDamageStats.waterAOEDamage;
        private float totalTime = 2f;
        private float activeTime = 1.5f;
        private float time = 0f;
        private int manaCost = (int)(WeaponDamageStats.defaultWaterAOECost * UpgradeStats.manaEfficiency);

        private void Start()
        {
            if (GameController.player.inventory.HasMana(manaCost))
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
                rb.GetComponent<IEnemy>().Push(transform.position - rb.position);
            }
        }
    }
}