using ABOGGUS.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ABOGGUS.Menus
{
    public class MainMenu : MonoBehaviour
    {
        private float initialVolume = 0.25f;
        private void Start()
        {
            AudioListener.volume = initialVolume;
        }
        public void PlayGame()
        {
            StartCoroutine(PlayGameEnum());
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

        public void volumeSlider()
        {

        }
    }
}