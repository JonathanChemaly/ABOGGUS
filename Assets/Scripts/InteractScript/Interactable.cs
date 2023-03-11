using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using ABOGGUS.Interact.Checks;

namespace ABOGGUS.Interact {
    public class Interactable : MonoBehaviour
    {
        [Tooltip("The name of the object to appear in UI when looked at")]
        public string itemName = "No Name";

        [Tooltip("The name of the action to appear in UI when looked at")]
        public string actionName = "Interact";

        [Tooltip("What action will be taken when item is interacted with")]
        public event Action InteractAction;

        [SerializeField]
        [Tooltip("What action will be taken when a specfic set of actions are completed in UI")]
        public event Action SuccessAction;

        [SerializeField]
        [Tooltip("LinkToMonobehavior object that is an InteractAction. calls this classes functions to check if items are in inventory for example")]
        private MonoBehaviour interactCheck = null;

        [HideInInspector]
        [Tooltip("bool to signify when certain conditions have been satisfied for the interactable to activate")]
        public InteractCheck conditionCheck;

        [HideInInspector]
        [Tooltip("Bool for saving")]
        public bool InteractSuccessful = false;

        public void Start()
        {
            conditionCheck = (InteractCheck) interactCheck;
        }

        public void DoAction()
        {
            InteractAction?.Invoke();
        }

        public void DoSuccesAction()
        {
            InteractSuccessful = true;
            SuccessAction?.Invoke();
        }
    }
}

