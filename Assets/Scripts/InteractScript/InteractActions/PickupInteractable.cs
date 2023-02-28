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
            player.inventory.key = true;
            interact.DoSuccesAction();
        }
    }
}

