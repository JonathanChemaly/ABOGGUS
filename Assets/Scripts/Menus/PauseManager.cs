using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ABOGGUS.Input;
using ABOGGUS;

namespace ABOGGUS.Menus
{
    public class PauseManager : MonoBehaviour
    {
        private InputAction pauseAction;
        private InputAction inventoryAction;
        public void Initialize(InputAction pauseAction, InputAction inventoryAction)
        {
            this.pauseAction = pauseAction;
            this.inventoryAction = inventoryAction;

            this.pauseAction.performed += TriggerPause;
            this.pauseAction.Enable();

            this.inventoryAction.performed += TriggerInventory;
            this.inventoryAction.Enable();
        }
        private void TriggerPause(InputAction.CallbackContext obj)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PauseMenu.Trigger();
        }
        private void TriggerInventory(InputAction.CallbackContext obj)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            InventoryMenu.Trigger();
        }

        private void OnDisable()
        {
            pauseAction.Disable();
            inventoryAction.Disable();
        }
    }
}