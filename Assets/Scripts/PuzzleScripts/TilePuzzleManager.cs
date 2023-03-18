using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles
{
    public class TilePuzzleManager : MonoBehaviour
    {
        public int TILE_PUZZLE_LENGTH = 4;
        public const int TILE_PUZZLE_SPACE = 3;
        public GameObject tilePrefab;
        [SerializeField] Vector2 emptyPos;

        private TilePuzzle[,] tiles;
        private int orderCnt;
        private bool gameOver;

        // Start is called before the first frame update
        void Awake()
        {
            CreatePuzzle();
        }

        private void CreatePuzzle()
        {
            tiles = new TilePuzzle[TILE_PUZZLE_LENGTH, TILE_PUZZLE_LENGTH];
            orderCnt = 0;

            for (int i = 0; i < TILE_PUZZLE_LENGTH; i++)
            {
                for (int j = 0; j < TILE_PUZZLE_LENGTH; j++)
                {
                    // check not empty
                    if (i != emptyPos.x || j != emptyPos.y)
                    {
                        GameObject newObj = Instantiate(tilePrefab, new Vector3(TILE_PUZZLE_SPACE * i, 1, TILE_PUZZLE_SPACE * j), Quaternion.identity);
                        newObj.transform.parent = this.transform;
                        TilePuzzle newTile = newObj.GetComponent<TilePuzzle>();
                        newTile.SetTPM(this, i, j, orderCnt);
                        tiles[i, j] = newTile;
                    }
                    orderCnt++;
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
                tile.SetPosition(temp);
                //CheckWinningOrder();
            }
        }

        private void CheckWinningOrder()
        {
            int checkCnt = 0;
            for (int i = 0; i < TILE_PUZZLE_LENGTH; i++)
            {
                for (int j = 0; j < TILE_PUZZLE_LENGTH; j++)
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