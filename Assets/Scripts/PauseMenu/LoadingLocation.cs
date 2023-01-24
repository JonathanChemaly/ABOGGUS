using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingLocation
{
    public static string SceneToLoad
    {
        get { return sceneName; }
        set { sceneName = value; }
    }
    private static string sceneName = "Assets/Scenes/TestLoader.unity";
}
