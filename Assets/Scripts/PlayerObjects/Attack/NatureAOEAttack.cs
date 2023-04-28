using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;
using ABOGGUS.BossObjects;

namespace ABOGGUS.PlayerObjects
{
    public class NatureAOEAttack : MonoBehaviour, IMagicAttack
    {
        public int healing = WeaponDamageStats.natureAOEHealing;
        private float totalTime = 10f;
        private float activeTime = 1.5f;
        private float timeBetweenHealing = 3.6f;
        private float time = 0f;
        private float healTime = 0f;
        private int manaCost = (int)(WeaponDamageStats.defaultNatureAOECost * UpgradeStats.manaEfficiency);

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
            if (other.CompareTag("Player"))
                HealPlayer();
        }

        private void HealPlayer()
        {
            if (Time.deltaTime + healTime >= timeBetweenHealing)
            {
                GameController.player.TakeDamage(-healing);
                healTime = 0;
            }
            else
            {
                healTime += Time.deltaTime;
            }
        }
    }
}
