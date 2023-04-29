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

            // get current scene name
            scene = SceneManager.GetActiveScene().name;
            //Debug.Log("Current game scene: " + scene);

            // update game state
            UpdateStateFromScene(scene);
            //Debug.Log("Current state: " + gameState);

            // update player stuff (play states only)
            if (gameState == GameConstants.GameState.InGame) {
                StartCoroutine(UpdatePlayerNotNull());
            }

            SaveGameManager.LoadGameConstants();

            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            /*if (InventoryMenu.isPaused || PauseMenu.isPaused || GameOverMenu.isPaused || ThirdPersonCameraController.isPaused)
            {
                gameState = GameConstants.GameState.Paused;
            }

            if (gameState == GameConstants.GameState.EndGame)
            {

            }*/
        }

        public static void PauseGame()
        {
            gameState = GameConstants.GameState.Paused;
        }

        public static void ResumeGame()
        {
            gameState = GameConstants.GameState.InGame;
        }

        private void FixedUpdate()
        {
            if (GameConstants.SCENES_INGAME.Contains(scene) && !PauseMenu.isPaused && !InventoryMenu.isPaused && !GameOverMenu.isPaused)
            {
                player._FixedUpdate();
            }
        }

        public static void NewGame()
        {
            //Debug.Log("Starting new game");
            GameController.ChangeScene("Main menu to hotel lobby.", GameConstants.SCENE_MAINLOBBY, false);
        }

        public static void LoadGame(string sceneName)
        {
            loadingPlayer = true;
            GameController.ChangeScene("Main menu to loaded scene: " + sceneName, sceneName, false);
        }

        public static void Respawn()
        {
            GameController.ChangeScene("Player died go to hotel lobby", GameConstants.SCENE_MAINLOBBY, true);
        }

        public static void ResetPlayerHealth()
        {
            player.inventory.health = player.inventory.maxHealth;
        }

        public static void ChangeScene(string message, string newScene, bool loading)
        {
            Debug.Log(message);

            string oldScene = scene;

            if (loading)
            {
                LoadingLocation.SceneToLoad = newScene;
                scene = newScene;
                ThirdPersonCameraController.animationState = false;
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
                    SaveGameManager.finishedLoadingPlayer = false;
                    break;
                case GameConstants.SCENE_CREDITS:
                    ChangeState(GameConstants.GameState.EndGame);
                    SaveGameManager.SaveProgressToFile(null, player, oldScene);
                    SaveGameManager.finishedLoadingPlayer = false;
                    break;
                default:
                    if (GameConstants.SCENES_INGAME.Contains(newScene))
                    {
                        ChangeState(GameConstants.GameState.InGame);
                        SaveGameManager.SaveScene(newScene);    // Autosave: only save new scene if its in-game (play state)
                        SaveGameManager.SaveDataToFile(null);
                    }
                    break;
            }

            UpdatePlayer();
            loadingPlayer = false;
        }

        public static void UpdateStateFromScene(string scene)
        {
            switch (scene)
            {
                case GameConstants.SCENE_MAINMENU:
                    ChangeState(GameConstants.GameState.StartMenu);
                    break;
                case GameConstants.SCENE_CREDITS:
                    ChangeState(GameConstants.GameState.EndGame);
                    break;
                default:
                    if (GameConstants.SCENES_INGAME.Contains(scene))
                    {
                        ChangeState(GameConstants.GameState.InGame);
                    }
                    break;
            }
        }

        public static IEnumerator UpdatePlayerNotNull()
        {
            while (playerUpdater is null) yield return null;

            UpdatePlayer();
        }

        public static void UpdatePlayer()
        {
            if (gameState == GameConstants.GameState.InGame)
            {
                playerUpdater.UpdatePhysicalGameObjectForPlayer(scene, loadingPlayer);
            }
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

        public static bool PuzzleScene()
        {
            bool puzzleScene = false;
            if (scene == GameConstants.SCENE_WINTERROOM || scene == GameConstants.SCENE_SUMMERROOM || scene == GameConstants.SCENE_SPRINGROOM || scene == GameConstants.SCENE_AUTUMNROOM || scene == GameConstants.SCENE_MAINLOBBY)
            {
                puzzleScene = true;
            }

            return puzzleScene;
        }
    }
}
