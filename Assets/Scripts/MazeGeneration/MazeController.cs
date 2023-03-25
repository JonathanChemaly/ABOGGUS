using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.MazeGeneration
{
    public class MazeController : MonoBehaviour
    {
        public const int DEFAULT_SCALE = 3;
        public const int DEFAULT_POSITION = 15 * DEFAULT_SCALE;
        public const float DEFAULT_HEIGHT_WALLS = 0.25f;
        public const float DEFAULT_HEIGHT_COLLECTABLES = 2f;
        public const float DEFAULT_WIDTH_WALLS = 0.02f;
        public const float SHIFT = 3.5f;
        public const int ROWS = 10;
        public const int COLUMNS = 10;
        public const int LENGTH_OF_MAZE = ROWS * DEFAULT_SCALE;
        public const int WIDTH_OF_MAZE = COLUMNS * DEFAULT_SCALE;

        public const int OFFSETX = 0;
        public const int OFFSETY = 0;
        public const int OFFSETZ = 0;

        [SerializeField] public GameObject mazes;
        private GameObject mazeParent1;
        private GameObject mazeParent2;
        private GameObject mazeParent3;
        private GameObject mazeParent4;

        [SerializeField] public GameObject collectables;
        private GameObject collectable1;
        private GameObject collectable2;
        private GameObject collectable3;
        private GameObject collectable4;

        //[SerializeField] public GameObject floorPrefab;
        [SerializeField] public GameObject wallPrefab;

        private int seed1;
        private int seed2;
        private int seed3;
        private int seed4;

        private System.Random random;
        // Start is called before the first frame update
        void Start()
        {
            mazeParent1 = mazes.transform.GetChild(0).gameObject;
            random = new System.Random();
            seed1 = random.Next();
            seed2 = random.Next();
            seed3 = random.Next();
            seed4 = random.Next();

            AssignVariables();
            InitializeMazes();
            InitializeCollectables();
            //ApplyOffset();
        }

        private void AssignVariables()
        {
            mazeParent1 = mazes.transform.GetChild(0).gameObject;
            mazeParent2 = mazes.transform.GetChild(1).gameObject;
            mazeParent3 = mazes.transform.GetChild(2).gameObject;
            mazeParent4 = mazes.transform.GetChild(3).gameObject;

            collectable1 = collectables.transform.GetChild(0).gameObject;
            collectable2 = collectables.transform.GetChild(1).gameObject;
            collectable3 = collectables.transform.GetChild(2).gameObject;
            collectable4 = collectables.transform.GetChild(3).gameObject;
        }

        private void InitializeMazes()
        {
            MazeGenerator.instance.SetPrefabs(wallPrefab);
            MazeGenerator.instance.SetValues(mazeParent1, seed1, 0);
            MazeGenerator.instance.GenerateMaze();
            MazeGenerator.instance.SetValues(mazeParent2, seed2, 1);
            MazeGenerator.instance.GenerateMaze();
            MazeGenerator.instance.SetValues(mazeParent3, seed3, 2);
            MazeGenerator.instance.GenerateMaze();
            MazeGenerator.instance.SetValues(mazeParent4, seed4, 3);
            MazeGenerator.instance.GenerateMaze();

            mazeParent1.transform.localScale = new Vector3(DEFAULT_SCALE, DEFAULT_SCALE, DEFAULT_SCALE);
            mazeParent1.transform.localPosition = new Vector3(DEFAULT_POSITION / 2 - LENGTH_OF_MAZE / 2 - SHIFT,
                0, DEFAULT_POSITION / 2 - WIDTH_OF_MAZE / 2 - SHIFT);
            mazeParent2.transform.localScale = new Vector3(DEFAULT_SCALE, DEFAULT_SCALE, DEFAULT_SCALE);
            mazeParent2.transform.localPosition = new Vector3(DEFAULT_POSITION / 2 - LENGTH_OF_MAZE / 2 - SHIFT,
                0, -(DEFAULT_POSITION - WIDTH_OF_MAZE / 2) - SHIFT);
            mazeParent3.transform.localScale = new Vector3(DEFAULT_SCALE, DEFAULT_SCALE, DEFAULT_SCALE);
            mazeParent3.transform.localPosition = new Vector3(-(DEFAULT_POSITION - LENGTH_OF_MAZE / 2) - SHIFT,
                0, -(DEFAULT_POSITION - WIDTH_OF_MAZE / 2) - SHIFT);
            mazeParent4.transform.localScale = new Vector3(DEFAULT_SCALE, DEFAULT_SCALE, DEFAULT_SCALE);
            mazeParent4.transform.localPosition = new Vector3(-(DEFAULT_POSITION - LENGTH_OF_MAZE / 2) - SHIFT,
                0, DEFAULT_POSITION / 2 - WIDTH_OF_MAZE / 2 - SHIFT);
        }

        private void InitializeCollectables()
        {
            collectable1.transform.localPosition = new Vector3(LENGTH_OF_MAZE + mazeParent1.transform.localPosition.x - 1,
                DEFAULT_HEIGHT_COLLECTABLES, WIDTH_OF_MAZE + mazeParent1.transform.localPosition.z - 1);
            collectable2.transform.localPosition = new Vector3((LENGTH_OF_MAZE + mazeParent2.transform.localPosition.x - 1),
                DEFAULT_HEIGHT_COLLECTABLES, mazeParent2.transform.localPosition.z + 1);
            collectable3.transform.localPosition = new Vector3(mazeParent3.transform.localPosition.x + 1,
                DEFAULT_HEIGHT_COLLECTABLES, mazeParent3.transform.localPosition.z + 1);
            collectable4.transform.localPosition = new Vector3(mazeParent4.transform.localPosition.x + 1,
                DEFAULT_HEIGHT_COLLECTABLES, WIDTH_OF_MAZE + mazeParent1.transform.localPosition.z - 1);
        }

        private void ApplyOffset()
        {
            Vector3 offset = new Vector3(OFFSETX, OFFSETY, OFFSETZ);

            mazeParent1.transform.position += offset;
            mazeParent2.transform.position += offset;
            mazeParent3.transform.position += offset;
            mazeParent4.transform.position += offset;

            collectable1.transform.position += offset;
            collectable2.transform.position += offset;
            collectable3.transform.position += offset;
            collectable4.transform.position += offset;
        }
    }
}