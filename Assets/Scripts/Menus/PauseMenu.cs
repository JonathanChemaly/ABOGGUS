using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using ABOGGUS.Gameplay;
using ABOGGUS.SaveSystem;

namespace ABOGGUS.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenu;
        public GameObject statsBox;
        public GameObject controlsBox;
        public static bool isPaused;

        // Start is called before the first frame update
        void Start()
        {
            pauseMenu.SetActive(false);
            statsBox.SetActive(false);
            controlsBox.SetActive(false);
            isPaused = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (isPaused) this.PauseGame();
            else this.ResumeGame();
        }

        public static void Trigger()
        {
            if (!InventoryMenu.isPaused && !GameOverMenu.isPaused)
            {
                isPaused = !isPaused;
            }
        }

        private void PauseGame()
        {
            pauseMenu.SetActive(true);
            GameController.PauseGame();
        }

        public void ResumeGame()
        {
            pauseMenu.SetActive(false);
            statsBox.SetActive(false);
            controlsBox.SetActive(false);
            GameController.ResumeGame();
        }

        public void GoToMainMenu()
        {
            GameController.ChangeScene("Going to main menu from pause menu.", GameConstants.SCENE_MAINMENU, true);
        }

        public void QuitGame()
        {
            SaveGameManager.SaveProgressToFile(null, GameController.player, GameController.scene);
            GameController.QuitGame("Quit from pause menu.");
        }

        public void SeeStats()
        {
            pauseMenu.SetActive(false);
            statsBox.SetActive(true);
        }

        public void SeeControls()
        {
            pauseMenu.SetActive(false);
            controlsBox.SetActive(true);
        }
    }
}
