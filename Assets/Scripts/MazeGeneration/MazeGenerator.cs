using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.MazeGeneration
{
    
    public class MazeGenerator
    {
        public static MazeGenerator instance = new MazeGenerator();

        private GameObject parent;
        private int seed;
        private int location;

        private int rowTotal = MazeController.ROWS;
        private int columnTotal = MazeController.COLUMNS;
        //private GameObject floorPrefab;
        private GameObject wallPrefab;
        private System.Random random;
        private WallGenerator wallGenerator;
        private List<List<int>> pathLists;
        private List<Wall> walls;
        private List<Wall> potentialEntryExit;
        private Wall currentWall;
        private GameObject floor;

        public void SetPrefabs(GameObject wallPrefab)
        {
            //this.floorPrefab = floorPrefab;
            this.wallPrefab = wallPrefab;
        } 

        public void SetValues(GameObject parent, int seed, int location)
        {
            this.parent = parent;
            this.seed = seed;
            this.location = location;
        }

        public void GenerateMaze()
        {
            wallGenerator = new WallGenerator(rowTotal, columnTotal);
            walls = new List<Wall>();
            walls = wallGenerator.GenerateWalls(parent.transform, wallPrefab);
            pathLists = new List<List<int>>();
            potentialEntryExit = new List<Wall>();
            random = new System.Random(seed);
            CreateMaze();
            int cellNum;
            switch (location)
            {
                case 0: cellNum = 0; break;
                case 1: cellNum = rowTotal * (columnTotal - 1); break;
                case 2: cellNum = rowTotal* columnTotal -1; break;
                case 3: cellNum = rowTotal - 1; break;
                default: cellNum = -1; break;
            }
            CreateEntrance(cellNum);
            //CreateEntrance();
            //CreateFloor();
        }

        private void CreateMaze()
        {
            while (walls.Count > 0)
            {
                int wallIndex = random.Next(walls.Count);
                currentWall = walls[wallIndex];
                walls.RemoveAt(wallIndex);

                //Check if wall is a border wall
                if (currentWall.GetCells().Contains(-1))
                {
                    potentialEntryExit.Add(currentWall);
                }
                else
                {
                    if (!MazeAnalysis.CheckForLoop(currentWall.GetCells(), pathLists))
                    {
                        if (MazeAnalysis.CheckForPath(currentWall.GetCells(), pathLists))
                        {
                            pathLists = MazeAnalysis.MergePaths(currentWall.GetCells(), pathLists);
                        }
                        else
                        {
                            pathLists.Add(currentWall.GetCells());
                        }
                        currentWall.DestroyWall();
                    }
                }
            }
        }

        private void CreateEntrance(int cellNum)
        {
            bool created1 = false;
            bool created2 = false;
            bool created = created1 && created2;
            while (!created && potentialEntryExit.Count > 0)
            {
                int wallIndex = 0;//random.Next(potentialEntryExit.Count);
                currentWall = potentialEntryExit[wallIndex];
                potentialEntryExit.RemoveAt(wallIndex);
                if (currentWall.GetCells().Contains(cellNum))
                {
                    if (MazeAnalysis.CheckForPath(currentWall.GetCells(), pathLists))
                    {
                        pathLists = MazeAnalysis.MergePaths(currentWall.GetCells(), pathLists);
                        currentWall.DestroyWall();
                        if (created1)
                            created2 = false;
                        else created1 = true;
                    }
                }
                
                created = created1 && created2;

            }
        }
        
        /*
        private void CreateFloor()
        {
            float xScale = columnTotal / 10f;
            float zScale = rowTotal / 10f;
            floor = UnityEngine.Object.Instantiate(floorPrefab, new Vector3(columnTotal / 2f, 0, rowTotal / 2f), Quaternion.identity);
            floor.transform.localScale = new Vector3(xScale, 1, zScale);
            floor.transform.parent = parent.transform;
        }
        */
    }
}
