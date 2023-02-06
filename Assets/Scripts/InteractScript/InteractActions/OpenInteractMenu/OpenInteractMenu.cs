using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ABOGGUS.Input;
using ABOGGUS;
using ABOGGUS.Interact.Statics;

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

        // Start is called before the first frame update
        void Start()
        {
            item.InteractAction += OpenMenu;
        }

        private void OpenMenu()
        {
            SceneManager.LoadScene("Assets/Scenes/InteractableMenu.unity", LoadSceneMode.Additive);
            inputM.InputScheme.Player.Disable();
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
            
            //Checks if the interact menu determined that the user succeed a specfic action before they closed
            if (InteractStatics.interactActionSuccess)
            {
                item.doSuccesAction();
            }
            InteractStatics.interactActionSuccess = false;

            //when the menu is unloaded
            inputM.InputScheme.Player.Enable(); //re-enable player movement
            canvas.enabled = true; //re-enable player movement
        }
    }
}

