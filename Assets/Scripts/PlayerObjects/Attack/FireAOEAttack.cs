using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class FireAOEAttack : MonoBehaviour, IMagicAttack
    {
        private int damage = 3;
        private float totalTime = 10f;
        private float activeTime = 1.5f;
        private float time = 0f;
        private void Start()
        {
            StartCoroutine(ActivateAfterDelay());
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Enemy") DamageObject(other.GetComponent<Rigidbody>(), true);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Enemy") DamageObject(collision.rigidbody, true);
        }

        private void DamageObject(Rigidbody rb, bool enemy)
        {
            if (enemy)
            {
                rb.GetComponent<Boss>().TakeDamage(damage);
            }
        }
    }
}
