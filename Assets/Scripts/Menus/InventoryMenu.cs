using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;

namespace ABOGGUS.Menus
{
    public class InventoryMenu : MonoBehaviour
    {
        public GameObject inventoryMenu;

        public Sprite keyImage;
        public Image keyContainer;

        public static bool isPaused;
        private bool updated = false;

        public const string FILE_PATH = "Assets/Resources/Images/InventoryIcons/";
        public const string FILE_TYPE = ".png";

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
            if (!PauseMenu.isPaused && !GameOverMenu.isPaused)
            {
                isPaused = !isPaused;
            }
        }

        private void OpenInventory()
        {
            inventoryMenu.SetActive(true);
            if (GameController.player.inventory.key)
            {
                keyContainer.enabled = true;
            } else
            {
                keyContainer.enabled = false;
            }
            GameController.PauseGame();
        }

        private void CloseInventory()
        {
            inventoryMenu.SetActive(false);
            GameController.ResumeGame();
        }

        private void UpdateInventory()
        {
            List<IItem> items = GameController.player.inventory.GetItems();
            for(int i = 0; i < items.Count; i++)
            {
                Sprite sprite = InventoryMenu.LoadNewSprite(FILE_PATH + items[i].GetName() + FILE_TYPE);
                containers[i].color = Color.white;
                containers[i].sprite = sprite;
                containers[i].enabled = true;
                hoverTips[i].enabled = true;
                hoverTips[i].setText(items[i].GetDescription());
            }

            updated = true;
        }

        //Sprite loading is taken from https://forum.unity.com/threads/generating-sprites-dynamically-from-png-or-jpeg-files-in-c.343735/
        public static Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f)
        {

            // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

            Sprite NewSprite;
            Texture2D SpriteTexture = LoadTexture(FilePath);
            NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);

            return NewSprite;
        }

        public static Texture2D LoadTexture(string FilePath)
        {

            // Load a PNG or JPG file from disk to a Texture2D
            // Returns null if load fails

            Texture2D Tex2D;
            byte[] FileData;

            if (File.Exists(FilePath))
            {
                FileData = File.ReadAllBytes(FilePath);
                Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
                if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                    return Tex2D;                 // If data = readable -> return texture
            }
            return null;                     // Return null if load failed
        }
    }
}