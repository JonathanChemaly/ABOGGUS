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
    public class OpenInteractMenu : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Interactable Associated with this action")]
        private Interactable item;

        [SerializeField]
        [Tooltip("Main input so we can disable player movement")]
        private InputManager inputM;

        [SerializeField]
        [Tooltip("Canvas of current Scene so we can close it")]
        private Canvas canvas;

        [SerializeField]
        [Tooltip("Path of the item to loadInTheUI")]
        private string itemToLoadInUI;

        [SerializeField]
        [Tooltip("Postion of the item to loadInTheUI")]
        private ThirdPersonCameraController camera;

        [SerializeField]
        [Tooltip("Postion of the item to loadInTheUI")]
        private Vector3 posToLoadInUI = Vector3.zero;

        [SerializeField]
        [Tooltip("Path of the item to loadInTheUI")]
        private Vector3 scaleToLoadInUI = Vector3.one;
        // Start is called before the first frame update
        void Start()
        {
            item.InteractAction += OpenMenu;
        }

        private void OpenMenu()
        {
            InteractStatics.pathToLoad = itemToLoadInUI; //set static so we can load the prefabs properly
            InteractStatics.posToLoadAt = posToLoadInUI;
            InteractStatics.scaleToLoadAt = scaleToLoadInUI;
            SceneManager.LoadScene("Assets/Scenes/InteractableMenu.unity", LoadSceneMode.Additive);
            //Temp Change for input
            inputM.InputScheme.Player.Disable();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            camera.enabled = false;
            //GameController.PauseGame();
            Debug.Log("After open menu Game state = " + GameController.gameState);
            //Temp Change for input end
            canvas.enabled = false;
            StartCoroutine(CheckIfUnloaded());
            
        }

        /*
        * checks for when interactMenu is unloaded
        */
        IEnumerator CheckIfUnloaded()
        {
            //Get interactMenuScene
            Scene interactMenu = SceneManager.GetSceneByName("InteractableMenu");

            //Temp Change for input end

            //Wait for menu to be loaded.
            while (!interactMenu.isLoaded)
            {
                yield return null;
            }

            //Keep spinning while the menu is loaded
            while (interactMenu.isLoaded)
            {
                Debug.Log("While menu open Game state = " + GameController.gameState);
                Debug.Log(Cursor.lockState);
                Debug.Log(Cursor.visible);
                yield return null;
            }
            
            //Checks if the interact menu determined that the user succeed a specfic action before they closed
            if (InteractStatics.interactActionSuccess)
            {
                item.DoSuccesAction();
            }
            InteractStatics.interactActionSuccess = false;



            //when the menu is unloaded
            //Temp Change for input
            //GameController.ResumeGame();
            camera.enabled = true;
            inputM.InputScheme.Player.Enable(); //re-enable player movement
            canvas.enabled = true; //re-enable player movement
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //Temp Change for input end

        }
    }
}

