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
    public class OpenShopMenu : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Interactable Associated with this action")]
        private Interactable item;

        [SerializeField]
        [Tooltip("Main input so we can disable player movement")]
        private InputManager inputM;

        [SerializeField]
        [Tooltip("List of Canvas of current Scene so we can close it")]
        private List<Canvas> canvasList;

        [SerializeField]
        [Tooltip("Camera To disable on open")]
        private ThirdPersonCameraController thirdPersonCamera;
        public static bool shopOpen = false;

        // Start is called before the first frame update
        void Start()
        {
            item.InteractAction += OpenMenu;
        }

        private void OpenMenu()
        {
            shopOpen = true;
            SceneManager.LoadScene("Assets/Scenes/Shop/ShopMenu.unity", LoadSceneMode.Additive);
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
            Scene interactMenu = SceneManager.GetSceneByName("ShopMenu");

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
            shopOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //Temp Change for input end

        }
    }
}

