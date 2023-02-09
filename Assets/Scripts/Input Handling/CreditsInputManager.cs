using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Menus;
using ABOGGUS.Gameplay;
using ABOGGUS.Input;

namespace ABOGGUS
{
    public class CreditsInputManager : MonoBehaviour
    {
        [SerializeField] private GameOptionsManager gameOptionsManager;
        private Input.InputActions inputScheme;
        private string str;

        private void Awake()
        {
            inputScheme = new Input.InputActions();
            gameOptionsManager.Initialize(inputScheme.Player.Quit, inputScheme.Player.ReturnToMainMenu);
        }
    }
}