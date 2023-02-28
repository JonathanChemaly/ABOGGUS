using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ABOGGUS.Interact
{
    public class OnSuccessMove : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("interact to watch")]
        private Interactable interact;

        [SerializeField]
        [Tooltip("LockManager that this object is listening too")]
        private float speed = 0.1f;

        [SerializeField]
        [Tooltip("LockManager that this object is listening too")]
        private float yPostionToStop = 10f;

        void Start()
        {
            interact.SuccessAction += MoveObject;
        }

        private void MoveObject()
        {
            StartCoroutine(MoveUpOverTime());
        }

        IEnumerator MoveUpOverTime()
        {
            while (transform.position.y < yPostionToStop)
            {
                yield return null;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, speed);
            }
        }
    }
}

