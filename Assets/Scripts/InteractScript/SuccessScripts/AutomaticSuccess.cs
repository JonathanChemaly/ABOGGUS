using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;

namespace ABOGGUS.Interact
{
    public class AutomaticSuccess : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("interact to watch")]
        private Interactable interact;

        private Player player;

        // Start is called before the first frame update
        void Start()
        {
            interact.InteractAction += success;
            player = GameController.player;
        }

        private void success()
        {
            interact.DoSuccesAction();
        }
    }
}

