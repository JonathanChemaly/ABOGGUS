using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class FireAttack : MonoBehaviour, IMagicAttack
    {
        private float damage = WeaponDamageStats.defaultFireDamage;
        private float speed = 0.18f;
        private float totalTime = 5f;
        private float time = 0f;
        private void Start()
        {

        }

        void FixedUpdate()
        {
            Vector3 target = transform.position + transform.forward * speed;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, speed);
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
                rb.GetComponent<IEnemy>().TakeDamage(damage, PlayerConstants.DamageSource.Fire);
            }
            Destroy();
        }
    }
}