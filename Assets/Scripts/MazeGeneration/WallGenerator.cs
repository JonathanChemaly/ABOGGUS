using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.MazeGeneration
{
    public class WallGenerator
    {
        private int rowTotal;
        private int columnTotal;
        private List<Wall> walls, NSWalls, EWWalls;
        private bool NSWall;

        public WallGenerator(int totalRows, int totalColumns)
        {
            rowTotal = totalRows;
            columnTotal = totalColumns;
            walls = new List<Wall>();
            NSWalls = new List<Wall>();
            EWWalls = new List<Wall>();
        }

        public List<Wall> GenerateWalls(Transform parent, GameObject wallPrefab)
        {
            walls.AddRange(GenerateNSWalls(parent, wallPrefab));
            walls.AddRange(GenerateEWWalls(parent, wallPrefab));
            return walls;
        }
        
        private List<Wall> GenerateNSWalls(Transform parent, GameObject wallPrefab)
        {
            NSWall = true;

            for (int i = 0; i <= rowTotal; i++)
            {
                for (int j = 0; j < columnTotal; j++)
                {
                    Wall wall = new Wall(NSWall, i, j, rowTotal, columnTotal, parent, wallPrefab);
                    NSWalls.Add(wall);
                }
            }

            return NSWalls;
        }

        private List<Wall> GenerateEWWalls(Transform parent, GameObject wallPrefab)
        {
            NSWall = false;

            for (int i = 0; i < rowTotal; i++)
            {
                for (int j = 0; j <= columnTotal; j++)
                {
                    Wall wall = new Wall(NSWall, i, j, rowTotal, columnTotal, parent, wallPrefab);
                    EWWalls.Add(wall);
                }
            }

            return EWWalls;
        }
    }
}
