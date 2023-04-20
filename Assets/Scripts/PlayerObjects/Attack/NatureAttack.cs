using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;

namespace ABOGGUS.PlayerObjects
{
    public class NatureAttack : MonoBehaviour, IMagicAttack
    {
        public int damage = WeaponDamageStats.natureDamage;
        private float totalTime = 10f;
        private float time = 0f;
        private int manaCost = (int)(WeaponDamageStats.defaultNatureCost * UpgradeStats.manaEfficiency);
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
            animator.fireEvents = false;
            damage = WeaponDamageStats.natureDamage;
            if (GameController.player.inventory.HasMana(manaCost))
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
            animator.Play(PlayerAnimationStateController.currentState);
            Debug.Log("Nature State: " + PlayerAnimationStateController.currentState);
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

        private void OnTriggerEnter(Collider other)
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
                rb.GetComponent<IEnemy>().TakeDamage(damage, PlayerConstants.DamageSource.Nature);
            }
        }
    }
}