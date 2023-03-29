using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;

namespace ABOGGUS.Interact.Checks
{
    public class InventoryCheckForInteractable : MonoBehaviour, InteractCheck
    {

        [SerializeField]
        [Tooltip("name of bool field in player inventory to check if the player has (case-sensitive)")]
        private string itemName;

        [SerializeField]
        [Tooltip("what to display in the case item is missing from player inventory")]
        private string failureText = "Missing Required Item";

        private PlayerInventory playerInv;

        private System.Reflection.PropertyInfo itemInfo;

        public bool DoCheck()
        {
            
            return playerInv.hasItem(itemName);
        }

        public string GetFailureText()
        {
            return failureText;
        }

        // Start is called before the first frame update
        void Start()
        {
            playerInv = GameController.player.inventory;
            
            //itemInfo = playerInv.GetType().GetProperty(itemName);
            
        }
    }
}

