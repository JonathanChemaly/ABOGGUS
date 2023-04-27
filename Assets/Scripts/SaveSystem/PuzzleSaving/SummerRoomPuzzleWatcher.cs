using ABOGGUS.Gameplay;
using ABOGGUS.Interact.Puzzles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.MazeGeneration;

namespace ABOGGUS.SaveSystem
{
    public class SummerRoomPuzzleWatcher : MonoBehaviour
    {
        //For tile slide puzzle
        //Completion bool, diffuclty level
        [SerializeField]
        [Tooltip("TileSlidePuzzle Loader")]
        private TilePuzzleManager tileSlidePuzzle;

        //For wind push puzzle
        //completion bool, 
        [SerializeField]
        [Tooltip("WindPushPuzzle Loader")]
        private GameObject windPushPuzzle;

        // For fire puzzle
        // completion bool,
        [SerializeField]
        [Tooltip("FirePuzzle Loader")]
        private GameObject firePuzzle;

        private void OnDestroy()
        {
            // save tile slide
            int difficulty = tileSlidePuzzle.size;
            // save wind push?

            // save fire?

            SaveGameManager.SaveSummerPuzzleStatus(difficulty);

            // temp for testing
            SaveGameManager.SaveDataToFile(null);
        }

        private void Awake()
        {
            //Temp loads For Testing Purposes
            SaveGameManager.LoadDataFromFile(null);

            SaveGameManager.LoadSummerPuzzleStatus(out int difficulty);

            // load tile slide
            tileSlidePuzzle.size = difficulty;
            tileSlidePuzzle.LoadPuzzle();

        }
    }
}

