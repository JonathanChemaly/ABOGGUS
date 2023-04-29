using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Menus;
using ABOGGUS.Gameplay;

namespace ABOGGUS.Input
{
    public class CreditsInputManager : MonoBehaviour
    {
        [SerializeField] private GameOptionsManager gameOptionsManager;
        private InputActions inputScheme;
        private string str;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            inputScheme = new InputActions();
            gameOptionsManager.Initialize(inputScheme.Player.Quit, inputScheme.Player.ReturnToMainMenu);
        }
    }
}