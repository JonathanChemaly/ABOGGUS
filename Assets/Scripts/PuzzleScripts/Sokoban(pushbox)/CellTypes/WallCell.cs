using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class WallCell : SokobanCell
    {
        public override List<SokobanCell> AdjacentList => throw new System.NotImplementedException();

        public override bool PlayerIsHere { get => playerIsHere; set => playerIsHere = value; }

        private bool playerIsHere = false;

        public override bool IsFloor()
        {
            return false;
        }

        public WallCell()
        {

        }
    }
}
