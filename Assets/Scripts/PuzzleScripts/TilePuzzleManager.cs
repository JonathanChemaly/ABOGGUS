using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles
{
    public class TilePuzzleManager : MonoBehaviour
    {
        public int numTilesLong = 4;
        public float tileSpace = 2.5f;
        public GameObject tilePrefab;
        [SerializeField] int emptyIndex;
        [SerializeField] Vector2 emptyPos;
        [SerializeField] Texture2D fullTexture;

        private TilePuzzle[,] tiles;
        //private int orderCnt;
        private bool gameOver;

        // Start is called before the first frame update
        void Awake()
        {
            CreatePuzzle();
            gameOver = false;
        }

        private void CreatePuzzle()
        {
            // sets up list to determine order
            List<int> numList = new List<int>();
            int num = 0;
            while (num < numTilesLong * numTilesLong)
            {
                if (num != emptyIndex)
                {
                    numList.Add(num);
                }
                num++;
            }

            tiles = new TilePuzzle[numTilesLong, numTilesLong];
            //orderCnt = 0;
            for (int i = 0; i < numTilesLong; i++)
            {
                for (int j = 0; j < numTilesLong; j++)
                {
                    // check not empty
                    if (i != emptyPos.x || j != emptyPos.y)
                    {
                        // create prefab instance
                        GameObject newObj = Instantiate(tilePrefab, new Vector3(tileSpace * i, 1, tileSpace * j), Quaternion.identity);
                        newObj.transform.parent = this.transform;
                        TilePuzzle newTile = newObj.GetComponent<TilePuzzle>();

                        // randomly set which piece of picture
                        int index = Random.Range(0, numList.Count);
                        int order = numList[index];
                        numList.RemoveAt(index);

                        // set Tile object
                        newTile.SetTPM(this, i, j, order);
                        newTile.ApplyTextureFromOrder(fullTexture);
                        tiles[i, j] = newTile;
                    }
                    //orderCnt++;
                }
            }
        }

        public void TileInteract(TilePuzzle tile)
        {
            // check if next to empty tile
            Vector2 dist = tile.pos - emptyPos;
            if (dist.magnitude == 1)
            {
                // swap tiles
                Vector2 temp = emptyPos;
                emptyPos = tile.pos;
                tiles[(int)temp.x, (int)temp.y] = tiles[(int)tile.pos.x, (int)tile.pos.y];
                tiles[(int)tile.pos.x, (int)tile.pos.y] = null;
                tile.SetPosition(temp);
                CheckWinningOrder();
            }
        }

        private void CheckWinningOrder()
        {
            int checkCnt = 0;
            for (int i = 0; i < numTilesLong; i++)
            {
                for (int j = 0; j < numTilesLong; j++)
                {
                    checkCnt++;
                    if (i == emptyPos.x && j == emptyPos.y) continue;
                    if (checkCnt-1 != tiles[i, j].orderNum) return;
                }
            }

            gameOver = true;
            Debug.Log("Player has solved the puzzle!");
        }
    }
}