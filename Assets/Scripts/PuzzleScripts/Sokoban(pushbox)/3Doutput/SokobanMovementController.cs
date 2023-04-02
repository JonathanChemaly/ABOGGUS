using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class SokobanMovementController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Where the generated Sokoban level is held")]
        private OutputSokoban3D outputedSokoban;

        private void UpdateGameObjectPos(GameObject objectBeingMoved, int row, int col)
        {
            objectBeingMoved.transform.localPosition =
                        new Vector3(row * OutputSokoban3D.cellScaleConst - outputedSokoban.xOffset,
                                    OutputSokoban3D.cellScaleConst / 2,
                                    col * OutputSokoban3D.cellScaleConst - outputedSokoban.yOffset);
        }

        private GameObject RemoveBoxAtPos(int row, int col)
        {
            for (int i = 0; i < curBoxPostions.Count; i++)
            {
                System.Tuple<int, int, GameObject> potentialTup = curBoxPostions[i];
                if (potentialTup.Item1 == row && potentialTup.Item2 == col)
                {
                    curBoxPostions.RemoveAt(i);
                    return potentialTup.Item3;
                }
            }

            //if we don't find it we return null
            return null;
        }

        private void DoGoalActions()
        {
            //Check if all our boxes are on goals
            if (CheckGoals())
            {
                SokobanStatics.SokobanSolved = true;
                //do goal actions
                Debug.Log("All Boxes On Goals! Performing Goal Action...");
            }
        }

        private bool CheckGoals()
        {
            foreach (System.Tuple<int, int, GameObject> boxTup in curBoxPostions)
            {
                bool boxMatchesWithGoal = false;
                foreach(System.Tuple <int, int> goalTup in outputedSokoban.goalLocations)
                {
                    if(boxTup.Item1 == goalTup.Item1 && boxTup.Item2 == goalTup.Item2)
                    {
                        boxMatchesWithGoal = true;
                        break;
                    }
                }
                
                //if any of the boxes is not on a matching goal we return here
                if (!boxMatchesWithGoal)
                {
                    return false;
                }
            }

            //all boxes have to be on goals to get here
            return true;
        }

        private void MoveHere(int rowOffset, int colOffset)
        {
            int rowBeingMovedTo = playerLocationRow + rowOffset;
            int colBeingMovedTo = playerLocationCol + colOffset;

            if (!SokobanHelper.IsOutOfSokobanBounds(rowBeingMovedTo, colBeingMovedTo, outputedSokoban.sokoban))
            {
                SokobanCell upCell = outputedSokoban.sokoban[rowBeingMovedTo, colBeingMovedTo];
                GameObject potentialBox = RemoveBoxAtPos(rowBeingMovedTo, colBeingMovedTo);

                //If the up cell is a box we check if we can move the box then move it if we can
                if (potentialBox != null)
                {
                    int rowBoxIsPushedTo = rowBeingMovedTo + rowOffset;
                    int colBoxIsPushedTo = colBeingMovedTo + colOffset;
                    //nested if to prevent hitting else if down below
                    if (!SokobanHelper.IsOutOfSokobanBounds(rowBoxIsPushedTo, colBoxIsPushedTo, outputedSokoban.sokoban))
                    {
                        SokobanCell cellNearBox = outputedSokoban.sokoban[rowBoxIsPushedTo, colBoxIsPushedTo];
                        GameObject boxBehindBox = RemoveBoxAtPos(rowBoxIsPushedTo, colBoxIsPushedTo);
                        //checks if the cell we are pushing the box into is a floor and not another box
                        if (cellNearBox.isFloor() && boxBehindBox == null)
                        {
                            //move box
                            //update our list to match new box postion
                            UpdateGameObjectPos(potentialBox, rowBoxIsPushedTo, colBoxIsPushedTo);

                            curBoxPostions.Add(new System.Tuple<int, int, GameObject>(rowBoxIsPushedTo, colBoxIsPushedTo, potentialBox));
                            //move player as well
                            //update our int postion to match movement
                            playerLocationRow += rowOffset;
                            playerLocationCol += colOffset;

                            //udate game object postion to match movement
                            UpdateGameObjectPos(outputedSokoban.playerObject, playerLocationRow, playerLocationCol);

                            //check if all boxes are on goal
                            DoGoalActions();
                        }
                        else //add back if we can't push it
                        {
                            curBoxPostions.Add(new System.Tuple<int, int, GameObject>(rowBeingMovedTo, colBeingMovedTo, potentialBox));
                            if(boxBehindBox != null)
                            {
                                curBoxPostions.Add(new System.Tuple<int, int, GameObject>(rowBoxIsPushedTo, colBoxIsPushedTo, boxBehindBox));
                            }
                        }
                    }
                }
                else if (upCell.isFloor()) //if the adjacentcell is a floor we move into that cell;
                {
                    //update our int postion to match movement
                    playerLocationRow += rowOffset;
                    playerLocationCol += colOffset;

                    //udate game object postion to match movement
                    UpdateGameObjectPos(outputedSokoban.playerObject, playerLocationRow, playerLocationCol);
                }
            }
        }

        

        private void MoveUp(InputAction.CallbackContext obj)
        {
            Debug.Log("Moving Sokoban Player Up");
            //decreasing row by one to move up
            MoveHere(-1, 0);
        }

        private void MoveDown(InputAction.CallbackContext obj)
        {
            Debug.Log("Moving Sokoban Player Down");
            //increasing row by one to move down
            MoveHere(1, 0);
        }

        private void MoveLeft(InputAction.CallbackContext obj)
        {
            Debug.Log("Moving Sokoban Player Left");
            //decreasing col by one to move left
            MoveHere(0, -1);
        }

        private void MoveRight(InputAction.CallbackContext obj)
        {
            Debug.Log("Moving Sokoban Player Right");
            //increasing col by one to move right
            MoveHere(0, 1);
        }

        public void InitializeInput(Input.InputActions inputScheme)
        {
            inputScheme.Sokoban.Down.performed += MoveDown;
            inputScheme.Sokoban.Down.Enable();

            inputScheme.Sokoban.Up.performed += MoveUp;
            inputScheme.Sokoban.Up.Enable();

            inputScheme.Sokoban.Left.performed += MoveLeft;
            inputScheme.Sokoban.Left.Enable();

            inputScheme.Sokoban.Right.performed += MoveRight;
            inputScheme.Sokoban.Right.Enable();
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(getStartingPostions());
        }

        private int playerLocationRow, playerLocationCol;

        private List<System.Tuple<int, int, GameObject>> curBoxPostions = new(); 

        IEnumerator getStartingPostions()
        {
            while(outputedSokoban.playerObject == null)
            {
                yield return null;
            }

            //setup our variables for our object;

            playerLocationRow = outputedSokoban.startingPlayerPostion.Item1;
            playerLocationCol = outputedSokoban.startingPlayerPostion.Item2;

            //add Blocks for checking
            foreach (System.Tuple<int, int, GameObject> tuple in outputedSokoban.startingBoxLocations)
            {
                curBoxPostions.Add(new System.Tuple<int, int, GameObject>(tuple.Item1, tuple.Item2, tuple.Item3));
            }
        }
    }
}
