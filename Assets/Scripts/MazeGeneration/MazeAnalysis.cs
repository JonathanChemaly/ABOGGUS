using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.MazeGeneration
{
    public static class MazeAnalysis
    {
       //Check if current Wall creates loop
       public static bool CheckForLoop(List<int> wallCells, List<List<int>> pathLists)
       {
            bool loop = false;
            int index = 0;

            while (!loop && index < pathLists.Count)
            {
                List<int> path = pathLists[index];
                if (path.Contains(wallCells[0]) && path.Contains(wallCells[1]))
                {
                    loop = true;
                }
                index++;
            }
            return loop;
       }

       //Check if current Wall is part of a path
       public static bool CheckForPath(List<int> wallCells, List<List<int>> pathLists)
       {
            bool inPath = false;
            int index = 0;

            while (!inPath && index < pathLists.Count)
            {
                List<int> path = pathLists[index];
                if (path.Contains(wallCells[0]) || path.Contains(wallCells[1]))
                {
                    inPath = true;
                }
                index++;
            }

            return inPath;
       }

       //Add and merge paths together
       public static List<List<int>> MergePaths(List<int> wallCells, List<List<int>> pathLists)
       {
            List<int> pathIndexes = new List<int>();

            for (int i = 0; i < pathLists.Count; i++)
            {
                if (pathLists[i].Contains(wallCells[0]) || pathLists[i].Contains(wallCells[1]))
                {
                    pathIndexes.Add(i);
                }
            }
            List<int> newPath = new List<int>();
            newPath.AddRange(wallCells);
            for (int j = 0; j < pathIndexes.Count; j++)
            {
                newPath.AddRange(pathLists[pathIndexes[j]-j]);
                pathLists.RemoveAt(pathIndexes[j]-j);
            }
            pathLists.Add(newPath);

            return pathLists;
       }
    }
}
