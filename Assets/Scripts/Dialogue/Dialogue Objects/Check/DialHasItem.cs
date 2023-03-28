using ABOGGUS.Gameplay;
using ABOGGUS.PlayerObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Sound.Dialogue
{
    public class DialHasItem : MonoBehaviour, IDialogueCheck
    {
        [SerializeField]
        [Tooltip("name of bool field in player inventory to check if the player has (case-sensitive)")]
        private string itemName;

        [SerializeField]
        [Tooltip("what to display in the case item is missing from player inventory")]
        private string failureText = "Missing Required Item";

        private PlayerInventory playerInv;

        private System.Reflection.PropertyInfo itemInfo;

        // Start is called before the first frame update
        public void Start()
        {
            playerInv = GameController.player.inventory;

            itemInfo = playerInv.GetType().GetProperty(itemName);

        }

        public bool doCheck()
        {
            return (bool)itemInfo.GetValue(playerInv);
        }


    }
}