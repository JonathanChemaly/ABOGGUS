using ABOGGUS.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] public int num;

    bool activated = false;
    bool colliding = false;

    private void Start()
    {
        FirePuzzle.NextHalf += ResetForHalf;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstants.NAME_PLAYERGAMEOBJECT))
        {
            if (colliding) return;
            colliding = true;
            Activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameConstants.NAME_PLAYERGAMEOBJECT))
        {
            colliding = false;
        }
    }

    private void Activate()
    {
        if (FirePuzzle.Instance.fireActive)
        {
            if (activated)
            {
                //FirePuzzle.Reset();
            }
            else
            {
                activated = true;
                FirePuzzle.Instance.ActivatePiece(num);
            }
        }
    }

    private void ResetForHalf()
    {
        if (FirePuzzle.secondLine.Contains(num))
        {
            this.activated = false;
        }
    }
}
