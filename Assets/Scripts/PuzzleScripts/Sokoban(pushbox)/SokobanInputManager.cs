using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Input;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class SokobanInputManager : MonoBehaviour
    {
        // Start is called before the first frame update
        private InputActions InputScheme;
        private bool additiveLoaded = false;

        [SerializeField]
        [Tooltip("Material to apply to the floor")]
        private SokobanMovementController sokobanMovementController;

        void Start()
        {
            InputManager[] imList = GameObject.FindObjectsOfType<InputManager>();
            if(imList.Length > 0)
            {
                InputScheme = imList[0].InputScheme;
                //disable all player input
                InputScheme.Player.Disable();
                additiveLoaded = true;
            }
            else
            {
                InputScheme = new Input.InputActions();
            }
            sokobanMovementController.InitializeInput(InputScheme);
        }

        private void OnDestroy()
        {
            if (additiveLoaded)
            {
                InputScheme.Player.Enable();
            }
        }

        private void OnDisable()
        {
            if (additiveLoaded)
            {
                InputScheme.Player.Enable();
            }
        }
    }
}

