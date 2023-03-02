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

        private Player player;

        // Start is called before the first frame update
        void Start()
        {
            interact.InteractAction += pickUpObject;
            player = GameController.player;
        }

        private void pickUpObject()
        {
            if (interact.CompareTag("grimore")) {
                player.inventory.grimore = true;
            }
            else if (interact.CompareTag("wheel"))
            {
                player.inventory.wheel = true;
            }
            else if (interact.CompareTag("wrench"))
            {
                player.inventory.wrench = true;
            }
            else if (interact.CompareTag("gas"))
            {
                player.inventory.gas = true;
            }
            else if (interact.CompareTag("tractorkey"))
            {
                player.inventory.tractorkey = true;
            }
            interact.DoSuccesAction();
        }
    }
}

