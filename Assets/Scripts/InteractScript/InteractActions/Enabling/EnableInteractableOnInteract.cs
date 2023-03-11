using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact
{
    public class EnableInteractableOnInteract : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("interact to enable on the succes of the the other interactable")]
        private Interactable interactToEnable;

        [SerializeField]
        [Tooltip("interact to watch for success then enable the other")]
        private Interactable interactToWatch;

        private void Start()
        {
            interactToEnable.enabled = false;
            interactToWatch.InteractAction += EnableAction;
        }

        private void EnableAction()
        {
            interactToEnable.enabled = true;
        }
    }
}

