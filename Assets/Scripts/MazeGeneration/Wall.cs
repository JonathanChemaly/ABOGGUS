using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.MazeGeneration
{
    public class Wall
    {
        private GameObject wall;
        private List<int> cells;
        private float wallWidth = MazeController.DEFAULT_WIDTH_WALLS;
        private float standardPos = MazeController.DEFAULT_HEIGHT_WALLS;

        public Wall(bool NSWall, int rowPosition, int columnPosition, int rowTotal, int columnTotal, Transform parent, GameObject wallPrefab)
        {
            if (NSWall)
            {
                ZTransform(rowPosition, columnPosition, wallPrefab, parent);
                cells = CellGenerator.GenerateNSCells(rowPosition, columnPosition, rowTotal, columnTotal);
            }
            else
            {
                XTransform(rowPosition, columnPosition, wallPrefab, parent);
                cells = CellGenerator.GenerateEWCells(rowPosition, columnPosition, columnTotal);
            }
            //wall.transform.parent = parent;
        }

        private void XTransform(int row, int column, GameObject wallPrefab, Transform parent)
        {
            wall = Object.Instantiate(wallPrefab, parent, false);
            wall.transform.localPosition = new Vector3((wallWidth / 2) + column, standardPos, standardPos + row);
            wall.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }

        private void ZTransform(int row, int column, GameObject wallPrefab, Transform parent)
        {
            wall = Object.Instantiate(wallPrefab, parent, false);
            wall.transform.localPosition = new Vector3(standardPos + column, standardPos, (wallWidth / 2) + row);
            wall.transform.localRotation = Quaternion.identity;
        }

        public void DestroyWall()
        {
            MonoBehaviour.Destroy(wall);
        }
        
        public List<int> GetCells()
        {
            return cells;
        }
    }
}
