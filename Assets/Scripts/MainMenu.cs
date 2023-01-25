using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        StartCoroutine(PlayGameEnum());
    }

    IEnumerator PlayGameEnum()
    {
        yield return new WaitForSeconds(0.3f);
        LoadingLocation.LoadSceneAtPath("Assets/Scenes/MiniGame.unity");

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
