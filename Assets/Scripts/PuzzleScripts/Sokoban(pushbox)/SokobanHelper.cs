using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ABOGGUS.Interact.Puzzles.Sokoban.SokobanStructs;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public static class SokobanHelper
    {
        private static readonly int maxFails = 100;

        /*
         * ~~~~~~~~~~~~~~~~~~~~~~~Helper Methods~~~~~~~~~~~~~~~~~~~~~~~
         */

        public static bool IsOutOfSokobanBounds(int rowNumber, int colNumber, SokobanCell[,] sokoban)
        {
            int sokobanRows = sokoban.GetLength(0);
            int sokobanCols = sokoban.GetLength(1);

            return rowNumber < 0 || rowNumber >= sokobanRows || colNumber < 0 || colNumber >= sokobanCols;

        }

        // ************** Start: Placement Checking methods **************

        public static void DebugPrintSokoban(SokobanCell[,] toPrint)
        {
            int rowNum = toPrint.GetLength(0);
            int colNum = toPrint.GetLength(1);

            //Debug.Log("Printing Sokoban:");

            string sokobanWhole = "Printing Sokoban\n";

            //goes through all rows
            for (int row = 0; row < rowNum; row++)
            {
                string line = "Row " + row + ": ";
                //goes through all cols
                for (int col = 0; col < colNum; col++)
                {
                    SokobanCell curCell = toPrint[row, col];
                    if (curCell ==null)
                    {
                        line += "E";
                    } 
                    else
                    {
                        Type curCellType = curCell.GetType();
                        if (curCellType.Equals(typeof(FloorCell)))
                        {
                            line += "F";
                        }
                        else if (curCellType.Equals(typeof(WallCell)))
                        {
                            line += "W";
                        }
                        else if (curCellType.Equals(typeof(GoalCell)))
                        {
                            line += "G";
                        }
                        else if (curCellType.Equals(typeof(PlayerSpawnCell)))
                        {
                            line += "P";
                        }
                        else if (curCellType.Equals(typeof(BoxCell)))
                        {
                            line += "B";
                        }
                        else
                        {
                            line += "E";
                        }
                    }
                     
                }
                sokobanWhole += line + "\n";
            }
            Debug.Log(sokobanWhole);
        }

        public static SokobanCell[,] Clone(SokobanCell[,] toClone)
        {
            int numRows = toClone.GetLength(0);
            int numCols = toClone.GetLength(1);

            SokobanCell[,] ret = new SokobanCell[numRows, numCols];

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    SokobanCell sc = toClone[row, col];

                    System.Type cellType = sc.GetType();

                    SokobanCell clonedCell = (SokobanCell)System.Activator.CreateInstance(cellType);

                    clonedCell.PlayerIsHere = sc.PlayerIsHere;

                    ret[row, col] = clonedCell;
                }
            }

            return ret;
        }
    }
}
