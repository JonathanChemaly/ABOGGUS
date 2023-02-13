using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingController : MonoBehaviour
{
    [SerializeField] private Image loadingIcon;//field that holds the image that tracks progress
    [SerializeField] private TextMeshProUGUI tipText;

    public static bool complete = true;

    public const float COMPLETION_AMOUNT = 0.8f;

    //Float to hold time to have loading screen on without skipping to the next sceen.
    [SerializeField] private float timeToHaveLoadingScreenOn; 

    //When loading scene is loaded call a corroutine to load scene in the background
    void Start()
    {
        tipText.text = GetTipMessage(); //sets tip up.
        loadingIcon.fillAmount = 0.0f; //sets fill of loading icon to zero to reset it.
       StartCoroutine(LoadNextScene());

    }
    /*
     * Method that loads scene (in the background) using LoadScyncAsync
     * and updates progress wheel so that user can see the progress of the loading.
     */
    IEnumerator LoadNextScene()
    {
        complete = false;

        AsyncOperation sceneLoadingOperation = SceneManager.LoadSceneAsync(LoadingLocation.SceneToLoad);

        sceneLoadingOperation.allowSceneActivation = false; //Stops scene activating

        
        //keep on going until we have finished loading
        while (!sceneLoadingOperation.isDone)
        {
            //makes the fill of the image equal to the amount of progress made toward the scene loading
            loadingIcon.fillAmount = sceneLoadingOperation.progress;

            //Because of inaccuracy of progress need to do this to enable scene activation again
            if (sceneLoadingOperation.progress >= COMPLETION_AMOUNT)
            {
                //waiting time so we have loading screen on a min amount of time.
                yield return new WaitForSeconds(timeToHaveLoadingScreenOn);
                complete = true;
                sceneLoadingOperation.allowSceneActivation = true; //now that scene is down we can all the scen to load
            }

            yield return null;
        }

        
    }

    /*
     * Gets a tip message for this loading to be displayed
     */
    private string GetTipMessage()
    {
        return "If you have trouble surviving, try not dying.";
    }
}
