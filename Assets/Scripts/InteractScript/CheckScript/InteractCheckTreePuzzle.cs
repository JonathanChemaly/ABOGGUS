using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;
using ABOGGUS.Interact.Puzzles;

namespace ABOGGUS.Interact.Checks
{
    public class InteractCheckTreePuzzle : MonoBehaviour, InteractCheck
    {

        [SerializeField]
        [Tooltip("name of bool field in player inventory to check if the player has (case-sensitive)")]
        private string itemName;

        [SerializeField]
        [Tooltip("what to display in the case item is missing from player inventory")]
        private string failureText = "Missing Required Item";

        [SerializeField]
        [Tooltip("what to display when player has already interacted with puzzle")]
        private string failureText2 = "Already Watered this Run";

        private PlayerInventory playerInv;

        public bool DoCheck()
        {
            return UpgradeStats.runs > TreePuzzle.latestRun && playerInv.HasItem(itemName);
        }

        public string GetFailureText()
        {
            if (UpgradeStats.runs <= TreePuzzle.latestRun) return failureText2;
            return failureText;
        }

        // Start is called before the first frame update
        void Start()
        {
            playerInv = GameController.player.inventory;
        }
    }
}

