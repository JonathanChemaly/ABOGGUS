using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles
{
    public class TilePuzzleManager : MonoBehaviour
    {
        public int dimensionLength = 4;
        public float tileSpace = 2.5f;
        [SerializeField] GameObject tilePrefab;
        [SerializeField] Texture2D fullTexture;

        private TilePuzzle[,] tiles;
        private Vector2 emptyPos;

        public static bool gameOver;
        public static bool movingTile;

        private GameObject wall;
        public const float wallMoveSpeed = 0.001f;
        public const float wallStop = 6.5f;

        // Start is called before the first frame update
        void Awake()
        {
            CreatePuzzle();
            gameOver = false;
            movingTile = false;
            wall = transform.Find("Door").Find("Wall").gameObject;
        }

        private void CreatePuzzle()
        {
            // sets up list to determine random order
            List<int> numList = new List<int>();
            int num = 0;
            while (num < dimensionLength * dimensionLength - 1)
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
            if (dimensionLength % 2 == 0)
            {
                if (inversions % 2 == 0) emptyPos = new Vector2(dimensionLength - 1, dimensionLength - 1);
                else emptyPos = new Vector2(dimensionLength - 1, 0);
            }
            else emptyPos = new Vector2(dimensionLength - 1, dimensionLength - 1);

            int index = 0;
            tiles = new TilePuzzle[dimensionLength, dimensionLength];
            for (int j = 0; j < dimensionLength; j++)
            {
                for (int i = 0; i < dimensionLength; i++)
                {
                    // check not empty tile
                    if (i != emptyPos.x || j != emptyPos.y)
                    {
                        // create prefab instance
                        GameObject newObj = Instantiate(tilePrefab, Vector3.one, Quaternion.identity);
                        newObj.transform.parent = this.transform;
                        TilePuzzle newTile = newObj.GetComponent<TilePuzzle>();

                        // set piece of picture (order) from randomly shuffled list
                        int order = numList[index];
                        index++;

                        // set Tile object
                        newTile.SetTPM(this, i, j, order);
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
            return dimensionLength % 2 == 0 || inversions % 2 == 0;
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
            for (int j = 0; j < dimensionLength; j++)
            {
                for (int i = 0; i < dimensionLength; i++)
                {
                    checkCnt++;
                    if (i == emptyPos.x && j == emptyPos.y) continue;
                    if (checkCnt-1 != tiles[i, j].orderNum) return;
                }
            }

            gameOver = true;
            Debug.Log("Player has solved the puzzle!");
            MoveWall();
        }

        private void MoveWall()
        {
            StartCoroutine(MoveWallUpOverTime());
        }

        IEnumerator MoveWallUpOverTime()
        {
            while (wall.transform.position.y < wallStop)
            {
                yield return null;
                wall.transform.position = Vector3.MoveTowards(wall.transform.position, wall.transform.position + Vector3.up, wallMoveSpeed);
            }
        }

        private string ListToString(List<int> list)
        {
            string str = "" + list[0];
            for (int i = 1; i < list.Count; i++) str += ", " + list[i];
            return str;
        }

    }
}