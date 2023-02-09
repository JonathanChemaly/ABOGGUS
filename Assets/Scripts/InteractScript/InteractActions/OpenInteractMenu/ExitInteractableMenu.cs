using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ABOGGUS.Interact
{
    public class ExitInteractableMenu : MonoBehaviour
    {
        private Scene sceneToExit; //Scene we are unloading to exit

        private void Start()
        {
            sceneToExit = gameObject.scene; //gets scene this object is in.
        }

        public void exitMenu()
        {
            SceneManager.UnloadSceneAsync(sceneToExit);
        }
    }

}
