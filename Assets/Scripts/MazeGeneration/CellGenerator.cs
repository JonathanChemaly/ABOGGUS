using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.MazeGeneration
{
    public static class CellGenerator
    {
        //A cell of negative 1 means a border wall
        public static List<int> GenerateNSCells(int row, int column, int rowTotal, int columnTotal)
        {
            List<int> cells = new List<int>();
            int cell = columnTotal * row + column;

            //Get North Cell
            if (cell < rowTotal * columnTotal)
            {
                cells.Add(cell);
            }
            else
            {
                cells.Add(-1);
            }

            //Get South Cell
            cell = cell - columnTotal;
            if (cell >= 0)
            {
                cells.Add(cell);
            }
            else
            {
                cells.Add(-1);
            }

            return cells;
        }

        public static List<int> GenerateEWCells(int row, int column, int columnTotal)
        {
            List<int> cells = new List<int>();
            //Special Case
            if (row == 0 && column == 0) 
            {
                cells.Add(0);
                cells.Add(-1);
            }
            else
            {
                //Get West Cell
                if (column == columnTotal)
                {
                    cells.Add(-1);
                }
                else
                {
                    cells.Add(columnTotal * row + column);
                }

                //Get East Cell
                if (column == 0)
                {
                    cells.Add(-1);
                }
                else
                {
                    cells.Add(columnTotal * row + column - 1);
                }
            }

            return cells;
        }
    }
}
