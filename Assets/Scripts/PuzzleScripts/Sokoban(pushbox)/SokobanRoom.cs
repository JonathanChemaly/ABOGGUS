using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class SokobanRoom
    {
        private SokobanCell[,] roomMatrix;

        private int arraySize;

        /*
         * ~~~~~~~~~~~~~~~~~~~~~~~Helper Methods~~~~~~~~~~~~~~~~~~~~~~~
         */

        /**
         * Transposes our room matrix
         */
        private void Transpose()
        {
            for (int row = 0; row < arraySize; row++)
            {
                for (int col = row; col < arraySize; col++)
                {
                    SokobanCell temp = roomMatrix[col, row];
                    roomMatrix[col, row] = roomMatrix[row, col];
                    roomMatrix[row, col] = temp;
                }
            }
        }

        /**
         * Reverses the columns in our room matrix
         */
        private void ReverseColumns()
        {
            //goes through all columns
            for (int col = 0; col < arraySize; col++)
            {
                //than for the rows starts incremeting the first index (0) and decrement the last index until they meet
                for (int startRow = 0, endRow = arraySize - 1; startRow < endRow; startRow++, endRow--)
                {
                    SokobanCell temp = roomMatrix[startRow, col];
                    roomMatrix[startRow, col] = roomMatrix[endRow, col];
                    roomMatrix[endRow, col] = temp;
                }
            }
        }

        private void ReverseRows()
        {
            //goes through all rows
            for (int row = 0; row < arraySize; row++)
            {
                //than for the cols starts incremeting the first index (0) and decrement the last index until they meet
                for (int startCol = 0, endCol = arraySize - 1; startCol < endCol; startCol++, endCol--)
                {
                    SokobanCell temp = roomMatrix[startCol, row];
                    roomMatrix[startCol, row] = roomMatrix[endCol, row];
                    roomMatrix[endCol, row] = temp;
                }
            }
        }

        /*
         * ~~~~~~~~~~~~~~~~~~~~~~~Public Methods~~~~~~~~~~~~~~~~~~~~~~~
         */

        public SokobanCell[,] RoomMatrix { get => roomMatrix; }

        public SokobanRoom(int roomSize)
        {
            switch (roomSize)
            {
                case 3:
                    {
                        //Gets all properties from our template class
                        PropertyInfo[] templateProperties = typeof(Sokoban3x3Templates).GetProperties();
                        //picks one of those properties at random and gets the value from it.
                        //this creates a new array for our room
                        roomMatrix = (SokobanCell[,])templateProperties[Random.Range(0, templateProperties.Length)].GetValue(null);
                        break;
                    }
                default:
                    {
                        //Debug.LogError("Sokoban Room size " + roomSize + " not avaiable");
                        break;
                    }
            }

            arraySize = roomSize + 2;
        }
        
        public void RotateRandomly()
        {
            //rotates room by 90 degrees 0, 1, 2 or 3 times
            int rotationAmount = Random.Range(0, 4);
            while(rotationAmount > 0)
            {
                //To rotate we take the transpose of the matrix and the reverse each column
                Transpose();
                ReverseColumns();
                rotationAmount--;
            }
        }

        public void ReflectRandomly()
        {
            //reflects room over rows (0), cols(1), rows and cols(3), or not at all
            int reflectionNum = Random.Range(0, 4);
            switch(reflectionNum)
            {
                case 0:
                    {
                        ReverseRows();
                        break;
                    }
                case 1:
                    {
                        ReverseColumns();
                        break;
                    }
                case 2:
                    {
                        ReverseColumns();
                        ReverseRows();
                        break;
                    }
                case 3:
                    {
                        break;
                    }
                default:
                    {
                        //Debug.LogError("Reflect Random int failed");
                        break;
                    }
            }
        }

    }
}
