using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class GenerateSokoban : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Count of number of templates you want to place vertically")]
        private int gridHeight = 3;

        [SerializeField]
        [Tooltip("Count of number of templates you want to place horizontally")]
        private int gridWidth = 3;

        [SerializeField]
        [Tooltip("Size of Rooms to generate. Current valid numbers: 3")]
        private int roomSize = 3;

        [SerializeField]
        [Tooltip("Number of boxes to generate")]
        private int boxNumbers = 2;

        [HideInInspector]
        public SokobanCell[,] sokoban;

        private void Start()
        {
            NormalGeneration();
            //DebugSokoban();
        }

        private bool CheckIfHavePlayerSpawn()
        {
            foreach (SokobanCell cell in sokoban)
            {
                if (cell.GetType().Equals(typeof(PlayerSpawnCell))) return true;
            }

            return false;
        }

        private void NormalGeneration()
        {
            do
            {
                //Generate Walls
                sokoban = SokobanWallGen.GenerateSokoban(gridHeight, gridWidth, roomSize);
                SokobanHelper.DebugPrintSokoban(sokoban); //output after generation

                //Generate Goals
                sokoban = SokobanGoals.GenerateSokobanGoals(sokoban, boxNumbers);
                SokobanHelper.DebugPrintSokoban(sokoban); //output after goal generation
                                                          //Output everything

                SokobanStatics.generatedSokoban = sokoban;
            } while (!CheckIfHavePlayerSpawn());
        }
            
        private void DebugSokoban()
        {
            do
            {
                //Generate Walls
                sokoban = GenerateSokobanTestLevels.TwoByTwoTestLevel1WithGoal;
                SokobanHelper.DebugPrintSokoban(sokoban); //output after generation

                //Generate Goals
                sokoban = SokobanGoals.TestSearch(sokoban);
                SokobanHelper.DebugPrintSokoban(sokoban); //output after goal generation
                                                          //Output everything

                SokobanStatics.generatedSokoban = sokoban;
            } while (!CheckIfHavePlayerSpawn());
            //Test Level if neededd
            
        }
    }

}

