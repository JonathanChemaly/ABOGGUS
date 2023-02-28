using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact
{
    public class EnableColliderOnInteract : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("interact to watch")]
        private Interactable interact;

        [SerializeField]
        [Tooltip("collider to enable on interact")]
        private Collider colliderToEnable;

        // Start is called before the first frame update
        void Start()
        {
            colliderToEnable.enabled = false;
            interact.InteractAction += DisableThis;
        }

        private void DisableThis()
        {
            colliderToEnable.enabled = true;
        }
    }
}

