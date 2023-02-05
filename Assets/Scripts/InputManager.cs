using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Input;

namespace ABOGGUS
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PauseManager pauseManager;
        [SerializeField] private CameraController cameraController;
        private InputActions inputScheme;

        public InputActions InputScheme { get => inputScheme; }

        private string str;

        private void Awake()
        {
            inputScheme = new InputActions();
            playerController.Initialize(inputScheme);
            pauseManager.Initialize(inputScheme.Player.Pause, inputScheme.Player.Inventory);
            cameraController.Initialize(inputScheme.Player.CameraSwitch);
        }
    }
}