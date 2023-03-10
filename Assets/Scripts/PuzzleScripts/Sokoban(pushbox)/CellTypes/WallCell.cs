using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class WallCell : SokobanCell
    {
        public override List<SokobanCell> AdjacentList => throw new System.NotImplementedException();

        public override bool isFloor()
        {
            return false;
        }
    }
}
