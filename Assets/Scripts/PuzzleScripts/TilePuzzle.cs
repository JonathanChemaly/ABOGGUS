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

        [SerializeField] Material incorrect;
        [SerializeField] Material correct;
        private MeshRenderer mr;

        public const float moveSpeed = 0.05f;

        // Start is called before the first frame update
        void Awake()
        {
            item.InteractAction += Interact;
            mr = transform.Find("Indicator").GetComponent<MeshRenderer>();
        }

        public void SetTPM(TilePuzzleManager tpm, int x, int y, int order)
        {
            this.tpm = tpm;
            pos = new Vector2(x, y);
            this.orderNum = order;
        }

        public void MoveTile(Vector2 newPos)
        {
            StartCoroutine(MoveTileOverTime(newPos));
        }

        IEnumerator MoveTileOverTime(Vector2 newPos)
        {
            TilePuzzleManager.movingTile = true;
            Vector3 targetPos = new Vector3(tpm.tileSpace * newPos.x, 1, tpm.tileSpace * -1 * newPos.y);
            while (transform.position != targetPos)
            {
                yield return null;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed);
            }
            SetPosition(newPos);
            TilePuzzleManager.movingTile = false;
        }

        public void SetPosition(Vector2 newPos)
        {
            pos = newPos;
            this.transform.position = new Vector3(tpm.tileSpace * newPos.x, 1, tpm.tileSpace * -1 * newPos.y);
            UpdateIndicator();
        }

        private void UpdateIndicator()
        {
            // change color (material) of border indicator
            if (orderNum == tpm.dimensionLength * pos.y + pos.x) mr.material = correct;
            else mr.material = incorrect;
        }

        public void ApplyTextureFromOrder(Texture2D texture)
        {
            // create sprite
            int rectLength = texture.width / tpm.dimensionLength;
            int xStart = orderNum % tpm.dimensionLength, yStart = (tpm.dimensionLength-1) - orderNum / tpm.dimensionLength;
            Rect rect = new Rect(rectLength * xStart, rectLength * yStart, rectLength, rectLength);
            Sprite sprite = Sprite.Create(texture, rect, Vector2.one * 0.5f);

            // apply to Sprite child of tile
            GameObject spriteObj = transform.Find("Sprite").gameObject;
            spriteObj.transform.localScale /= (rectLength / 100.0f);
            spriteObj.GetComponent<SpriteRenderer>().sprite = sprite;
        }

        public bool InteractCheck()
        {
            return tpm.CheckTileCanMove(this) && !TilePuzzleManager.gameOver && !TilePuzzleManager.movingTile;
        }

        private void Interact()
        {
            tpm.MoveTile(this);
            //Debug.Log("Clicked on this tile");
        }
    }
}
