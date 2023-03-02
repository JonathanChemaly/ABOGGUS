using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class WindAOEAttack : MonoBehaviour, IMagicAttack
    {
        private int damage = 3;
        private float height = 5f;
        private float totalTime = 0.3f;
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
            if (other.transform.tag == "Enemy") MoveObject(other.GetComponent<Rigidbody>(), true);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Enemy") MoveObject(collision.rigidbody, true);
        }

        private void MoveObject(Rigidbody rb, bool enemy)
        {
            if (enemy)
            {
                rb.GetComponent<Boss>().TakeDamage(damage);
            }
            rb.MovePosition(rb.position + new Vector3(0, height, 0));
        }
    }
}
