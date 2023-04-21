using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;

namespace ABOGGUS.PlayerObjects
{
    public class WindAOEAttack : MonoBehaviour, IMagicAttack
    {
        public int damage = WeaponDamageStats.windAOEDamage;
        private float height = 5f;
        private float totalTime = 2f;
        private float activeTime = 1.5f;
        private float time = 0f;
        private int manaCost = (int)(WeaponDamageStats.defaultWindAOECost * UpgradeStats.manaEfficiency);

        private void Start()
        {
            damage = WeaponDamageStats.windAOEDamage;
            if (UpgradeStats.CanDealBonusDamAtMaxHealth())
            {
                damage = (int)(damage * UpgradeStats.bonusDamMultiplier);
            }
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
                rb.GetComponent<IEnemy>().TakeDamage(damage, PlayerConstants.DamageSource.Wind);
                rb.GetComponent<IEnemy>().Push(new Vector3(0, height - rb.position.y, 0));
            }
        }
    }
}
