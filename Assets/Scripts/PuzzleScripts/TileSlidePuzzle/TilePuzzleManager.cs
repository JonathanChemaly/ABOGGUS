using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;

namespace ABOGGUS.Interact.Puzzles
{
    public class TilePuzzleManager : MonoBehaviour
    {
        public int size = 3;
        public float tileSpace = 2.5f;
        [SerializeField] GameObject tilePrefab;
        [SerializeField] Texture2D fullTexture;
        [SerializeField] float tileHeight;
        [SerializeField] float tileColliderScale;
        [SerializeField] float rotation;
        [SerializeField] Material matBorder;
        [SerializeField] GameObject wall;
        [SerializeField] LargeEEDrop manaDrop;

        [System.NonSerialized] public Vector3 center;
        [System.NonSerialized] public Vector3 topLeft;
        [System.NonSerialized] public Vector3 localTopLeft;

        private TilePuzzle[,] tiles;
        private Vector2 emptyPos;
        private float puzzleLength;
        private float betweenSpace;

        public static bool gameOver;
        public static bool movingTile;

        public const float wallMoveSpeed = 0.4f;
        public const float wallDist = 6f;

        // Start is called before the first frame update
        public void LoadPuzzle()
        {
            puzzleLength = size * tileSpace;
            betweenSpace = tileSpace - 2.0f;
            center = this.transform.position;
            topLeft = center + new Vector3(-1 * (size - 1) * tileSpace / 2, 0, (size - 1) * tileSpace / 2);
            localTopLeft = this.transform.InverseTransformPoint(topLeft);
            //Debug.Log("Top Left: " + topLeft);
            //Debug.Log("Local Top Left: " + localTopLeft);

            CreatePuzzle();
            CreateBorders();
            CreateInvisFloor();
            this.transform.rotation = Quaternion.Euler(0, rotation, 0);

            gameOver = false;
            movingTile = false;
            manaDrop.eventOnPickup += PuzzleComplete;
        }

