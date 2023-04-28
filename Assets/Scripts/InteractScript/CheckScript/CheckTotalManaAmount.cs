using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;

namespace ABOGGUS.Interact.Checks
{
    public class CheckTotalManaAmount : MonoBehaviour, InteractCheck
    {

        [SerializeField]
        [Tooltip("Amount of mana the player must have accumulated to use interactable")]
        private int amountOfManaToHave;

        public bool DoCheck()
        {
            return UpgradeStats.totalMana >= amountOfManaToHave;
        }

        public string GetFailureText()
        {
            return "Must have " + amountOfManaToHave + " total accumulated mana.";
        }

    }
}

