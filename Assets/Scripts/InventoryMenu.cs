using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using UnityEngine.UI;

namespace ABOGGUS {
    public class InventoryMenu : MonoBehaviour
    {
        public Player player;
        public GameObject inventoryMenu;

        public Sprite keyImage;
        public Image keyContainer;

        public static bool isPaused;

        // Start is called before the first frame update
        void Start()
        {
            inventoryMenu.SetActive(false);
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
            if (player.key)
            {
                keyContainer.sprite = keyImage;
                keyContainer.color = Color.white;
            } else
            {
                keyContainer.sprite = null;
                keyContainer.color = new Color(167, 169, 173);
            }
            Time.timeScale = 0f;
        }

        private void CloseInventory()
        {
            inventoryMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}