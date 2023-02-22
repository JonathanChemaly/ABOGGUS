using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact
{
    public class DisableThisInteractable : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("interact to watch")]
        private Interactable interact;

        // Start is called before the first frame update
        void Start()
        {
            interact.InteractAction += DisableThis;
        }

        private void DisableThis()
        {
            Destroy(interact);
        }
    }
}

