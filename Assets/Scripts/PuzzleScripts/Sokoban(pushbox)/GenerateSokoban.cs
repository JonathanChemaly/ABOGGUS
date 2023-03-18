using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{   
    public class GenerateSokoban : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Count of number of templates you want to place vertically")]
        private int gridHeight = 3;

        [SerializeField]
        [Tooltip("Count of number of templates you want to place horizontally")]
        private int gridWidth = 3;

        [SerializeField]
        [Tooltip("Size of Rooms to generate. Current valid numbers: 3")]
        private int roomSize = 3;

        [SerializeField]
        [Tooltip("Number of boxes to generate")]
        private int boxNumbers = 2;

        [HideInInspector]
        public SokobanCell[,] sokoban;

        private void Start()
        {
            //TO-DO
            //Generate Walls
            sokoban = SokobanHelper.GenerateSokoban(gridHeight, gridWidth, roomSize);
            SokobanHelper.DebugPrintSokoban(sokoban); //output after generation

            //Generate Goals
            sokoban = SokobanGoals.GenerateSokobanGoals(sokoban, boxNumbers);
            SokobanHelper.DebugPrintSokoban(sokoban); //output after goal generation
            //Output everything
            Out3D();
        }

        private GameObject Floor;

        private const float floorScaleConst = 0.1f;

        [HideInInspector]
        public const float cellScaleConst = 0.5f;

        [SerializeField]
        [Tooltip("Material to apply to the floor")]
        private Material floorMaterial = null;

        [SerializeField]
        [Tooltip("Prefab that is top be used as the wall")]
        private GameObject wallPrefab = null;

        [SerializeField]
        [Tooltip("Prefab that is top be used as the goals")]
        private GameObject goalPrefab = null;

        [SerializeField]
        [Tooltip("Prefab that is top be used as the box")]
        private GameObject boxPrefab = null;

        [SerializeField]
        [Tooltip("Prefab that is top be used as the player")]
        private GameObject playerPrefab = null;

        private float curXFloorScale, curYFloorScale;
        [HideInInspector]
        public float xOffset, yOffset;

        private void Out3D()
        {
            int sokobanRows = sokoban.GetLength(0);
            int sokobanCols = sokoban.GetLength(1);


            xOffset = sokobanRows / 2 * cellScaleConst;// - sokobanRows%2 * 0.25f;
            yOffset = sokobanCols / 2 * cellScaleConst;// - sokobanCols%2 * 0.25f;

            if (sokobanRows % 2 == 0)
            {
                xOffset -= cellScaleConst / 2;
            }
            if (sokobanCols%2 == 0)
            {
                yOffset -= cellScaleConst / 2;
            }

            /*
             * Print Out floor 
             */

            //Creates Floor
            Floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
            //float ratioOfRowsToCols = sokobanRows / sokobanCols;
            curXFloorScale = (float)sokobanRows / 2 * floorScaleConst;
            curYFloorScale = (float)sokobanCols / 2 * floorScaleConst;

            Floor.transform.localScale = new Vector3(curXFloorScale, 1, curYFloorScale);
            Floor.transform.localPosition = new Vector3(0, 0, 0);
            Floor.isStatic = true;

            //Rescale material to tile properly
            Material fm = floorMaterial;
            fm.mainTextureScale = new Vector2(sokobanRows, sokobanCols);
            Floor.GetComponent<Renderer>().material = fm;

            Floor.transform.parent = transform;

            /*
             * Print Out SokobanCells
             */

            //gets size of cells based on floor size
            
            //goes through all rows
            for (int row = 0; row < sokobanRows; row++)
            {
                //goes through all cols
                for (int col = 0; col < sokobanCols; col++)
                {

                    SokobanCell cell = sokoban[row, col]; 
                    CreateCellObject(row, col, gameObject, cell);
                }
            }
        }

        private void CreateCellObject(int row, int col, GameObject parent, SokobanCell sc)
        {
            Type cellType = sc.GetType();
            //Go through all cell types that have corresponding game objects
            if (cellType.Equals(typeof(WallCell)))
            {
                CreateWallObject(row, col, parent);
            } 
            else if (cellType.Equals(typeof(GoalCell)))
            {
                CreateGoalObject(row, col, parent);
            } 
            else if (cellType.Equals(typeof(BoxCell)))
            {
                CreateBoxObject(row, col, parent);
            }
            else if (cellType.Equals(typeof(PlayerSpawnCell)))
            {
                CreatePlayerObject(row, col, parent);
            }
            
        }

        private void CreateWallObject(int row, int col, GameObject parent)
        {
            GameObject wall = Instantiate(wallPrefab);
            wall.transform.parent = parent.transform;
            wall.transform.localScale = new Vector3(cellScaleConst, cellScaleConst, cellScaleConst);
            wall.transform.localPosition = new Vector3(row * cellScaleConst - xOffset, cellScaleConst/2, col * cellScaleConst - yOffset);

            wall.isStatic = true;
            BoxCollider bc = wall.AddComponent<BoxCollider>();
            bc.size = new Vector3(cellScaleConst, cellScaleConst, cellScaleConst); ;
        }

        [HideInInspector]
        public GameObject playerObject;
        [HideInInspector]
        public System.Tuple<int, int> startingPlayerPostion;

        private void CreatePlayerObject(int row, int col, GameObject parent)
        {
            GameObject player = Instantiate(playerPrefab);
            player.transform.parent = parent.transform;
            player.transform.localScale = new Vector3(cellScaleConst, cellScaleConst, cellScaleConst);
            player.transform.localPosition = new Vector3(row * cellScaleConst - xOffset, cellScaleConst / 2, col * cellScaleConst - yOffset);

            startingPlayerPostion = new System.Tuple<int, int>(row, col);
            playerObject = player;
        }


        [HideInInspector]
        public List<System.Tuple<int, int>> goalLocations = new();

        private void CreateGoalObject(int row, int col, GameObject parent)
        {
            GameObject goal = Instantiate(goalPrefab);
            goal.transform.parent = parent.transform;
            goal.transform.localScale = new Vector3(cellScaleConst, cellScaleConst, cellScaleConst);
            goal.transform.localPosition = new Vector3(row * cellScaleConst - xOffset, cellScaleConst / 2, col * cellScaleConst - yOffset);

            goal.isStatic = true;
            goalLocations.Add(new System.Tuple<int, int>(row, col));
        }

        [HideInInspector]
        public List<System.Tuple<int, int, GameObject>> startingBoxLocations = new();

        private void CreateBoxObject(int row, int col, GameObject parent)
        {
            GameObject box = Instantiate(boxPrefab);
            box.transform.parent = parent.transform;
            box.transform.localScale = new Vector3(cellScaleConst, cellScaleConst, cellScaleConst);
            box.transform.localPosition = new Vector3(row * cellScaleConst - xOffset, cellScaleConst / 2, col * cellScaleConst - yOffset);

            startingBoxLocations.Add(new System.Tuple<int, int, GameObject>(row, col, box));
        }
    }
}

