using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ABOGGUS.Gameplay;

namespace ABOGGUS.PlayerObjects
{
    public class FireAttack : MonoBehaviour, IMagicAttack
    {
        public float damage = WeaponDamageStats.defaultFireDamage;
        private float speed = 0.18f;
        private float totalTime = 5f;
        private float time = 0f;
        private int manaCost = (int)(WeaponDamageStats.defaultFireCost * UpgradeStats.manaEfficiency);

        private void Start()
        {
            damage = WeaponDamageStats.fireDamage;
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
            else if (other.transform.CompareTag("Meltable")) StartCoroutine(Melt(other.gameObject));
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

        IEnumerator Melt(GameObject obj)
        {
            UnityEngine.Color objectColor = obj.transform.GetComponent<MeshRenderer>().material.color;
            while (obj.GetComponent<MeshRenderer>().material.color.a > 0)
            {
                float fadeAmount = objectColor.a - (Time.deltaTime);

                objectColor = new UnityEngine.Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                obj.GetComponent<MeshRenderer>().material.color = objectColor;
                yield return null;
            }
            Destroy(obj);
        }
    }
}