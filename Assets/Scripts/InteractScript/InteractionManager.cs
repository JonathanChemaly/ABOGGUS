using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ABOGGUS.Interact
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("What camera to see what you are interacting with")]
        private Camera FPScam;

        [SerializeField]
        [Tooltip("Game object containing Gui to enable or disable when necessary")]
        private GameObject InteractionUI;

        [SerializeField]
        [Tooltip("What layer to search for items on")]
        private LayerMask layerToSearch;

        [SerializeField]
        [Tooltip("Max distance that you can look at an interactable and be able to interact with it")]
        private float maxRayDistance = 2.0f;

        [SerializeField]
        [Tooltip("UI holder of item name")]
        private TextMeshProUGUI itemName;

        [SerializeField]
        [Tooltip("UI holder of Action name")]
        private TextMeshProUGUI actionName;

        public event Action ObjectNameChangeEvent;

        private Interactable currentInteractable;

        // Start is called before the first frame update
        private void Start()
        {
            InteractionUI.SetActive(false);
        }

        // Update is called once per frame
        private void Update()
        {
            RayCastCheck(); //does interactable finding logic

            if (LookingAtInteractable()) //if we are looking at something ...
            {
                //enable our interaction UI
                InteractionUI.SetActive(true);
                if (UnityEngine.Input.GetKeyDown(KeyCode.E))
                {
                    currentInteractable.doAction();
                }
            }
            else //if we are not looking at something ...
            {
                //disable our interaction UI
                InteractionUI.SetActive(false);
            }
        }
        /**
         * checks if we are looking at an object
         */
        private bool LookingAtInteractable()
        {
            return currentInteractable != null;
        }

        /**
         * Sends a ray out from where are camera is looking to see if the camera is pointed at an interactable object
         * Sets Current interactable to null if there is no
         */
        private void RayCastCheck()
        {
            Ray lookAtRay = FPScam.ViewportPointToRay(Vector3.one / 2f); //creates ray where camera is looking


            //Checks if we get a hit off an item in our layer of choice.
            if (Physics.Raycast(lookAtRay, out RaycastHit rayHitInfo, maxRayDistance, layerToSearch))
            {
                //if we got a hit the object should have an interacable item attached to it so we get it
                Interactable hitObject = rayHitInfo.collider.GetComponent<Interactable>();

                //if for some reason the object is null (for example didn't get the component right)
                //we ignore it
                if (hitObject == null)
                {
                    currentInteractable = null;
                }
                else if (hitObject != currentInteractable) //if not looking at same object as before
                {
                    currentInteractable = hitObject; //set current object to new object
                    itemName.text = currentInteractable.itemName; //sets the names in the pop ui correctly
                    actionName.text = currentInteractable.actionName;
                    ObjectNameChangeEvent?.Invoke(); //invokes event to scale UI properly
                }
            }
            else //if we do not get a hit set our current item to null
            {
                currentInteractable = null;
            }
        }
    }
}
