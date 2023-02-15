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
        public void PlayGame()
        {
            StartCoroutine(PlayGameEnum());
        }
        public void LoadGame()
        {
            StartCoroutine(LoadGameEnum());
        }
        public void PlayCredits()
        {
            StartCoroutine(PlayCreditsEnum());
        }
        IEnumerator PlayGameEnum()
        {
            yield return new WaitForSeconds(0.3f);
            GameController.ChangeScene("Main menu to hotel lobby.", GameConstants.SCENE_MAINLOBBY, true);
        }

        IEnumerator LoadGameEnum()
        {
            SaveGameManager.LoadProgressFromFile(null);
            string sceneName = SaveGameManager.currentSaveData.lastScene;
            yield return new WaitForSeconds(0.3f);
            GameController.ChangeScene("Main menu to last saved scene: " + sceneName, sceneName, true);
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