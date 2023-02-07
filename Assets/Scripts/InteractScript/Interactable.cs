using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ABOGGUS.Interact {
    public class Interactable : MonoBehaviour
    {
        [Tooltip("The name of the object to appear in UI when looked at")]
        public string itemName = "No Name";

        [Tooltip("The name of the action to appear in UI when looked at")]
        public string actionName = "Interact";

        [SerializeField]
        [Tooltip("What action will be taken when item is interacted with")]
        public event Action InteractAction;

        [SerializeField]
        [Tooltip("What action will be taken when a specfic set of actions are completed in UI")]
        public event Action SuccessAction;

        public void DoAction()
        {
            InteractAction?.Invoke();
        }

        public void DoSuccesAction()
        {
            SuccessAction?.Invoke();
        }
    }
}

