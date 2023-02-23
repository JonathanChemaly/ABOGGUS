using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

using ABOGGUS.Gameplay;
using ABOGGUS.SaveSystem;

namespace ABOGGUS.Menus
{
    public class MainMenu : MonoBehaviour
    {
        //[SerializeField] private GameObject loadButton;
        private float initialVolume = 0.25f;
        private void Start()
        {
            AudioListener.volume = initialVolume;

            // disable load button if no save folder found
            if (!Directory.Exists(Application.dataPath + SaveGameManager.saveFolder))
            {
                Button loadButton = this.gameObject.transform.Find("LoadGameButton").GetComponent<Button>();
                loadButton.interactable = false;
            }
        }
        public void PlayNewGame()
        {
            StartCoroutine(PlayNewGameEnum());
        }
        public void PlayLoadGame()
        {
            StartCoroutine(PlayLoadGameEnum());
        }
        public void PlayCredits()
        {
            StartCoroutine(PlayCreditsEnum());
        }
        IEnumerator PlayNewGameEnum()
        {
            SaveGameManager.StartNewData();
            yield return new WaitForSeconds(0.3f);
            GameController.NewGame();
        }

        IEnumerator PlayLoadGameEnum()
        {
            SaveGameManager.LoadProgressFromFile(null);
            string sceneName = SaveGameManager.currentSaveData.lastScene;
            yield return new WaitForSeconds(0.3f);
            GameController.LoadGame(sceneName);
        }

        IEnumerator PlayCreditsEnum()
        {
            yield return new WaitForSeconds(0.3f);
            GameController.ChangeScene("Main menu to credits.", GameConstants.SCENE_CREDITS, true);
        }

        public void QuitGame()
        {
            Debug.Log("Quit game");
#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}