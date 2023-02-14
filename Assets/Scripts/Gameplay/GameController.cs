using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.PlayerObjects;
using ABOGGUS.Menus;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using ABOGGUS.SaveSystem;

namespace ABOGGUS.Gameplay
{
    public class GameController : MonoBehaviour
    {

        public static PlayerObjects.Player player;

        public static GameController instance;

        public static GameConstants.GameState gameState = GameConstants.GameState.StartMenu;

        //For running the elevator scene change back to mainmenu 
        public static string scene = GameConstants.SCENE_MAINMENU;

        // Start is called before the first frame update
        void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (InventoryMenu.isPaused || PauseMenu.isPaused || ThirdPersonCameraController.isPaused)
            {
                gameState = GameConstants.GameState.Paused;
            }
        }

        private void FixedUpdate()
        {
            if(gameState == GameConstants.GameState.InGame)
            {
                player._FixedUpdate();
            }
        }

        public static void ChangeScene(string message, string newScene, bool loading)
        {
            Debug.Log(message);

            string oldScene = scene;

            if (loading)
            {
                LoadingLocation.SceneToLoad = newScene; 
                scene = newScene;
                if (scene.Equals(GameConstants.SCENE_BOSS))
                    PlayerController.speed = 0.1f;
                else
                    PlayerController.speed = PlayerConstants.SPEED_DEFAULT;
                SceneManager.LoadScene(sceneName: GameConstants.SCENE_LOADING);
            }
            else
            {
                scene = newScene;
                SceneManager.LoadScene(sceneName: newScene);
            }

            switch (scene)
            {
                case GameConstants.SCENE_MAINMENU:
                    SaveGameManager.SaveScene(oldScene);
                    ChangeState(GameConstants.GameState.StartMenu);
                    break;
                case GameConstants.SCENE_CREDITS:
                    SaveGameManager.SaveScene(oldScene);
                    ChangeState(GameConstants.GameState.EndGame); 
                    break;
                default:
                    if (GameConstants.SCENES_INGAME.Contains(newScene))
                    {
                        SaveGameManager.SaveScene(newScene);    // Autosave: only save new scene if its in game
                        ChangeState(GameConstants.GameState.InGame);
                    }
                    break;
            }


            if (player != null)
            {
                while (!LoadingController.complete) ;

                GameObject physicalGameObject = GameObject.Find("Player");
                player.SetGameObject(physicalGameObject);

                SaveGameManager.SavePlayerProgress(player);
            }

            SaveGameManager.SaveDataToFile(null);
        }

        public static void ChangeState(GameConstants.GameState gameState)
        {
            GameController.gameState = gameState;
        }
        public static void QuitGame(string message)
        {
            Debug.Log(message);
            QuitGame();
        }

        private static void QuitGame()
        {

#if UNITY_STANDALONE
            Application.Quit();
#endif

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
