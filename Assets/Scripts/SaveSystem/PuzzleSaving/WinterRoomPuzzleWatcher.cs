using ABOGGUS.Gameplay;
using ABOGGUS.Interact.Puzzles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.MazeGeneration;

namespace ABOGGUS.SaveSystem
{
    public class WinterRoomPuzzleWatcher : MonoBehaviour
    {
        //For melt ice puzzle
        //Completion bool,
        [SerializeField]
        [Tooltip("MeltIcePuzzle Loader")]
        private GameObject meltIcePuzzle;

        //For falling ice puzzle
        //completion bool,
        [SerializeField]
        [Tooltip("FallingIcePuzzle Loader")]
        private GameObject fallingIcePuzzle;

        private void OnDestroy()
        {
            // save melting ice


            // save falling ice


            SaveGameManager.SaveWinterPuzzleStatus();

            // temp for testing
            SaveGameManager.SaveDebug();
        }

        private void Awake()
        {
            //Temp loads For Testing Purposes
            SaveGameManager.LoadDebug();

            SaveGameManager.LoadWinterPuzzleStatus();

            // load melting ice

            // load falling ice


        }
    }
}