        private void CreatePuzzle()
        {
            // sets up list to determine random order
            List<int> numList = new List<int>();
            int num = 0;
            while (num < size * size - 1)
            {
                numList.Add(num);
                num++;
            }

            // shuffle list and make sure it can be solved
            int inversions = 0;
            do
            {
                Shuffle(numList);
                //Debug.Log("Shuffled list: " + ListToString(numList));
                inversions = CountInversions(numList);
            } while (!IsSolvable(inversions));
            if (size % 2 == 0)
            {
                if (inversions % 2 == 0) emptyPos = new Vector2(size - 1, size - 1);
                else emptyPos = new Vector2(size - 1, 0);
            }
            else emptyPos = new Vector2(size - 1, size - 1);

            int index = 0;
            tiles = new TilePuzzle[size, size];
            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    // check not empty tile
                    if (i != emptyPos.x || j != emptyPos.y)
                    {
                        // create prefab instance
                        GameObject newObj = Instantiate(tilePrefab, Vector3.one, Quaternion.identity);
                        newObj.GetComponent<BoxCollider>().size = new Vector3(1, tileColliderScale, 1);
                        //newObj.transform.RotateAround(center, Vector3.up, rotation);
                        newObj.transform.parent = this.transform;
                        TilePuzzle newTile = newObj.GetComponent<TilePuzzle>();

                        // set piece of picture (order) from randomly shuffled list
                        int order = numList[index];
                        index++;

                        // set Tile object
                        newTile.SetTPM(this, i, j, order, tileHeight);
                        newTile.SetPosition(new Vector2(i, j));
                        newTile.ApplyTextureFromOrder(fullTexture);
                        tiles[i, j] = newTile;
                    }
                }
            }
        }

        // Fisher-Yates shuffle algorithm
        private void Shuffle(List<int> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = (int)(Random.value * (n + 1));
                if (k == n+1) k = n;
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // count number of inversions, will help determine whether it can be solved
        private int CountInversions(List<int> list)
        {
            int count = 0;
            for (int i = 0; i < list.Count; i++)
            {
                int tileCount = 0;
                for (int j = i; j < list.Count; j++)
                {
                    if (list[j] < list[i]) tileCount++;
                }
                count += tileCount;
            }
            //Debug.Log("Inversion count: " + count);
            return count;
        }

        // if even then depends on empty space, if odd then inversions must be even
        private bool IsSolvable(int inversions)
        {
            return size % 2 == 0 || inversions % 2 == 0;
        }

        public bool CheckTileCanMove(TilePuzzle tile)
        {
            // check if next to empty tile
            Vector2 dist = tile.pos - emptyPos;
            return dist.magnitude == 1;
        }

        public void MoveTile(TilePuzzle tile)
        {
            // swap position with empty tile
            Vector2 temp = emptyPos;
            emptyPos = tile.pos;
            tiles[(int)temp.x, (int)temp.y] = tiles[(int)tile.pos.x, (int)tile.pos.y];
            tiles[(int)tile.pos.x, (int)tile.pos.y] = null;
            tile.MoveTile(temp);
            CheckWinningOrder();
        }

        private void CheckWinningOrder()
        {
            int checkCnt = 0;
            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    checkCnt++;
                    if (i == emptyPos.x && j == emptyPos.y) continue;
                    if (checkCnt-1 != tiles[i, j].orderNum) return;
                }
            }

            gameOver = true;
            //Debug.Log("Player has solved the slide puzzle!");
            if (!GameConstants.puzzleStatus["TileSlidePuzzle"])
            {
                manaDrop.transform.parent.gameObject.SetActive(true);
            }
            GameConstants.puzzleStatus["TileSlidePuzzle"] = true;
            MoveWall();
        }

        private void MoveWall()
        {
            StartCoroutine(MoveWallUpOverTime());
        }

        IEnumerator MoveWallUpOverTime()
        {
            float targetY = wall.transform.position.y + wallDist;
            while (wall.transform.position.y < targetY)
            {
                yield return null;
                wall.transform.position = Vector3.MoveTowards(wall.transform.position, wall.transform.position + Vector3.up, Time.deltaTime*wallMoveSpeed);
            }
        }

        public void PuzzleComplete()
        {
            GameConstants.puzzleStatus["TileSlidePuzzle"] = true;
            size++;
        }

        private string ListToString(List<int> list)
        {
            string str = "" + list[0];
            for (int i = 1; i < list.Count; i++) str += ", " + list[i];
            return str;
        }


        private void CreateBorders()
        {
            GameObject border1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            border1.name = "LeftBorder";
            border1.transform.localScale = new Vector3(1, tileColliderScale, puzzleLength + 2*betweenSpace);
            border1.transform.position = center + new Vector3(-1 * puzzleLength / 2 - betweenSpace - 0.5f, tileHeight - 0.6f, 0);
            //border1.transform.RotateAround(center, Vector3.up, rotation);
            border1.transform.parent = this.transform;
            border1.GetComponent<MeshRenderer>().material = matBorder;

            GameObject border2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            border2.name = "RightBorder";
            border2.transform.localScale = new Vector3(1, tileColliderScale, puzzleLength + 2*betweenSpace);
            border2.transform.position = center + new Vector3(puzzleLength / 2 + betweenSpace + 0.5f, tileHeight - 0.6f, 0);
            //border2.transform.RotateAround(center, Vector3.up, rotation);
            border2.transform.parent = this.transform;
            border2.GetComponent<MeshRenderer>().material = matBorder;

            GameObject border3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            border3.name = "TopBorder";
            border3.transform.localScale = new Vector3(puzzleLength + 2*betweenSpace + 2, tileColliderScale, 1);
            border3.transform.position = center + new Vector3(0, tileHeight - 0.6f, puzzleLength / 2 + betweenSpace + 0.5f);
            //border3.transform.RotateAround(center, Vector3.up, rotation);
            border3.transform.parent = this.transform;
            border3.GetComponent<MeshRenderer>().material = matBorder;

            GameObject border4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            border4.name = "BottomBorder";
            border4.transform.localScale = new Vector3(puzzleLength + 2*betweenSpace + 2, tileColliderScale, 1);
            border4.transform.position = center + new Vector3(0, tileHeight - 0.6f, -1 * puzzleLength / 2 - betweenSpace - 0.5f);
            //border4.transform.RotateAround(center, Vector3.up, rotation);
            border4.transform.parent = this.transform;
            border4.GetComponent<MeshRenderer>().material = matBorder;
        }

        private void CreateInvisFloor()
        {
            GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.name = "InvisFloor";
            floor.transform.localScale = new Vector3(puzzleLength, tileColliderScale, puzzleLength);
            floor.transform.position = center + new Vector3(0, tileHeight - 0.6f, 0);
            //floor.transform.RotateAround(center, Vector3.up, rotation);
            floor.transform.parent = this.transform;
            floor.GetComponent<MeshRenderer>().enabled = false;
        }

    }
}