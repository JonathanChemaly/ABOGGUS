using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;

namespace ABOGGUS.Interact
{
    public class PickupInteractable : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("interact to watch")]
        private Interactable interact;

        [SerializeField]
        [Tooltip("name of bool field in player inventory to check if the player has (case-sensitive)")]
        private string itemName = "key";

        private PlayerInventory playerInv;
        private System.Reflection.PropertyInfo itemInfo;

        // Start is called before the first frame update
        void Start()
        {
            interact.InteractAction += pickUpObject;
            playerInv = GameController.player.inventory;
            itemInfo = playerInv.GetType().GetProperty(itemName);
        }

        private void pickUpObject()
        {
            if (interact.CompareTag("grimore")) {
                playerInv.grimore = true;
            }
            else if (interact.CompareTag("wheel"))
            {
                playerInv.wheel = true;
            }
            else if (interact.CompareTag("wrench"))
            {
                playerInv.wrench = true;
            }
            else if (interact.CompareTag("gas"))
            {
                playerInv.gas = true;
            }
            else if (interact.CompareTag("tractorkey"))
            {
                playerInv.tractorkey = true;
            }
            itemInfo.SetValue(playerInv, true);
            interact.DoSuccesAction();
        }
    }
}
