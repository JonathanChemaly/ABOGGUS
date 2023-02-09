using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Input;

using ABOGGUS.Interact;

using ABOGGUS.PlayerObjects;


namespace ABOGGUS
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PauseManager pauseManager;
        [SerializeField] private CameraController cameraController;

        [SerializeField] private InteractionManager interactionManager;

        


        [SerializeField] private ThirdPersonCameraController thirdPersonCameraController;
        [SerializeField] private RotateControl rotateController;
        private Input.InputActions inputScheme;

        public InputActions InputScheme { get => inputScheme; }

        private string str;

        private void Awake()
        {
            inputScheme = new Input.InputActions();
            playerController.Initialize(inputScheme);
            pauseManager.Initialize(inputScheme.Player.Pause, inputScheme.Player.Inventory);
            cameraController.Initialize(inputScheme.Player.CameraSwitch);

            interactionManager.Initialize(inputScheme.Player.Interact);

            thirdPersonCameraController.Initialize(inputScheme.Player.Look);
            rotateController.Initialize(inputScheme.Player.Rotate);
            Cursor.lockState = CursorLockMode.Locked;

        }

        /*Not needed anymore I think- Josh
        void OnEnable()
        {
            var _ = new QuitHandler(inputScheme.Player.Quit);
        }
        */
    }
}