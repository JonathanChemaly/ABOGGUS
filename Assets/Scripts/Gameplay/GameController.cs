using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

using ABOGGUS.PlayerObjects;
using ABOGGUS.Menus;
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

        public static PlayerUpdater playerUpdater;

        private static bool loadingPlayer = false;

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

            if (gameState == GameConstants.GameState.EndGame)
            {

            }
        }

        private void FixedUpdate()
        {
            if (gameState == GameConstants.GameState.InGame)
            {
                player._FixedUpdate();
            }
        }

        public static void NewGame()
        {
            GameController.ChangeScene("Main menu to hotel lobby.", GameConstants.SCENE_MAINLOBBY, false);
        }

        public static void LoadGame(string sceneName)
        {
            loadingPlayer = true;
            GameController.ChangeScene("Main menu to loaded scene: " + sceneName, sceneName, false);
        }

        public static void Respawn()
        {
            player.inventory.health = player.inventory.maxHealth;
            GameController.ChangeScene("Player died go to hotel lobby", GameConstants.SCENE_MAINLOBBY, true);
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
                    ChangeState(GameConstants.GameState.StartMenu);
                    SaveGameManager.SaveProgressToFile(null, player, oldScene);
                    //SaveGameManager.SaveScene(oldScene);
                    break;
                case GameConstants.SCENE_CREDITS:
                    ChangeState(GameConstants.GameState.EndGame);
                    SaveGameManager.SaveProgressToFile(null, player, oldScene);
                    //SaveGameManager.SaveScene(oldScene);
                    break;
                default:
                    if (GameConstants.SCENES_INGAME.Contains(newScene))
                    {
                        ChangeState(GameConstants.GameState.InGame);
                        //SaveGameManager.SaveProgressToFile(null, player, newScene);
                        SaveGameManager.SaveScene(newScene);    // Autosave: only save new scene if its in-game (play state)
                        SaveGameManager.SaveDataToFile(null);
                    }
                    break;
            }

            if (gameState == GameConstants.GameState.InGame)
            {
                playerUpdater.UpdatePhysicalGameObjectForPlayer(scene, loadingPlayer);
            }
            loadingPlayer = false;

            //SaveGameManager.SaveDataToFile(null);
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
