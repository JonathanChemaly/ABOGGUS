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
        [Tooltip("Movement Controller that we intialize")]
        private SokobanMovementController sokobanMovementController;

        //[SerializeField]
        //[Tooltip("Material to apply to the sky")]
        //private Material skyBoxMaterial;

        //private Material oldSkyBoxMaterial;
        
        void Start()
        {
            //Skybox changes
            //oldSkyBoxMaterial = RenderSettings.skybox;
            //RenderSettings.skybox = skyBoxMaterial;

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
                GenerateSokoban[] genSokList = GameObject.FindObjectsOfType<GenerateSokoban>();
                genSokList[0].enabled = true;
            }
            sokobanMovementController.InitializeInput(InputScheme);

            
        }

        private void RevertSkyBox()
        {
            //RenderSettings.skybox = oldSkyBoxMaterial;
        }

        private void OnDestroy()
        {
            if (additiveLoaded)
            {
                InputScheme.Player.Enable();
                InputScheme.Sokoban.Disable();
                //RevertSkyBox();
            }
        }

        private void OnDisable()
        {
            if (additiveLoaded)
            {
                InputScheme.Player.Enable();
                InputScheme.Sokoban.Disable();
                //RevertSkyBox();
            }
        }
    }
}

