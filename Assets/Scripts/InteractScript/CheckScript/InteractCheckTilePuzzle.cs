using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.Interact.Puzzles;
using ABOGGUS.Gameplay;

namespace ABOGGUS.Interact.Checks
{
    public class InteractCheckTilePuzzle : MonoBehaviour, InteractCheck
    {
        [SerializeField] 
        private TilePuzzle tile;

        [SerializeField]
        [Tooltip("what to display in case tile cannot be moved")]
        private string failureText1 = "Cannot slide";

        [SerializeField]
        [Tooltip("what to display in case game is already finished")]
        private string failureText2 = "Puzzle already completed";

        public bool DoCheck()
        {
            return tile.InteractCheck();
        }

        public string GetFailureText()
        {
            if (TilePuzzleManager.gameOver) return failureText2;
            else return failureText1;
        }
    }
}

