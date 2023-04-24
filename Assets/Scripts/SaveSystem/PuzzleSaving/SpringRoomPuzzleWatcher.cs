using ABOGGUS.Gameplay;
using ABOGGUS.Interact.Puzzles.RunePuzzle;
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
        [Tooltip("Interactables Associated with this class that need to watched for interaction")]
        private TrackActiveRunes runePuzzle;

        //For watering Puzzles
        //completion bool, A state number or enum, Last run

        private void OnDestroy()
        {
            List<bool> activeRunesList = new();
            
            runePuzzle.SaveFromList(activeRunesList);

            SaveGameManager.SaveSpringPuzzleStatus(activeRunesList);
            //set dictionary value of puzzles to their status

        }

        private void Awake()
        {
            //Grab values from dictionary puzzles to their status
            bool runeCompleteness = GameConstants.puzzleStatus["RunePuzzle"];
            bool treeCompleteness = GameConstants.puzzleStatus["TreeGrowPuzzle"];

            List<bool> activeRunesList = new();
            SaveGameManager.LoadSpringPuzzleStatus(activeRunesList);

            runePuzzle.LoadFromList(activeRunesList, runeCompleteness);


        }
    }
}

