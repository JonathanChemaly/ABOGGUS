using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public static class SokobanGoals
    {
        public static SokobanCell[,] GenerateSokobanGoals(SokobanCell[,] board, int numberOfBoxesToAdd)
        {
            int count = 0;

            int sokobanRows = board.GetLength(0);
            int sokobanCols = board.GetLength(1);

            while (count < numberOfBoxesToAdd)
            {
                Debug.Log("GenerateSokobanGoals- Goals add = " + count);
                //Erase player and boxes from board
                //goes through all rows
                for (int row = 0; row < sokobanRows; row++)
                {
                    //goes through all cols
                    for (int col = 0; col < sokobanCols; col++)
                    {
                        SokobanCell sc = board[row, col];

                        if ((sc.GetType().Equals(typeof(PlayerSpawnCell)) || sc.GetType().Equals(typeof(BoxCell))))
                        {
                            board[row, col] = new FloorCell();
                        }
                    }
                }

                /*
                 * Code to add player into level
                 */
                SetPlayerSpawn(sokobanRows, sokobanCols, board);

                //intialize a list of floors to empty
                List<System.Tuple<int, int>> floorLocations = new();
                //for each floor in our puzzle add it to our list
                for (int row = 0; row < sokobanRows; row++)
                {
                    //goes through all cols
                    for (int col = 0; col < sokobanCols; col++)
                    {
                        SokobanCell sc = board[row, col];

                        if (sc.GetType().Equals(typeof(FloorCell)))
                        {
                            floorLocations.Add(new System.Tuple<int, int>(row, col));
                        }
                    }
                }

                Debug.Log("GenerateSokobanGoals- number of floors = " + floorLocations.Count);

                //shuffle the list of floors?
                Shuffle(floorLocations);

                //intialize our variable for solution
                HashSet<System.Tuple<SokobanCell[,], int>> best = new();
                //for each pair of floor cells in our list
                for (int i = 0; i < floorLocations.Count; i++)
                {
                    //temp mark each floor as goals
                    System.Tuple<int, int> floorLoc1 = floorLocations[i];

                    board[floorLoc1.Item1, floorLoc1.Item2] = new GoalCell();

                    //execute search to get a puzzle solution
                    HashSet<System.Tuple<SokobanCell[,], int>> temp = Search(board);
                    Debug.Log("GenerateSokobanGoals- temp level count = " + temp.Count);
                    Debug.Log("GenerateSokobanGoals- temp level Length = " + GetLengthOfSolutions(temp));
                    //if the length of our solution is greatter then the length of best we change best to our temp solution
                    if (GetLengthOfSolutions(temp) > GetLengthOfSolutions(best))
                    {
                        best = temp;
                        Debug.Log("GenerateSokobanGoals- Best level count = " + best.Count);

                        //Turn the goal cells back in the solutions
                        foreach (System.Tuple<SokobanCell[,], int> tup in best)
                        {
                            tup.Item1[floorLoc1.Item1, floorLoc1.Item2] = new GoalCell();
                        }
                    }
                    //turn back to floors
                    board[floorLoc1.Item1, floorLoc1.Item2] = new FloorCell();

                }

                Debug.Log("GenerateSokobanGoals- Best level count = " + best.Count);

                //chose one "level" from best

                SokobanCell[,] level = new SokobanCell[sokobanRows, sokobanCols];
                int maxDifference = -1;
                foreach (System.Tuple<SokobanCell[,], int> tuple in best)
                {
                    if (tuple.Item2 > maxDifference)
                    {
                        level = tuple.Item1;
                        maxDifference = tuple.Item2;
                    }
                }
                //set the board equal to the level
                board = level;
                //set our count to count plus two because we have added 2 things
                count++;
            }

            return board;
        }

        /**
         * Code to add player into level
         */
        private static void SetPlayerSpawn(int sokobanRows, int sokobanCols, SokobanCell[,] board)
        {
            
            bool playerSpawnCellAdded = false;
            //for each contigous section of floor on board
            for (int row = 0; row < sokobanRows && !playerSpawnCellAdded; row++)
            {
                for (int col = 0; col < sokobanCols && !playerSpawnCellAdded; col++)
                {
                    SokobanCell sc = board[row, col];

                    if (sc.GetType().Equals(typeof(FloorCell)))
                    {
                        //add board with player in section to 
                        board[row, col] = new PlayerSpawnCell();

                        playerSpawnCellAdded = true;

                    }
                }
            }
            Debug.Log("Player Spawn Added to board = " + playerSpawnCellAdded);
        }

        private static System.Tuple<int, int> FindGoal(SokobanCell[,] board, int numRows, int numCols)
        {
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    SokobanCell sc = board[row, col];

                    if (sc.GetType().Equals(typeof(GoalCell)))
                    {
                        return new System.Tuple<int, int>(row, col);
                    }
                }
            }

            return null;
        }

        public static SokobanCell[,] TestSearch(SokobanCell[,] board)
        {
            int sokobanRows = board.GetLength(0);
            int sokobanCols = board.GetLength(1);

            System.Tuple<int, int> goalLoc = FindGoal(board, sokobanRows, sokobanCols);

            /*
             * Code to add player into level
             */
            SetPlayerSpawn(sokobanRows, sokobanCols, board);

            HashSet<System.Tuple<SokobanCell[,], int>> temp = Search(board);
            Debug.Log("TestSearch- temp level count = " + temp.Count);
            Debug.Log("TestSearch- temp level Length = " + GetLengthOfSolutions(temp));


            foreach (System.Tuple<SokobanCell[,], int> tup in temp)
            {
                tup.Item1[goalLoc.Item1, goalLoc.Item2] = new GoalCell();
            }

            //chose one "level" from best

            SokobanCell[,] level = new SokobanCell[sokobanRows, sokobanCols];
            int maxDifference = -1;
            foreach (System.Tuple<SokobanCell[,], int> tuple in temp)
            {
                Debug.Log("DistanceFromGoal = " + tuple.Item2);
                SokobanHelper.DebugPrintSokoban(tuple.Item1);
                if (tuple.Item2 > maxDifference)
                {
                    level = tuple.Item1;
                    maxDifference = tuple.Item2;
                }
            }
            //set the board equal to the level

            return level;
        }

        private static int GetLengthOfSolutions(HashSet<System.Tuple<SokobanCell[,], int>> set)
        {
            int solutionLength = -1;
            foreach (System.Tuple<SokobanCell[,], int> tuple in set)
            {
                if(solutionLength < tuple.Item2)
                {
                    solutionLength = tuple.Item2;
                }
            }
            return solutionLength;
        }

        private static HashSet<System.Tuple<SokobanCell[,], int>> Search(SokobanCell[,] board)
        {
            int sokobanRows = board.GetLength(0);
            int sokobanCols = board.GetLength(1);

            HashSet<System.Tuple<SokobanCell[,], int>> start = new();
            HashSet<System.Tuple<SokobanCell[,], int>> startClone = new();

            //for each goal on board
            for (int row = 0; row < sokobanRows; row++)
            {
                for (int col = 0; col < sokobanCols; col++)
                {
                    SokobanCell sc = board[row, col];

                    if (sc.GetType().Equals(typeof(GoalCell)))
                    {
                        board[row, col] = new BoxCell(); //place a box on the goal
                    }
                }
            }

            start.Add(new System.Tuple<SokobanCell[,], int>(SokobanHelper.Clone(board), 0));
            startClone.Add(new System.Tuple<SokobanCell[,], int>(SokobanHelper.Clone(board), 0));


            //
            HashSet<System.Tuple<SokobanCell[,], int>> frontier_main = start;
            HashSet<System.Tuple<SokobanCell[,], int>> frontier_second = start;
            //
            int layer = 0;
            //repeat until returned

            while (true)
            {
                //temp = advance(frontier.main)
                HashSet<System.Tuple<SokobanCell[,], int>> temp = Advance(frontier_main);
                //if Advance returns an empty set we found our solution
                if(layer != 0 ) temp = SetDifference(temp, frontier_second);

                Debug.Log("!!!!!!!!!!!!!!ADVANCE");
                PrintAllSokobanInSet(temp);
                Debug.Log("!!!!!!!!!!!!!!END ADVANCE");

                Debug.Log("GenerateSokobanGoals-Search - temp count = " + temp.Count);
                if (temp.Count == 0)
                {
                    //return temp
                    return frontier_main;

                }


                //frontier.main = temp


                temp = SetDifference(temp, frontier_second);
                //frontier.second = start
                frontier_second = startClone;
                //frontier.main = frontier.main - frontier.second
                temp = SetDifference(temp, frontier_second); //does the set difference as intended

                Debug.Log("!!!!!!!!!!!!!!TEMP IN SEARCH");
                PrintAllSokobanInSet(temp);
                Debug.Log("!!!!!!!!!!!!!!END TEMP IN SEARCH");

                for (int i = 0; i < layer; i++)
                {
                    //frontier.second = Advence(frontier.second)
                    frontier_second = Advance(frontier_second);

                    //frontier.main = frontier.main - frontier.second
                    temp = SetDifference(temp, frontier_second);
                }

                if (temp.Count < 1)
                {
                    //return temp;
                    temp = Advance(Advance(frontier_main));
                    return temp;
                }

                frontier_main = temp;

                layer++;
            }

        }

        private static HashSet<System.Tuple<SokobanCell[,], int>> SetDifference(HashSet<System.Tuple<SokobanCell[,], int>> set1, HashSet<System.Tuple<SokobanCell[,], int>> set2)
        {
            foreach (System.Tuple<SokobanCell[,], int> set2Tuple in set2)
            {
                set1.RemoveWhere(t => SokobanTupleEquals(set1, t, set2Tuple));
            }

            return set1;
        }

        private static bool SokobanTupleEquals(HashSet<System.Tuple<SokobanCell[,], int>> set1, System.Tuple<SokobanCell[,], int> tuple1, System.Tuple<SokobanCell[,], int> tuple2)
        {
            return SokobanBoardEquals(tuple1.Item1, tuple2.Item1);
        }

        private static bool SokobanBoardEquals(SokobanCell[,] sb1, SokobanCell[,] sb2)
        {
            bool equals = true;
            int sokobanRows = sb1.GetLength(0);
            int sokobanCols = sb1.GetLength(1);

            for (int row = 0; row < sokobanRows; row++)
            {
                for (int col = 0; col < sokobanCols; col++)
                {
                    //check if type of each square is equal. if it is not return false.
                    if (!sb1[row, col].GetType().Equals(sb2[row, col].GetType()))
                    {
                        return false;
                    }
                }
            }

            return equals;
        }

        private static HashSet<System.Tuple<SokobanCell[,], int>> Advance(HashSet<System.Tuple<SokobanCell[,], int>> frontier)
        {
            HashSet<System.Tuple<SokobanCell[,], int>> results = new();

            //for each frontier.state in frontier
            foreach (System.Tuple<SokobanCell[,], int> tuple in frontier)
            {
                SokobanCell[,] frontierState = tuple.Item1;

                int frontierRows = frontierState.GetLength(0);
                int frontierCols = frontierState.GetLength(1);
                //for each box reachable by the player in frontier.state 
                //because of the level being all connected all boxes should be reachable
                for (int row = 0; row < frontierRows; row++)
                {
                    for (int col = 0; col < frontierCols; col++)
                    {
                        SokobanCell sc = frontierState[row, col];

                        if (sc.GetType().Equals(typeof(BoxCell)))
                        {
                            //for each temp.state reachable by pulling box along a straight line
                            HashSet<System.Tuple<SokobanCell[,], int>> newStates = GetBoxPullingStates(row, col, frontierState, tuple.Item2);
                            //Debug.Log("GenerateSokobanGoals-Advance- new state count = " + newStates.Count);
                            foreach (System.Tuple<SokobanCell[,], int> straightLineState in newStates)
                            {
                                //if box is not a slippery tile in temp.state -- IGNORE FOR NOW
                                if (tuple.Item2 <= straightLineState.Item2)
                                {
                                    //add temp.state to results
                                    results.Add(straightLineState);
                                }

                            }
                        }
                    }
                }
            }

            return results;
        }

        private static System.Tuple<int, int> FindPlayer(SokobanCell[,] frontierState, int frontierRows, int frontierCols)
        {
            for (int row = 0; row < frontierRows; row++)
            {
                for (int col = 0; col < frontierCols; col++)
                {
                    SokobanCell sc = frontierState[row, col];

                    if (sc.PlayerIsHere)
                    {
                        return new System.Tuple<int, int>(row, col);
                    }
                }
            }

            return null;
        }

        private static bool IsCellReachable(int checkRow, int checkCol, SokobanCell[,] frontierState, System.Tuple<int, int> playerLoc, List<System.Tuple<int,int>> visted)
        {
            visted.Add(new System.Tuple<int, int>(checkRow, checkCol));
            if (!SokobanHelper.IsOutOfSokobanBounds(checkRow, checkCol, frontierState))
            {
                SokobanCell cellToReach = frontierState[checkRow, checkCol];

                if (cellToReach.IsFloor())
                {
                    if(checkRow == playerLoc.Item1 && checkCol == playerLoc.Item2)
                    {
                        //if the current check row is the location of the player cell can reach it.
                        return true;
                    }
                    else
                    {
                        //current cell is reachable if one of the adjacent cells are reachable
                        bool adjacentCellReachable = false;
                        if (!visted.Contains(new System.Tuple<int, int>(checkRow + 1, checkCol)))
                        {
                            adjacentCellReachable = IsCellReachable(checkRow + 1, checkCol, frontierState, playerLoc, visted);
                        }
                        if (!adjacentCellReachable && !visted.Contains(new System.Tuple<int, int>(checkRow - 1, checkCol)))
                        {
                            adjacentCellReachable = IsCellReachable(checkRow - 1, checkCol, frontierState, playerLoc, visted);
                        }
                        if (!adjacentCellReachable && !visted.Contains(new System.Tuple<int, int>(checkRow, checkCol - 1)))
                        {
                            adjacentCellReachable = IsCellReachable(checkRow, checkCol - 1, frontierState, playerLoc, visted);
                        }
                        if (!adjacentCellReachable && !visted.Contains(new System.Tuple<int, int>(checkRow, checkCol + 1)))
                        {
                            adjacentCellReachable = IsCellReachable(checkRow, checkCol + 1, frontierState, playerLoc, visted);
                        }

                        return adjacentCellReachable;
                    }
                    
                }
            }

            return false;
        }

        private static bool ThereIsSpaceForPlayerToPull(int checkRow, int checkCol, SokobanCell[,] frontierState, int directionX, int directionY, System.Tuple<int, int> playerLoc)
        {
            return IsCellReachable(checkRow, checkCol, frontierState, playerLoc, new List<System.Tuple<int, int>>())
                && !SokobanHelper.IsOutOfSokobanBounds(checkRow + directionX, checkCol + directionY, frontierState) && frontierState[checkRow + directionX, checkCol + directionY].IsFloor();
        }

        /**
         * Sets new play postion to put the player where to pull would come from
         */
        private static void SetNewPlayerPostion(System.Tuple<int, int> playerLoc, int offsetX, int offsetY, SokobanCell[,] newGameState, int row, int col)
        {
            newGameState[playerLoc.Item1, playerLoc.Item2].PlayerIsHere = false;
            newGameState[row + offsetX, col + offsetY].PlayerIsHere = true;

            //Debug.Log("new Player postion = " + (row + offsetX) + ", " + (col + offsetY));
        }

        /**
         * Checks the pull in one direction and adds the new gamestate if you can pull in that direction
         * 
         * incrementX = -1, incrementY = 0 is north
         * incrementX = 1, incrementY = 0 is south
         * incrementX = 0, incrementY = 1 is east
         * incrementX = 0, incrementY = -1 is west
         */
        private static void CheckPullInDirection(int incrementX, int incrementY, int row, int col, int oldOffset, SokobanCell[,] frontierState, HashSet<System.Tuple<SokobanCell[,], int>> reachableStates, System.Tuple<int, int> playerLoc)
        {
            int offsetX = incrementX;
            int offsetY = incrementY;
            if (ThereIsSpaceForPlayerToPull(row + offsetX, col + offsetY, frontierState, incrementX, incrementY, playerLoc)) //because next cell is -1 in rows
            {
                offsetX += incrementX;
                offsetY += incrementY;
            }

            //subtract one to get accurate offset
            offsetX -= incrementX;
            offsetY -= incrementY;
            if (offsetX != 0 || offsetY != 0)
            {
                SokobanCell[,] newGameState = SokobanHelper.Clone(frontierState);

                //set new playerlocation to place of pull
                SetNewPlayerPostion(playerLoc, offsetX + incrementX, offsetY + incrementY, newGameState, row, col);

                //old cell no longer a box
                newGameState[row, col] = new FloorCell();
                //new box is now at offset location
                newGameState[row + offsetX, col + offsetY] = new BoxCell();

                
                //add to result set
                reachableStates.Add(new System.Tuple<SokobanCell[,], int>(newGameState, System.Math.Abs(offsetX + offsetY) + oldOffset));
            }
        }


        private static HashSet<System.Tuple<SokobanCell[,], int>> GetBoxPullingStates(int row, int col, SokobanCell[,] frontierState, int oldOffset)
        {
            HashSet<System.Tuple<SokobanCell[,], int>> reachableStates = new();

            int frontierRows = frontierState.GetLength(0);
            int frontierCols = frontierState.GetLength(1);

            //find playerCell 
            System.Tuple<int, int> playerLoc = FindPlayer(frontierState, frontierRows, frontierCols);


            //Debug.Log("playerloc = " + playerLoc);
            //if we do not find a player return empty reachability
            if(playerLoc == null)
            {
                return reachableStates;
            }

            /*
            Debug.Log("~~~~~~~~~~~~~~~GetBoxPullingStates level");
            SokobanHelper.DebugPrintSokoban(frontierState);
            Debug.Log("~~~~~~~~~~~~~~~GetBoxPullingStates level");
            */

            //north
            CheckPullInDirection(-1, 0, row, col, oldOffset, frontierState, reachableStates, playerLoc);
            //south
            CheckPullInDirection(1, 0, row, col, oldOffset, frontierState, reachableStates, playerLoc);
            //east
            CheckPullInDirection(0, 1, row, col, oldOffset, frontierState, reachableStates, playerLoc);
            //west
            CheckPullInDirection(0, -1, row, col, oldOffset, frontierState, reachableStates, playerLoc);
            //Debug.Log("ReachableStates count-" + reachableStates.Count);
            return reachableStates;
        }

        //Fisher–Yates shuffle
        private static void Shuffle(List<System.Tuple<int, int>> ls)
        {
            for (int i = ls.Count -1; i > 1; i--)
            {
                int j = Random.Range(0, i + 1);
                System.Tuple<int, int> temp = ls[i];
                ls[i] = ls[j];
                ls[j] = temp;
            }
        }

        private static void PrintAllSokobanInSet(HashSet<System.Tuple<SokobanCell[,], int>> set)
        {
            foreach(System.Tuple<SokobanCell[,], int> tup in set)
            {
                Debug.Log("Distance to Goal = " + tup.Item2);
                Debug.Log("cur player postion = " + FindPlayer(tup.Item1, tup.Item1.GetLength(0), tup.Item1.GetLength(1))?.ToString());
                SokobanHelper.DebugPrintSokoban(tup.Item1);
                
            }
        }

        
    }
}

