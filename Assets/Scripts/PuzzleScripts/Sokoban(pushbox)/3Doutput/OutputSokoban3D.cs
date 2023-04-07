using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class OutputSokoban3D : MonoBehaviour
    {
        [HideInInspector]
        public SokobanCell[,] sokoban;

        [HideInInspector]
        public bool LevelGenerated { get => finishedLevelGeneration; }

        private bool finishedLevelGeneration = false;
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

        private void Start()
        {
            StartCoroutine(WaitForGeneratedLevel());         
        }

        IEnumerator WaitForGeneratedLevel()
        {
            //loop until a level is generated
            //should be imediately true on the interact, but just in case we do this
            while (SokobanStatics.generatedSokoban == null)
            {
                yield return null;
            }
            //set our sokoban
            sokoban = (SokobanCell[,])SokobanStatics.generatedSokoban.Clone();
            Out3D();
        }

        //Method to rest the level in case the player messed up and got puzzle into un reachable postion
        public void ResetLevel()
        {
            finishedLevelGeneration = false;
            sokoban = (SokobanCell[,])SokobanStatics.generatedSokoban.Clone();
            DestroyRecursive(this.gameObject);//destorys all we built on last run
            //Clear all variables passed to movement controller
            startingPlayerPostion = null;
            goalLocations = new();
            startingBoxLocations = new();
            playerObject = null;

            Out3D();
        }

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
            if (sokobanCols % 2 == 0)
            {
                yOffset -= cellScaleConst / 2;
            }

            /*
             * Print Out floor 
             */

            //Creates Floor
            Floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
            //float ratioOfRowsToCols = sokobanRows / sokobanCols;
            Floor.transform.eulerAngles = new Vector3(-90, 0, 0);
            Floor.transform.localPosition = this.transform.position;
            Floor.transform.parent = transform;

            curXFloorScale = ((float)sokobanRows / 2f) * floorScaleConst;
            curYFloorScale = ((float)sokobanCols / 2f) * floorScaleConst;
            
            Floor.transform.localScale = new Vector3(curYFloorScale, 1, curXFloorScale);
            
            Floor.isStatic = true;

            //Rescale material to tile properly
            Material fm = floorMaterial;
            fm.mainTextureScale = new Vector2(sokobanCols, sokobanRows);
            Floor.GetComponent<Renderer>().material = fm;

            

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

            //Clears the boxes so that we now have floors where to boxes were
            //to allow for pushing through those cells in movement controller
            ClearBoxesFromSokoban(sokobanRows, sokobanCols);

            SetGameLayerRecursive(this.gameObject, UILayerNum);

            finishedLevelGeneration = true;
        }

        private const int UILayerNum = 5;

        //Code from unity formum to set the layer of
        private void SetGameLayerRecursive(GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.layer = layer;

                Transform _HasChildren = child.GetComponentInChildren<Transform>();
                if (_HasChildren != null)
                    SetGameLayerRecursive(child.gameObject, layer);

            }
        }

        private void DestroyRecursive(GameObject gameObject)
        {
            foreach (Transform child in gameObject.transform)
            {
                Transform HasChildren = child.GetComponentInChildren<Transform>();
                if (HasChildren != null) 
                {
                    DestroyRecursive(child.gameObject); 
                }
                Destroy(child.gameObject);
            }
        }

        private void ClearBoxesFromSokoban(int sokobanRows, int sokobanCols)
        {
            for (int row = 0; row < sokobanRows; row++)
            {
                //goes through all cols
                for (int col = 0; col < sokobanCols; col++)
                {
                    SokobanCell cell = sokoban[row, col];
                    if (cell.GetType().Equals(typeof(BoxCell)))
                    {
                        sokoban[row, col] = new FloorCell();
                    }
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
            wall.transform.localPosition = new Vector3(row * cellScaleConst - xOffset, cellScaleConst / 2, col * cellScaleConst - yOffset);

            wall.layer = UILayerNum;
            wall.transform.eulerAngles = new Vector3(-90, 0, 0);

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

            player.layer = UILayerNum;
            player.transform.eulerAngles = new Vector3(-90, 0, 0);

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

            goal.layer = UILayerNum;
            goal.transform.eulerAngles = new Vector3(-90, 0, 0);

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

            box.layer = UILayerNum;
            box.transform.eulerAngles = new Vector3(-90, 0, 0);

            startingBoxLocations.Add(new System.Tuple<int, int, GameObject>(row, col, box));
        }
    }
}

