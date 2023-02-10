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
        public static bool isPaused;

        // Start is called before the first frame update
        void Start()
        {
            pauseMenu.SetActive(false);
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
            if (!InventoryMenu.isPaused)
            {
                isPaused = !isPaused;
            }
        }

        private void PauseGame()
        {
            pauseMenu.SetActive(true);
        }

        public void ResumeGame()
        {
            pauseMenu.SetActive(false);
        }

        public void GoToMainMenu()
        {
            SaveProgress();
            GameController.ChangeScene("Going to main menu from pause menu.", GameConstants.SCENE_MAINMENU, true);
        }

        public void QuitGame()
        {
            SaveProgress();
            GameController.QuitGame("Quit from pause menu.");
        }

        private void SaveProgress()
        {
            SaveGameManager.SaveScene(GameController.scene);

        }
    }
}
