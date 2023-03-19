using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.Interact;

namespace ABOGGUS.Interact.Puzzles
{
    public class TilePuzzle : MonoBehaviour
    {
        [SerializeField] private Interactable item;
        private TilePuzzleManager tpm;
        public Vector2 pos;
        public int orderNum;

        // Start is called before the first frame update
        void Awake()
        {
            item.InteractAction += Interact;
        }

        private void Interact()
        {
            tpm.TileInteract(this);
            //Debug.Log("Clicked on this tile");
        }

        public int GetIntendedOrder()
        {
            return orderNum;
        }
        public void SetTPM(TilePuzzleManager tpm, int x, int y, int order)
        {
            this.tpm = tpm;
            pos = new Vector2(x, y);
            this.orderNum = order;
        }

        public void SetPosition(Vector2 newPos)
        {
            pos = newPos;
            this.transform.position = new Vector3(TilePuzzleManager.TILE_PUZZLE_SPACE*newPos.x, 1, TilePuzzleManager.TILE_PUZZLE_SPACE*newPos.y);
        }
    }
}
