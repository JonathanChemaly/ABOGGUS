using ABOGGUS.Gameplay;
using ABOGGUS.Interact.Puzzles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.SaveSystem
{
    public class LobbyRoomWatcher : MonoBehaviour
    {

        private void OnDestroy()
        {


            // temp for testing
            SaveGameManager.SaveDebug();
        }

        private void Awake()
        {
            //Temp loads For Testing Purposes
            StartCoroutine(WaitForPlayer());


        }

        IEnumerator WaitForPlayer()
        {
            while (GameController.player is null)
            {
                yield return null;
            }
            SaveGameManager.LoadDebug();
        }
    }
}

