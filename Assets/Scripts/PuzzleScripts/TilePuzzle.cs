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
            this.transform.position = new Vector3(tpm.tileSpace*newPos.x, 1, tpm.tileSpace*newPos.y);
        }

        public void ApplyTextureFromOrder(Texture2D texture)
        {
            // create sprite
            int rectLength = texture.width / tpm.numTilesLong;
            int xStart = orderNum % tpm.numTilesLong, yStart = orderNum / tpm.numTilesLong;
            Rect rect = new Rect(rectLength * xStart, rectLength * yStart, rectLength, rectLength);
            Sprite sprite = Sprite.Create(texture, rect, Vector2.one * 0.5f);

            // apply to Sprite child of tile
            GameObject spriteObj = transform.Find("Sprite").gameObject;
            spriteObj.transform.localScale /= (rectLength / 100.0f);
            spriteObj.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }
}
