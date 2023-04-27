using ABOGGUS.Gameplay;
using ABOGGUS.Interact.Puzzles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.MazeGeneration;

namespace ABOGGUS.SaveSystem
{
    public class AutumnRoomPuzzleWatcher : MonoBehaviour
    {
        //For tractor puzzle
        //Completion bool, A state idenitifier
        [SerializeField]
        [Tooltip("TractorPuzzle Loader")]
        private TractorMovement tractorPuzzle;

        //For maze puzzle
        //completion bool, A state number or enum, Last run
        [SerializeField]
        [Tooltip("MazePuzzle Loader")]
        private GameObject mazePuzzle;

        private void OnDestroy()
        {
            // save tractor

            // save maze

            SaveGameManager.SaveDataToFile(null);
        }

        private void Awake()
        {
            //Temp loads For Testing Purposes
            SaveGameManager.LoadDataFromFile(null);

            // load tractor

            // load maze

        }
    }
}

