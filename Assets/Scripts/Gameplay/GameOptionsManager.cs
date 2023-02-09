using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ABOGGUS.Input;
using ABOGGUS;


namespace ABOGGUS.Gameplay
{
    public class GameOptionsManager : MonoBehaviour
    {
        private InputAction quitAction;
        private InputAction returnAction;
        public void Initialize(InputAction quitAction, InputAction returnAction)
        {
            this.quitAction = quitAction;
            this.returnAction = returnAction;

            this.quitAction.performed += TriggerQuit;
            this.quitAction.Enable();

            this.returnAction.performed += TriggerReturnToMainMenu;
            this.returnAction.Enable();
        }
        private void TriggerQuit(InputAction.CallbackContext obj)
        {
            GameController.QuitGame("Pressed quit key.");
        }
        private void TriggerReturnToMainMenu(InputAction.CallbackContext obj)
        {
            GameController.ChangeScene("Pressed return to main menu key.", GameConstants.SCENE_MAINMENU);
        }
        private void OnDisable()
        {
            quitAction.Disable();
            returnAction.Disable();
        }
    }
}
