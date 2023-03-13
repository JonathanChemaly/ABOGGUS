using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ABOGGUS.Interact.Puzzles.Sokoban.SokobanStructs;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public static class SokobanHelper
    {
        private static int maxFails = 100;

        /*
         * ~~~~~~~~~~~~~~~~~~~~~~~Helper Methods~~~~~~~~~~~~~~~~~~~~~~~
         */

        private static List<Tuple<int, int>> GetRegionList(int rowNum, int colNum, int roomSize) 
        {
            List<Tuple<int, int>> reigions = new List<Tuple<int, int>>();

            //goes through all rows
            for (int row = 0; row < rowNum; row++)
            {
                //goes through all cols
                for (int col = 0; col < colNum; col++)
                {
                    reigions.Add(new Tuple<int, int>(row * roomSize, col * roomSize));
                }
            }

            return reigions;
        }

        private static bool IsOutOfSokobanBounds(int rowNumber, int colNumber, SokobanCell[,] sokoban)
        {
            int sokobanRows = sokoban.GetLength(0);
            int sokobanCols = sokoban.GetLength(1);

            return rowNumber < 0 || rowNumber >= sokobanRows || colNumber < 0 || colNumber >= sokobanCols;

        }

        // ************** Start: Placement Checking methods **************



        //TO-DO Fix
        private static bool CanAddRowCheck(int rowToCheck, Tuple<int, int> reigion, int roomColCount, SokobanCell[,] roomArray, SokobanCell[,] sokoban)
        {
            bool canAdd = true;

            
            for (int col = 0; col < roomColCount && canAdd; col++)
            {
                SokobanCell roomCell = roomArray[rowToCheck, col];

                //if our cell to compare in the sokoban is not in bounds 
                if (IsOutOfSokobanBounds(rowToCheck + reigion.Item1 - 1, col + reigion.Item2 - 1 ,sokoban))
                {
                    //we can only add the room when that cell is empty.
                    canAdd = typeof(EmptyCell).Equals(roomCell.GetType());
                }
                //our cell is in bounds, we only need to check it if we have a empty cell.
                else if (!typeof(EmptyCell).Equals(roomCell.GetType()))
                {
                    SokobanCell cellInSokoban = sokoban[rowToCheck + reigion.Item1 - 1, col + reigion.Item2 - 1];
                    if (cellInSokoban == null)
                    {
                        canAdd = false;
                    } else
                    {
                        canAdd = cellInSokoban.GetType().Equals(roomCell.GetType());
                        Debug.Log(cellInSokoban.GetType() + " check succeeded with " + roomCell);
                    }
                     
                }

            }
            return canAdd;
        }

        //TO-DO Fix
        private static bool CanAddColCheck(int colToCheck, Tuple<int, int> reigion, int roomColCount, SokobanCell[,] roomArray, SokobanCell[,] sokoban)
        {
            bool canAdd = true;
            for (int row = 1; row < roomColCount - 1 && canAdd; row++)
            {
                SokobanCell roomCell = roomArray[row, colToCheck];

                //if our cell to compare in the sokoban is not in bounds 
                if (IsOutOfSokobanBounds(row + reigion.Item1 - 1, reigion.Item2 + colToCheck - 1, sokoban))
                {
                    //we can only add the room when that cell is empty.
                    canAdd = typeof(EmptyCell).Equals(roomCell.GetType());
                }
                //our cell is in bounds, we only need to check it if we have a empty cell.
                else if (!typeof(EmptyCell).Equals(roomCell.GetType()))
                {
                    SokobanCell cellInSokoban = sokoban[row + reigion.Item1 - 1, reigion.Item2 + colToCheck - 1];
                    if (cellInSokoban == null)
                    {
                        canAdd = false;
                    }
                    else
                    {
                        canAdd = cellInSokoban.GetType().Equals(roomCell.GetType());
                        Debug.Log(cellInSokoban.GetType()+ " check succeeded with " + roomCell);
                    }
                }

            }
            return canAdd;
        }

        private static bool CheckRequiredCells(SokobanCell[,] sokoban, SokobanRoom roomToAdd, Tuple<int, int> reigion) 
        {
            bool canAdd = true;

            SokobanCell[,] roomArray = roomToAdd.RoomMatrix;

            int roomRowCount = roomArray.GetLength(0);
            int roomColCount = roomArray.GetLength(1);

            //go through row 0 which is a check row.
            canAdd = canAdd & CanAddRowCheck(0, reigion, roomColCount, roomArray, sokoban);
            //go through row N-1 which is also a check row
            canAdd = canAdd & CanAddRowCheck(roomRowCount - 1, reigion, roomColCount, roomArray, sokoban);

            //go through cols similarly skipping already checked row values
            canAdd = canAdd & CanAddColCheck(0, reigion, roomColCount, roomArray, sokoban);
            canAdd = canAdd & CanAddColCheck(roomColCount - 1, reigion, roomColCount, roomArray, sokoban);


            return canAdd;
        }

        // ************** End: Placement Checking methods **************

        // ************** Start: Placement methods **************


        private static int AttemptFloorConnect(SokobanCell cellToConenct, SokobanCell[,] sokoban, int row, int col)
        {
            int connected = 0;
            if (!IsOutOfSokobanBounds(row, col, sokoban) && sokoban[row , col] != null && sokoban[row , col].isFloor() &&
                !cellToConenct.AdjacentList.Contains(sokoban[row , col]))
            {
                connected++; //increment Number of floors added
                cellToConenct.AdjacentList.Add(sokoban[row , col]); //add adjacent floor cell to our cell
                sokoban[row , col].AdjacentList.Add(cellToConenct); //add our cell to adjacent cell
            }
            return connected;
        }

        private static int ConnectFloors(SokobanCell cellToConenct, SokobanCell[,] sokoban, int row, int col)
        {
            int numberOfFloorsAdded = 0;

            //for each direction check if that direction is in bounds, if the cell in that direction is a floor cell,
            //and if our cell we are adding doesn't already contain that cell

            //north
            if (row - 1 > 0 && sokoban[row - 1, col] != null && sokoban[row - 1, col].isFloor() && !cellToConenct.AdjacentList.Contains(sokoban[row - 1, col])) 
            {
                numberOfFloorsAdded++; //increment Number of floors added
                cellToConenct.AdjacentList.Add(sokoban[row - 1, col]); //add adjacent floor cell to our cell
                sokoban[row - 1, col].AdjacentList.Add(cellToConenct); //add our cell to adjacent cell
            }
            //south
            if (row + 1 < sokoban.GetLength(0) && sokoban[row + 1, col] != null && sokoban[row + 1, col].isFloor() && !cellToConenct.AdjacentList.Contains(sokoban[row + 1, col]))
            {
                numberOfFloorsAdded++; //increment Number of floors added
                cellToConenct.AdjacentList.Add(sokoban[row + 1, col]); //add adjacent floor cell to our cell
                sokoban[row + 1, col].AdjacentList.Add(cellToConenct); //add our cell to adjacent cell
            }
            //east
            if (col + 1 < sokoban.GetLength(1) && sokoban[row, col + 1] != null && sokoban[row, col + 1].isFloor() && !cellToConenct.AdjacentList.Contains(sokoban[row, col + 1]))
            {
                numberOfFloorsAdded++; //increment Number of floors added
                cellToConenct.AdjacentList.Add(sokoban[row, col + 1]); //add adjacent floor cell to our cell
                sokoban[row, col + 1].AdjacentList.Add(cellToConenct); //add our cell to adjacent cell
            }
            //west
            if (col - 1 > 0 && sokoban[row, col - 1] != null && sokoban[row, col - 1].isFloor() && !cellToConenct.AdjacentList.Contains(sokoban[row, col - 1]))
            {
                numberOfFloorsAdded++; //increment Number of floors added
                cellToConenct.AdjacentList.Add(sokoban[row, col - 1]); //add adjacent floor cell to our cell
                sokoban[row, col - 1].AdjacentList.Add(cellToConenct); //add our cell to adjacent cell
            }

            return numberOfFloorsAdded;
        }

        private static bool CheckFor3x4(SokobanCell[,] sokoban, int sokobanRows, int sokobanCols)
        {
            bool threeByFourExists = false;

            /*
             * The following Nested loop checks for 3x4 spaces of floors 
             */

            //goes through all rows bounded so 3x4 check does not cause index out of bounds
            for (int row = 0; row < sokobanRows - 2; row++)
            {
                //goes through all cols so 3x4 check does not cause index out of bounds
                for (int col = 0; col < sokobanCols - 3; col++)
                {
                    bool foundSpaceOnThisRun = true;
                    //this nested loop goes through a 3x4 space with sokoban[i , j] is the top right cell
                    for (int i = 0; i < 3; i++) //goes through 3 to the rows
                    {
                        for (int j = 0; j < 4; j++) //goes through 4 to the cols
                        {
                            //If either we have not put anything there or the sokoboan is not a floor ...
                            //The space can exist here, so we stop looking
                            if (sokoban[i + row, j + col] == null || (sokoban[i + row, j + col] != null && !sokoban[i + row, j + col].isFloor()))
                            {
                                foundSpaceOnThisRun = false;
                                break;
                            }
                        }
                        if (!foundSpaceOnThisRun) break; //stop looking
                    }
                    //if the above loop didn't hit that false we have found that we have a large open space so we stop looking
                    if (foundSpaceOnThisRun)
                    {
                        threeByFourExists = true;
                        break;
                    }
                }
                if (threeByFourExists) break; //stop looking for more 3x4 spaces
            }

            return threeByFourExists;
        }

        private static bool CheckFor4x3(SokobanCell[,] sokoban, int sokobanRows, int sokobanCols)
        {
            bool FourByThreeExists = false;

            /*
             * If we haven't found a 3x4 space we execute the following loop to look for 4x3 spaces
             */

            //goes through all rows bounded so 4x3 check does not cause index out of bounds
            for (int row = 0; row < sokobanRows - 3; row++)
            {
                //goes through all cols so 4x3 check does not cause index out of bounds
                for (int col = 0; col < sokobanCols - 2; col++)
                {
                    bool foundSpaceOnThisRun = true;
                    //this nested loop goes through a 3x4 space with sokoban[i , j] is the top right cell
                    for (int i = 0; i < 4; i++) //goes through 4 to the rows
                    {
                        for (int j = 0; j < 3; j++) //goes through 3 to the cols
                        {
                            //If either we have not put anything there or the sokoboan is not a floor ...
                            //The space can exist here, so we stop looking
                            if (sokoban[i + row, j + col] == null || (sokoban[i + row, j + col] != null && !sokoban[i + row, j + col].isFloor()))
                            {
                                foundSpaceOnThisRun = false;
                                break;
                            }
                        }
                        if (!foundSpaceOnThisRun) break; //stop looking
                    }
                    //if the above loop didn't hit that false we have found that we have a large open space so we stop looking
                    if (foundSpaceOnThisRun)
                    {
                        FourByThreeExists = true;
                        break;
                    }
                }
                if (FourByThreeExists) break; //stop looking for more 3x4 spaces
            }

            return FourByThreeExists;
        }

        private static bool LargeOpenSpacesExist(SokobanCell[,] sokoban)
        {
            int sokobanRows = sokoban.GetLength(0);
            int sokobanCols = sokoban.GetLength(1);

            /*
             * The following method checks for 3x4 spaces of floors 
             */
            bool largeOpenSpaceExists = CheckFor3x4(sokoban, sokobanRows, sokobanCols);

            /*
             * If we haven't found a 3x4 space we execute the following method to look for 4x3 spaces
             */
            if (!largeOpenSpaceExists)
            {
                largeOpenSpaceExists = CheckFor4x3(sokoban, sokobanRows, sokobanCols);
            }

            if (largeOpenSpaceExists)
            {
                Debug.Log("3x4 or 4x3 area found");
            }

            return largeOpenSpaceExists;
        }

        

        private static bool IsThereCellBorderedBy3Walls(SokobanCell[,] sokoban, Tuple<int, int> reigion, int roomSize)
        {
            bool foundCellWith3Walls = false;

            //goes through all rows in newly added room and the adjacent ones
            for (int row = 0; row < roomSize + 2; row++)
            {
                
                //goes through all cols in newly added room and the adjacent ones
                for (int col = 0; col < roomSize + 2; col++)
                {
                    

                    int rowPosInSokoban = row + reigion.Item1 -1;
                    int colPosInSokoban = col + reigion.Item2 -1;

                    //Only check floor tiles that are in bounds.
                    if (!IsOutOfSokobanBounds(rowPosInSokoban, colPosInSokoban, sokoban) && 
                        sokoban[rowPosInSokoban, colPosInSokoban] != null && sokoban[rowPosInSokoban, colPosInSokoban].isFloor()) 
                    {

                        int numberOfBorderWalls = 0;
                        //checks each bordering cell
                        numberOfBorderWalls += CheckIfCellIsBorderWall(rowPosInSokoban - 1, colPosInSokoban, sokoban); //north
                        numberOfBorderWalls += CheckIfCellIsBorderWall(rowPosInSokoban + 1, colPosInSokoban, sokoban); //south
                        numberOfBorderWalls += CheckIfCellIsBorderWall(rowPosInSokoban, colPosInSokoban + 1, sokoban); //east
                        numberOfBorderWalls += CheckIfCellIsBorderWall(rowPosInSokoban, colPosInSokoban - 1, sokoban); //west

                        //if our current cell has 3 walls stop looping
                        if (numberOfBorderWalls >= 3)
                        {
                            foundCellWith3Walls = true;
                            //Debug.Log("FoundCell with " + numberOfBorderWalls + " walls bordering it");
                            break;
                        }
                    }
                }
                //stop looping if we found a cell with 3 walls
                if (foundCellWith3Walls) break;
                
            }
            return foundCellWith3Walls;
        }

        

        private static bool AddRoomToSokoban(SokobanCell[,] sokoban, SokobanRoom roomToAdd, Tuple<int, int> reigion, int roomSize, int countAdded)
        {
            bool roomAdd = true;
            SokobanCell[,] roomArray = roomToAdd.RoomMatrix;
            
            int numberOfFloorsAdded = 0;
            //goes through center rows ignoring the "Check" rows at 0 and roomSize +1 indexes
            for (int row = 1; row < roomSize + 1; row++)
            {
                //goes through center cols ignoring the "Check" cols at 0 and roomSize +1 indexes
                for (int col = 1; col < roomSize + 1; col++)
                {
                    //Sets the cell at the designated room in col to the approiate space given the reigion offset
                    sokoban[row + reigion.Item1 - 1, col + reigion.Item2 - 1] = roomArray[row, col];

                    SokobanCell addedCell = sokoban[row + reigion.Item1 - 1, col + reigion.Item2 - 1];
                    if (addedCell.isFloor()) //connect the floors
                    {
                        numberOfFloorsAdded += ConnectFloors(addedCell, sokoban, row + reigion.Item1 - 1, col + reigion.Item2 - 1);
                    }
                }
            }

            Debug.Log("Number of floor connections made = " + numberOfFloorsAdded);
            //checks below

            //Floor spaces form one contiguous room
            //When connecting floor space check if at least one floor cell connects to an outside cell
            //or we just added our first room
            //Note: only works if all floors in template are connected
                    //Debug.Log("numberOfFloorsAdd = " + numberOfFloorsAdded);
            roomAdd &= countAdded == 0 || numberOfFloorsAdded > 0;

            //No large open spaces
                //At end of connecting floor spaces check if there is a 3x4 area room

            //roomAdd &= !LargeOpenSpacesExist(sokoban);

            //no cells border on all 3 sides by walls
                //check each newly added floor if it is border by 3 walls ignore it.
            
            //roomAdd &= !IsThereCellBorderedBy3Walls(sokoban, reigion, roomSize);

            //resets room if inner checks fail
            if (!roomAdd)
            { 
                for (int row = 1; row < roomSize + 1; row++)
                {
                    //goes through center cols ignoring the "Check" cols at 0 and roomSize +1 indexes
                    for (int col = 1; col < roomSize + 1; col++)
                    {
                        //Sets the cell at the designated room in col to the approiate space given the reigion offset
                        sokoban[row + reigion.Item1 - 1, col + reigion.Item2 - 1] = null;

                        //TO-DO Remove Floor connection
                    }
                }
            }
            return roomAdd;

        }

        // ************** End: Placement methods **************

        // ************** Start: Valid Ending room check methods **************
        private static int CheckIfCellIsBorderWall(int row, int col, SokobanCell[,] sokoban)
        {
            int numberOfBorderWalls = 0;
            //check if is out of bounds
            if (IsOutOfSokobanBounds(row, col, sokoban))
            {
                //out of bounds is a wall
                numberOfBorderWalls++;
            }
            else //if in bounds
            {
                SokobanCell borderCell = sokoban[row, col];
                //increment if the cell isn't null and is a wall
                if (borderCell != null && borderCell.GetType().Equals(typeof(WallCell)))
                {
                    numberOfBorderWalls++;
                }
            }
            return numberOfBorderWalls;
        }

        private static bool IsThereCellBorderedBy3WallsWhole(SokobanCell[,] sokoban)
        {
            bool foundCellWith3Walls = false;

            int sokobanRows = sokoban.GetLength(0);
            int sokobanCols = sokoban.GetLength(1);

            //goes through all rows in newly added room and the adjacent ones
            for (int row = 0; row < sokobanRows; row++)
            {

                //goes through all cols in newly added room and the adjacent ones
                for (int col = 0; col < sokobanCols; col++)
                {
                    //Only check floor tiles that are in bounds.
                    if (!IsOutOfSokobanBounds(row, col, sokoban) &&
                        sokoban[row, col] != null && sokoban[row, col].isFloor())
                    {

                        int numberOfBorderWalls = 0;
                        //checks each bordering cell
                        numberOfBorderWalls += CheckIfCellIsBorderWall(row - 1, col, sokoban); //north
                        numberOfBorderWalls += CheckIfCellIsBorderWall(row + 1, col, sokoban); //south
                        numberOfBorderWalls += CheckIfCellIsBorderWall(row, col + 1, sokoban); //east
                        numberOfBorderWalls += CheckIfCellIsBorderWall(row, col - 1, sokoban); //west

                        //if our current cell has 3 walls stop looping
                        if (numberOfBorderWalls >= 3)
                        {
                            foundCellWith3Walls = true;
                            //Debug.Log("FoundCell with " + numberOfBorderWalls + " walls bordering it");
                            break;
                        }
                    }
                }
                //stop looping if we found a cell with 3 walls
                if (foundCellWith3Walls) break;

            }
            return foundCellWith3Walls;
        }

        private static bool DoFinalChecks(SokobanCell[,] sokoban)
        {
            bool finalCheckSuccess = true;

            //check sufficent floor space 3 x number of boxes

            //Do in attempt Add?
            //Floor spaces form one contiguous room

            //No large open spaces
            //At end of connecting floor spaces check if there is a 3x4 area room

            finalCheckSuccess &= !LargeOpenSpacesExist(sokoban);

            //no cells border on all 3 sides by walls
            //check each newly added floor if it is border by 3 walls ignore it.
            finalCheckSuccess &= !IsThereCellBorderedBy3WallsWhole(sokoban);
            //roomAdd &= !IsThereCellBorderedBy3Walls(sokoban, reigion, roomSize);


            return finalCheckSuccess;
        }


        // ************** Start: Valid Ending room check methods **************

        /*
         * ~~~~~~~~~~~~~~~~~~~~~~~Public Methods~~~~~~~~~~~~~~~~~~~~~~~
         */

        public static SokobanCell[,] GenerateSokoban(int height, int width, int roomSize)
        {
            SokobanCell[,] sokoban = new SokobanCell[height * roomSize, width * roomSize];

            List<Tuple<int, int>> regions = GetRegionList(height, width, roomSize);

            int count = 0, fails = 0;

            while (regions.Count > 0)
            {

                
                //Debug.Log("~~~~~~~Current Puzzle~~~~~~~");
                //DebugPrintSokoban(sokoban);
                SokobanRoom room = new SokobanRoom(roomSize);
                //Debug.Log("~~~~~~~Random Room~~~~~~~");
                //DebugPrintSokoban(room.RoomMatrix);
                room.RotateRandomly();
                room.ReflectRandomly();

                //chose random reigon
                int randomRegionNum = UnityEngine.Random.Range(0, regions.Count);
                
                //Debug.Log("randomReigonNum = " + randomRegionNum);
                //Debug.Log("Regions size = " + regions.Count);
                //Debug.Log("count of added rooms = " + count);
                 
                Tuple<int, int> reigion = regions[randomRegionNum];
                //if template can be placed in reigon place it on the board count = count + 1
                if (CheckRequiredCells(sokoban, room, reigion))
                {
                    if(AddRoomToSokoban(sokoban, room, reigion, roomSize, height * width - regions.Count))
                    {
                        regions.RemoveAt(randomRegionNum);
                        count += 1;
                    }
                    else
                    {
                        fails += 1;
                        if (fails > maxFails)
                        {
                            Debug.Log("Sokoban: Hit Max fails");
                            fails = 0;
                            count = 0;
                            sokoban = new SokobanCell[height * roomSize, width * roomSize];
                            regions = GetRegionList(height, width, roomSize);
                        }
                    }
                    
                }
                else//else increment fail count and check if we need to reset from max amount of fails
                {
                    fails += 1;
                    if (fails > maxFails)
                    {
                        Debug.Log("Sokoban: Hit Max fails");
                        fails = 0;
                        count = 0;
                        sokoban = new SokobanCell[height * roomSize, width * roomSize];
                        regions = GetRegionList(height, width, roomSize);
                    }
                }
            }

            //If final Checks fail regerenate sokoban
            if (!DoFinalChecks(sokoban))
            {
                Debug.Log("Final Checks Failed Regenerating");
                sokoban = GenerateSokoban(height, width, roomSize);
                
            }
            //do final checks to prepare for boxes
            //Most have a cert number of floor spaces

            return sokoban;
        }

        public static void DebugPrintSokoban(SokobanCell[,] toPrint)
        {
            int rowNum = toPrint.GetLength(0);
            int colNum = toPrint.GetLength(1);

            Debug.Log("Printing Sokoban:");

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
                        else
                        {
                            line += "E";
                        }
                    }
                     
                }
                Debug.Log(line);
            }
        }
    }
}
