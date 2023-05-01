using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;

namespace ABOGGUS.Interact
{
    public class OnSuccessClear : MonoBehaviour
    {
        [SerializeField] string puzzleName;

        [SerializeField]
        [Tooltip("interact to watch for success")]
        private Interactable interactToWatch;

        private void Start()
        {
            interactToWatch.SuccessAction += ClearPuzzle;
        }

        private void ClearPuzzle()
        {
            //Debug.Log("puzzlecomplete");
            GameConstants.puzzleStatus[puzzleName] = true;
        }
    }
}

