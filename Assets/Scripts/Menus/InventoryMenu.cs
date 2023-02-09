using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using UnityEngine.UI;

namespace ABOGGUS.Menus
{
    public class InventoryMenu : MonoBehaviour
    {
        public GameObject inventoryMenu;

        public Sprite keyImage;
        public Image keyContainer;

        public static bool isPaused;

        // Start is called before the first frame update
        void Start()
        {
            inventoryMenu.SetActive(false);
            keyContainer.color = Color.white;
            keyContainer.sprite = keyImage;
            keyContainer.enabled = false;
            isPaused = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (isPaused) this.OpenInventory();
            else this.CloseInventory();
        }

        public static void Trigger()
        {
            if (!PauseMenu.isPaused)
            {
                isPaused = !isPaused;
            }
        }

        private void OpenInventory()
        {
            inventoryMenu.SetActive(true);
            if (Player.key)
            {
                keyContainer.enabled = true;
            } else
            {
                keyContainer.enabled = false;
            }
        }

        private void CloseInventory()
        {
            inventoryMenu.SetActive(false);
        }
    }
}