using ABOGGUS.Gameplay;
using ABOGGUS.Interact.Puzzles.RunePuzzle;
using ABOGGUS.Interact.Puzzles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.SaveSystem
{
    public class LobbyPuzzleWatcher : MonoBehaviour
    {

        //For tree puzzle
        //completion bool, A state number or enum, Last run
        [SerializeField]
        [Tooltip("Game Objects for unlock Grimoire Puzzle")]
        private GameObject chest, comboLock, grimBook;

        private ABOGGUS.PlayerObjects.Player player = GameController.player; 

        private void OnDestroy()
        {
            //SaveGameManager.SaveLobbyPuzzleStatus(player);

            //temp save
            SaveGameManager.SaveDebug();
        }

        private void Awake()
        {
            //temp load
            SaveGameManager.LoadDebug();


            StartCoroutine(WaitToLoad());
           

        }

        IEnumerator WaitToLoad()
        {
            while (!SaveGameManager.finishedLoadingPlayer)
            {
                yield return null;
            }

            SaveGameManager.LoadLobbyPuzzleStatus(player, out bool grimAquired, out bool lockUnlocked);

            //Debug.Log("From Save we see GrimAquired = " + grimAquired + " and lockUnlocked = " + lockUnlocked);
            if (grimAquired)
            {
                grimBook.SetActive(false);
                chest.SetActive(false);
            }
            if (lockUnlocked)
            {
                comboLock.SetActive(false);
            }
        }
    }
}

