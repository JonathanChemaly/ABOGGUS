using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Menus;
using ABOGGUS.Gameplay;
using ABOGGUS.PlayerObjects;
using ABOGGUS.Input;

namespace ABOGGUS
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PauseManager pauseManager;
        [SerializeField] private CameraController cameraController;
        private Input.InputActions inputScheme;
        private string str;

        private void Awake()
        {
            inputScheme = new Input.InputActions();
            playerController.Initialize(inputScheme);
            pauseManager.Initialize(inputScheme.Player.Pause, inputScheme.Player.Inventory);
            cameraController.Initialize(inputScheme.Player.CameraSwitch);
        }
    }
}