using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact
{
    public class OnSuccessActive : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("interact to watch for success")]
        private Interactable interactToWatch;

        [SerializeField]
        [Tooltip("gameobject to activate")]
        private GameObject activation;

        private void Start()
        {
            interactToWatch.SuccessAction += ActivateAction;
        }

        private void ActivateAction()
        {
            activation.SetActive(true);
        }
    }
}

