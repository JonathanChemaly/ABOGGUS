using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles
{
    public class RemoveCuff : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("LockManager that this object is listening too")]
        private LockManager LM;

        [SerializeField]
        [Tooltip("LockManager that this object is listening too")]
        private float speed = 0.1f;

        [SerializeField]
        [Tooltip("LockManager that this object is listening too")]
        private float yPostionToStop = 10f;

        // Start is called before the first frame update
        void Start()
        {
            LM.doOnCorrectPassword += moveObject;
        }

        private void moveObject()
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

