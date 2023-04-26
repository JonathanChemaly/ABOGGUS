
using ABOGGUS.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace ABOGGUS.Menus
{
    public class GameOverMenu : MonoBehaviour
    {
        public GameObject gameOverMenu;
        public Image gameOverImage;
        public static bool isPaused;
        private bool openMenu;
        private static float ogVolume;

        public const float fadeDuration = 2.0f;
        private const float alphaInc = fadeDuration == 0.0f ? 1.0f : 1.0f / fadeDuration;

        // Start is called before the first frame update
        void Start()
        {
            gameOverMenu.SetActive(false);
            Color clearBlack = Color.black;
            clearBlack.a = 0;
            gameOverImage.color = clearBlack;
            isPaused = false;
            openMenu = false;

            // bring this canvas to front
            transform.Find("GameOverCanvas").GetComponent<Canvas>().sortingOrder = 10;
        }

        // Update is called once per frame
        void Update()
        {
            if (isPaused && !openMenu)
            {
                FadeToBlack();
            }
            else if (openMenu)
            {
                OpenGameOverMenu();
            }
        }

        public static void ActivateGameOver()
        {
            Debug.Log("Player died, game over man");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isPaused = true;
            GameController.PauseGame();
            ogVolume = AudioListener.volume;
            AudioListener.volume = 0;
            BuffManager.Instance.RemoveBuffs();
            //GameController.player.debug = false;
        }

        private void OpenGameOverMenu()
        {
            gameOverMenu.SetActive(true);
        }

        private void FadeToBlack()
        {
            // fade to black
            gameOverImage.gameObject.SetActive(true);
            Color newColor = gameOverImage.color;
            //Debug.Log("Current color: " + newColor);
            float t = Time.deltaTime;
            if (newColor.a + alphaInc*t > 1)
            {
                newColor.a = 1;
                Invoke("Delay", 1.0f);
            }
            else newColor.a += alphaInc*t;
            gameOverImage.color = newColor;
        }

        private void Delay()
        {
            openMenu = true;
        }

        public void GoToLobby()
        {
            GameController.ResetPlayerHealth();
            RestoreVolume();
            GameController.Respawn();
        }

        public void GoToMainMenu()
        {
            GameController.ResetPlayerHealth();
            RestoreVolume();
            GameController.ChangeScene("Going to main menu from game over menu.", GameConstants.SCENE_MAINMENU, true);
        }

        public void QuitGame()
        {
            GameController.ResetPlayerHealth();
            RestoreVolume();
            GameController.QuitGame("Quit from game over menu.");
        }

        private void RestoreVolume()
        {
            AudioListener.volume = ogVolume;
        }
    }
}
