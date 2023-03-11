using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class WindAttack : MonoBehaviour, IMagicAttack
    {
        private int damage = 5;
        private float speed = 0.2f;
        private void Start()
        {

        }

        void FixedUpdate()
        {
            Vector3 target = transform.position + transform.forward * speed;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, speed);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Moveable") MoveObject(other.GetComponent<Rigidbody>(), false);
            else if (other.transform.tag == "Enemy") MoveObject(other.GetComponent<Rigidbody>(), true);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Moveable") MoveObject(collision.rigidbody, false);
            else if (collision.transform.tag == "Enemy") MoveObject(collision.rigidbody, true);
        }

        private void MoveObject(Rigidbody rb, bool enemy)
        {
            if (enemy)
            {
                rb.GetComponent<Boss>().TakeDamage(damage);
            }
            rb.MovePosition(rb.position + transform.forward);
            Destroy();
        }
    }
}