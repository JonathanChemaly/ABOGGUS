using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace ABOGGUS.Gameplay
{
    public class CustomSceneManager : MonoBehaviour
    {
        public delegate void SceneChange(string sceneName);

        public static event SceneChange LoadScene;
        public static event SceneChange UnloadScene;
        /*

        private void Awake()
        {
            if (singleton && singleton != this)
            {
                Destroy(gameObject);
            }

            singleton = this;
            DontDestroyOnLoad(gameObject);
        }

        private static IEnumerator LoadLevel(string sceneName)
        {
            var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            LoadScene?.Invoke(newSceneName);
        }

        public static void OnLoadScene(string newSceneName)
        {
            if (!singleton)
            {
                singleton = new GameObject("CustomNetworkManager").AddComponent<CustomNetworkManager>();
            }

            OnUnloadScene(newSceneName);
            singleton.StartCoroutine(LoadLevel(newSceneName));
        }*/
    }
}
