using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using ABOGGUS.PlayerObjects.Items;
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
            if (interact.CompareTag(ItemLookup.GrimoireName))
            {
                playerInv.AddItem(ItemLookup.GrimoireName);
            }
            else if (interact.CompareTag(ItemLookup.WheelName))
            {
                playerInv.AddItem(ItemLookup.WheelName);
            }
            else if (interact.CompareTag(ItemLookup.WrenchName))
            {
                playerInv.AddItem(ItemLookup.WrenchName);
            }
            else if (interact.CompareTag(ItemLookup.GasName))
            {
                playerInv.AddItem(ItemLookup.GasName);
            }
            else if (interact.CompareTag(ItemLookup.TractorKeyName))
            {
                playerInv.AddItem(ItemLookup.TractorKeyName);
            }
            else if (interact.CompareTag(ItemLookup.HammerName))
            {
                playerInv.addItem(ItemLookup.HammerName);
            }
            itemInfo.SetValue(playerInv, true);
            interact.DoSuccesAction();
        }
    }
}
