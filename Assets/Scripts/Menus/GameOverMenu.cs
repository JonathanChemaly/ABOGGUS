using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using ABOGGUS.Gameplay;
using ABOGGUS.SaveSystem;
using System;

namespace ABOGGUS.Menus
{
    public class GameOverMenu : MonoBehaviour
    {
        public GameObject gameOverMenu;
        public static bool playerDied;

        // Start is called before the first frame update
        void Start()
        {
            gameOverMenu.SetActive(false);
            playerDied = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (playerDied) gameOverMenu.SetActive(true);
            //else this.ResumeGame();
        }

        public static void ActivateGameOver()
        {
            playerDied = true;
        }

        public void GoToLobby()
        {
            GameController.Respawn();
        }

        public void GoToMainMenu()
        {
            GameController.ChangeScene("Going to main menu from game over menu.", GameConstants.SCENE_MAINMENU, true);
        }

        public void QuitGame()
        {
            GameController.QuitGame("Quit from game over menu.");
        }
    }
}
