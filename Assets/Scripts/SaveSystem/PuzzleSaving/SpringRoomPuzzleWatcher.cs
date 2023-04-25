using ABOGGUS.Gameplay;
using ABOGGUS.Interact.Puzzles.RunePuzzle;
using ABOGGUS.Interact.Puzzles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.SaveSystem
{
    public class SpringRoomPuzzleWatcher : MonoBehaviour
    {
        //For Rune Puzzle;
        //Completion bool, List of bools for each active rune
        [SerializeField]
        [Tooltip("RunePuzzle Loader")]
        private TrackActiveRunes runePuzzle;

        [SerializeField]
        [Tooltip("TreePuzzle Loader")]
        private TreePuzzle treePuzzle;

        //For watering Puzzles
        //completion bool, A state number or enum, Last run

        private void OnDestroy()
        {
            List<bool> activeRunesList = new();
            runePuzzle.SaveFromList(activeRunesList);

            SaveGameManager.SaveSpringPuzzleStatus(activeRunesList, TreePuzzle.status, TreePuzzle.latestRun);
            //set dictionary value of puzzles to their status

            //Temp Save For Testing Purposes
            SaveGameManager.SaveDataToFile(null);
        }

        private void Awake()
        {
            //Temp loads For Testing Purposes
            SaveGameManager.LoadDataFromFile(null);


            //Grab values from dictionary puzzles to their status
            bool runeCompleteness = GameConstants.puzzleStatus["RunePuzzle"];
            //bool treeCompleteness = GameConstants.puzzleStatus["TreeGrowPuzzle"];

            SaveGameManager.LoadSpringPuzzleStatus(out List<bool> activeRunesList, out TreePuzzle.Status tStatus, out int currentRunNum);

            Debug.Log(tStatus);
            Debug.Log(currentRunNum);

            treePuzzle.LoadPuzzle(tStatus, currentRunNum);

            Debug.Log(TreePuzzle.status);
            Debug.Log(TreePuzzle.latestRun);

            string debugString = "";
            for (int i = 0; i < activeRunesList.Count; i++)
            {
                bool currentStatus = activeRunesList[i];
                debugString += currentStatus.ToString();
            }
            Debug.Log(debugString);

            runePuzzle.LoadFromList(activeRunesList, runeCompleteness);

            
        }
    }
}

