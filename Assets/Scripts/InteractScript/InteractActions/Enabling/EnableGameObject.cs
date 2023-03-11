using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact
{
    public class EnableGameObject : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Gameobject component to enable on the success of the the other interactable")]
        private GameObject component;

        [SerializeField]
        [Tooltip("interact to watch for success then enable the other")]
        private Interactable interactToWatch;

        private void Start()
        {
            component.SetActive(false);
            interactToWatch.InteractAction += EnableComp;
        }

        private void EnableComp()
        {
            component.SetActive(true);
        }
    }
}

