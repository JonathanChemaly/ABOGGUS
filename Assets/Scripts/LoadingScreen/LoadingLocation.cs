using ABOGGUS.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingLocation
{
    /**
     * loads scene at the specficed path using the loading screen scene as a pause between the scenes.
     */
    public static void LoadSceneAtPath(string path)
    {
        SceneToLoad = path;
        SceneManager.LoadScene(GameConstants.SCENE_LOADING);
    }

    public static string SceneToLoad
    {
        get { return sceneName; }
        set { sceneName = value; }
    }
    private static string sceneName = "Assets/Scenes/MiniGame.unity";
}
