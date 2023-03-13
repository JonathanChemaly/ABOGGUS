using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{   
    public class GenerateSokoban : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Count of number of templates you want to place vertically")]
        private int gridHeight = 2;

        [SerializeField]
        [Tooltip("Count of number of templates you want to place horizontally")]
        private int gridWidth = 2;

        [SerializeField]
        [Tooltip("Size of Rooms to generate. Current valid numbers: 3")]
        private int roomSize = 3;

        private SokobanCell[,] sokoban;

        private void Start()
        {
            //TO-DO
            //Generate Walls
            sokoban = SokobanHelper.GenerateSokoban(gridHeight, gridWidth, roomSize);
            //Generate Goals
            //Output everything
            SokobanHelper.DebugPrintSokoban(sokoban);

        }

    }
}

