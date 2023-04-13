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
        private float height;

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

        public void SetTPM(TilePuzzleManager tpm, int x, int y, int order, float height)
        {
            this.tpm = tpm;
            pos = new Vector2(x, y);
            this.orderNum = order;
            this.height = height;
        }

        public void MoveTile(Vector2 newPos)
        {
            StartCoroutine(MoveTileOverTime(newPos));
        }

        IEnumerator MoveTileOverTime(Vector2 newPos)
        {
            TilePuzzleManager.movingTile = true;
            Vector3 targetPos = GetNewPosition(newPos);
            while (transform.localPosition != targetPos)
            {
                yield return null;
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, moveSpeed);
            }
            SetPosition(newPos);
            TilePuzzleManager.movingTile = false;
        }

        public void SetPosition(Vector2 newPos)
        {
            pos = newPos;
            this.transform.localPosition = GetNewPosition(newPos);
            UpdateIndicator();
        }

        private Vector3 GetNewPosition(Vector2 newPos)
        {
            return new Vector3(tpm.localTopLeft.x + tpm.tileSpace * newPos.x, tpm.localTopLeft.y + height - 0.6f, tpm.localTopLeft.z + tpm.tileSpace * -1 * newPos.y);
        }

        private void UpdateIndicator()
        {
            // change color (material) of border indicator
            if (orderNum == tpm.size * pos.y + pos.x) mr.material = correct;
            else mr.material = incorrect;
        }

        public void ApplyTextureFromOrder(Texture2D texture)
        {
            // create sprite
            int rectLength = texture.width / tpm.size;
            int xStart = orderNum % tpm.size, yStart = (tpm.size-1) - orderNum / tpm.size;
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
