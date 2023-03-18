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
                            board[row, col] = new FloorCell(sc);
                        }
                    }
                }

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
                    for(int j = i+1; j< floorLocations.Count; j++)
                    {
                        //temp mark each floor as goals
                        System.Tuple<int, int> floorLoc1 = floorLocations[i];
                        System.Tuple<int, int> floorLoc2 = floorLocations[i]; //doesn't work with j?

                        board[floorLoc1.Item1, floorLoc1.Item2] = new GoalCell(board[floorLoc1.Item1, floorLoc1.Item2]);
                        board[floorLoc2.Item1, floorLoc2.Item2] = new GoalCell(board[floorLoc2.Item1, floorLoc2.Item2]);

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
                                tup.Item1[floorLoc1.Item1, floorLoc1.Item2] = new GoalCell(tup.Item1[floorLoc1.Item1, floorLoc1.Item2]);
                                tup.Item1[floorLoc2.Item1, floorLoc2.Item2] = new GoalCell(tup.Item1[floorLoc2.Item1, floorLoc2.Item2]);
                            }
                        }
                        //turn back to floors
                        board[floorLoc1.Item1, floorLoc1.Item2] = new FloorCell(board[floorLoc1.Item1, floorLoc1.Item2]);
                        board[floorLoc2.Item1, floorLoc2.Item2] = new FloorCell(board[floorLoc2.Item1, floorLoc2.Item2]);
                    }
                }

                Debug.Log("GenerateSokobanGoals- Best level count = " + best.Count);

                //chose one "level" from best
                
                

                SokobanCell[,] level = new SokobanCell[sokobanRows, sokobanCols];
                int maxDifference = -1;
                foreach (System.Tuple<SokobanCell[,], int> tuple in best)
                {
                    if(tuple.Item2 > maxDifference)
                    {
                        level = tuple.Item1;
                        maxDifference = tuple.Item2;
                    }
                }
                //set the board equal to the level
                board = level;
                //set our count to count plus two because we have added 2 things
                count += 2;
            }

            return board;
        }

        private static int GetLengthOfSolutions(HashSet<System.Tuple<SokobanCell[,], int>> set)
        {
            int solutionLength = 0;
            foreach (System.Tuple<SokobanCell[,], int> tuple in set)
            {
                Debug.Log(tuple);
                solutionLength += tuple.Item2;
            }
            return solutionLength;
        }

        private static HashSet<System.Tuple<SokobanCell[,], int>> Search(SokobanCell[,] board)
        {
            int sokobanRows = board.GetLength(0);
            int sokobanCols = board.GetLength(1);

            HashSet<System.Tuple<SokobanCell[,], int>> start = new();

            //for each goal on board
            for (int row = 0; row < sokobanRows; row++)
            {
                for (int col = 0; col < sokobanCols; col++)
                {
                    SokobanCell sc = board[row, col];

                    if (sc.GetType().Equals(typeof(GoalCell)))
                    {
                        board[row, col] = new BoxCell(sc); //place a box on the goal
                    }
                }
            }

            //for each contigous section of floor on board
            for (int row = 0; row < sokobanRows; row++)
            {
                for (int col = 0; col < sokobanCols; col++)
                {
                    SokobanCell sc = board[row, col];

                    if (sc.GetType().Equals(typeof(FloorCell)))
                    {
                        //add board with player in section to start
                        SokobanCell[,] clone = (SokobanCell[,]) board.Clone();
                        clone[row, col] = new PlayerSpawnCell(clone[row, col]);
                        start.Add(new System.Tuple<SokobanCell[,], int>(clone, 0));
                    }
                }
            }

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
                
                Debug.Log("GenerateSokobanGoals-Search - temp count = " + temp.Count);
                if(temp.Count == 0)
                {
                    //return temp;
                    return frontier_main;
                }

                //frontier.main = temp


                temp = SetDifference(temp, frontier_second);
                //frontier.second = start
                frontier_second = start;
                //frontier.main = frontier.main - frontier.second
                temp = SetDifference(temp, frontier_second); //does the set difference as intended


                for (int i = 0; i < layer; i++)
                {
                    //frontier.second = Advence(frontier.second)
                    frontier_second = Advance(frontier_second);
                    //frontier.main = frontier.main - frontier.second
                    temp = SetDifference(temp, frontier_second);

                    if (temp.Count <= 1)
                    {
                        //return temp;
                        return Advance(frontier_main);
                    }
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
            return set1.Count > 1 && SokobanBoardEquals(tuple1.Item1, tuple2.Item1);
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
            foreach(System.Tuple<SokobanCell[,], int> tuple in frontier)
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
                            Debug.Log("GenerateSokobanGoals-Advance- new state count = " + newStates.Count);
                            foreach (System.Tuple<SokobanCell[,], int> straightLineState in newStates)
                            {
                                //if box is not a slippery tile in temp.state -- IGNORE FOR NOW
                                if(tuple.Item2 <= straightLineState.Item2)
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

        private static bool ThereIsSpaceForPlayerToPull(int checkRow, int checkCol, SokobanCell[,] frontierState, int directionX, int directionY)
        {
            return !SokobanHelper.IsOutOfSokobanBounds(checkRow, checkCol, frontierState) && frontierState[checkRow, checkCol].isFloor()
                && !SokobanHelper.IsOutOfSokobanBounds(checkRow + directionX, checkCol + directionY, frontierState) && frontierState[checkRow + directionX, checkCol + directionY].isFloor();
        }

        private static HashSet<System.Tuple<SokobanCell[,], int>> GetBoxPullingStates(int row, int col, SokobanCell[,] frontierState, int oldOffset)
        {
            HashSet<System.Tuple<SokobanCell[,], int>> reachableStates = new();
            /*
            Debug.Log("GetBoxPullingStates called");
            Debug.Log("~~~~~Frontier State~~~~~");
            SokobanHelper.DebugPrintSokoban(frontierState);
            Debug.Log("Row-"+row + " Col-"+col);
            */
            //north
            int offset = 1;
            while (ThereIsSpaceForPlayerToPull(row - offset, col, frontierState, -1, 0)) //because next cell is -1 in rows
            {
                offset++;
            }
            offset--;//subtract one to get accurate offset
            if(offset != 0)
            {
                SokobanCell[,] newGameState = (SokobanCell[,])frontierState.Clone();
                //old cell no longer a box
                newGameState[row, col] = new FloorCell(newGameState[row, col]);
                //new box is now at offset location
                newGameState[row - offset, col] = new BoxCell(newGameState[row - offset, col]);
                //add to result set
                reachableStates.Add(new System.Tuple<SokobanCell[,], int>(newGameState, offset + oldOffset));
            }
            //south
            offset = 1;
            while (ThereIsSpaceForPlayerToPull(row + offset, col, frontierState, 1, 0)) //because next cell is +1 in rows
            {
                offset++;
            }
            offset--;//subtract one to get accurate offset
            if (offset != 0)
            {
                SokobanCell[,] newGameState = (SokobanCell[,])frontierState.Clone();
                //old cell no longer a box
                newGameState[row, col] = new FloorCell(newGameState[row, col]);
                //new box is now at offset location
                newGameState[row + offset, col] = new BoxCell(newGameState[row + offset, col]);
                //add to result set
                reachableStates.Add(new System.Tuple<SokobanCell[,], int>(newGameState, offset + oldOffset));
            }
            //east
            offset = 1;
            while (ThereIsSpaceForPlayerToPull(row, col + offset, frontierState, 0, 1)) //because next cell is +1 in cols
            {
                offset++;
            }
            offset--;//subtract one to get accurate offset
            if (offset != 0)
            {
                SokobanCell[,] newGameState = (SokobanCell[,])frontierState.Clone();
                //old cell no longer a box
                newGameState[row, col] = new FloorCell(newGameState[row, col]);
                //new box is now at offset location
                newGameState[row, col + offset] = new BoxCell(newGameState[row, col + offset]);
                //add to result set
                reachableStates.Add(new System.Tuple<SokobanCell[,], int>(newGameState, offset + oldOffset));
            }
            //west
            offset = 1;
            while (ThereIsSpaceForPlayerToPull(row, col - offset, frontierState, 0, -1)) //because next cell is +1 in cols
            {
                offset++;
            }
            offset--;//subtract one to get accurate offset
            if (offset != 0)
            {
                SokobanCell[,] newGameState = (SokobanCell[,])frontierState.Clone();
                //old cell no longer a box
                newGameState[row, col] = new FloorCell(newGameState[row, col]);
                //new box is now at offset location
                newGameState[row, col - offset] = new BoxCell(newGameState[row, col - offset]);
                //add to result set
                reachableStates.Add(new System.Tuple<SokobanCell[,], int>(newGameState, offset + oldOffset));
            }
            Debug.Log("ReachableStates count-" + reachableStates.Count);
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
    }
}

