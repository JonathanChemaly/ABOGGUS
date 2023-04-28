using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ABOGGUS.Input;
using ABOGGUS;
using ABOGGUS.Interact.Statics;
using ABOGGUS.Gameplay;

namespace ABOGGUS.Interact
{
    public class OpenSokobanOnInteract : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Interactable Associated with this action")]
        private Interactable item;

        [SerializeField]
        [Tooltip("Animation associated with opening the interactMenu")]
        private PlayAnimation openingAnimation;

        [SerializeField]
        [Tooltip("Main input so we can disable player movement")]
        private InputManager inputM;

        [SerializeField]
        [Tooltip("List of Canvas of current Scene so we can close it")]
        private List<Canvas> canvasList;

        [SerializeField]
        [Tooltip("AudioSourceToPlay when interacted with")]
        private AudioSource StaticAudio;

        [SerializeField]
        [Tooltip("Sokoban Generator")]
        private ABOGGUS.Interact.Puzzles.Sokoban.GenerateSokoban sokobanGen;

        [SerializeField]
        [Tooltip("Camera To disable on open")]
        private ThirdPersonCameraController thirdPersonCamera;
        public static bool SokobanOpen = false;

        // Start is called before the first frame update
        void Start()
        {
            CheckHowToOpenMenu();
            item.InteractAction += playStatic;
        }

        private void playStatic()
        {
            StaticAudio.Play();
        }

        private void CheckHowToOpenMenu()
        {
            openingAnimation.AnimationFinishAction += OpenMenu;   
        }

        private void OpenMenu()
        {
            Puzzles.Sokoban.SokobanStatics.SokobanSolved = false; //Set the puzzle we are loading's solve state to false
            Puzzles.Sokoban.SokobanStatics.generatedSokoban = sokobanGen.sokoban; //set the level to load

            SokobanOpen = true;
            SceneManager.LoadScene("Assets/Scenes/Sokoban/SokobanPopUp.unity", LoadSceneMode.Additive);
            //Temp Change for input
            inputM.InputScheme.Player.Disable();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            thirdPersonCamera.enabled = false;
            //GameController.PauseGame();
            Debug.Log("After open menu Game state = " + GameController.gameState);
            //Temp Change for input end
            foreach (Canvas canvas in canvasList)
            {
                canvas.enabled = false;
            }
            
            StartCoroutine(CheckIfUnloaded());
            
        }

        /*
        * checks for when interactMenu is unloaded
        */
        IEnumerator CheckIfUnloaded()
        {
            //Get interactMenuScene
            Scene interactMenu = SceneManager.GetSceneByName("SokobanPopUp");

            //Temp Change for input end

            //Wait for menu to be loaded.
            while (!interactMenu.isLoaded)
            {
                yield return null;
            }

            //Keep spinning while the menu is loaded
            while (interactMenu.isLoaded)
            {
                yield return null;
            }
            
            //when the menu is unloaded
            //Temp Change for input
            //GameController.ResumeGame();
            thirdPersonCamera.enabled = true;
            inputM.InputScheme.Player.Enable(); //re-enable player movement
            foreach (Canvas canvas in canvasList)
            {
                canvas.enabled = true;
            }
            SokobanOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //Temp Change for input end

            //Do success if we solved the puzzle
            if (Puzzles.Sokoban.SokobanStatics.SokobanSolved)
            {
                item.DoSuccesAction();
                //reset the bool value
                Puzzles.Sokoban.SokobanStatics.SokobanSolved = false;
            }

            inputM.InputScheme.Sokoban.Disable(); //re-enable player movement
            StaticAudio.Stop(); //stop the static
        }
    }
}

