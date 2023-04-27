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
        private TractorLoader tractorPuzzle;

        //For maze puzzle
        //completion bool, A state number or enum, Last run
        [SerializeField]
        [Tooltip("MazePuzzle Loader")]
        private GameObject mazePuzzle;

        private void OnDestroy()
        {
            // save tractor
            int state = tractorPuzzle.SavePuzzle();

            // save maze


            SaveGameManager.SaveAutumnPuzzleStatus(state);
            // temp for testing
            SaveGameManager.SaveDataToFile(null);
        }

        private void Awake()
        {
            //Temp loads For Testing Purposes
            SaveGameManager.LoadDataFromFile(null);
            while (GameController.player is null) ;
            SaveGameManager.LoadPlayerProgress(GameController.player);

            SaveGameManager.LoadAutumnPuzzleStatus(out int state);

            // load tractor
            tractorPuzzle.LoadPuzzle(state);

            // load maze
            // ???


        }
    }
}

