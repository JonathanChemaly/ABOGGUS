using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact
{
    public class DisableColliderOnInteract : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("interact to watch")]
        private Interactable interact;

        [SerializeField]
        [Tooltip("collider to disable on interact")]
        private Collider colliderToDisable;

        // Start is called before the first frame update
        void Start()
        {
            colliderToDisable.enabled = true;
            interact.InteractAction += DisableThis;
        }

        private void DisableThis()
        {
            colliderToDisable.enabled = false;
        }
    }
}

