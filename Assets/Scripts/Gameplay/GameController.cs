using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.PlayerObjects;
using ABOGGUS.Menus;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

namespace ABOGGUS.Gameplay
{
    public class GameController : MonoBehaviour
    {

        public static PlayerObjects.Player player;

        public static GameController instance;

        public static GameConstants.GameState gameState = GameConstants.GameState.StartMenu;
        //Change to main menu
        public static string scene = GameConstants.SCENE_MAINLOBBY;

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
                case GameConstants.SCENE_MAINMENU: ChangeState(GameConstants.GameState.StartMenu); break;
                case GameConstants.SCENE_CREDITS: ChangeState(GameConstants.GameState.EndGame); break;
                default: 
                    if(GameConstants.SCENES_INGAME.Contains(newScene)) ChangeState(GameConstants.GameState.InGame);
                    break;
            }


            if (player != null)
            {
                while (!LoadingController.complete) ;

                GameObject physicalGameObject = GameObject.Find("Player");
                player.SetGameObject(physicalGameObject);
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
    }
}
