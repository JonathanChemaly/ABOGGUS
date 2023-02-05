using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ABOGGUS.Input;
using ABOGGUS;

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

        // Start is called before the first frame update
        void Start()
        {
            item.InteractAction += OpenMenu;
        }

        private void OpenMenu()
        {
            SceneManager.LoadScene("Assets/Scenes/InteractableMenu.unity", LoadSceneMode.Additive);
            inputM.InputScheme.Player.Disable();

        }
    }
}

