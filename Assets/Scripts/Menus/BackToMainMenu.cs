using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReturnToMenu());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                     Application.Quit();
            #endif
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(sceneName: "Scenes/Menu");
        }

    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(25.0f);
        SceneManager.LoadScene(sceneName: "Scenes/Menu");
    }
}