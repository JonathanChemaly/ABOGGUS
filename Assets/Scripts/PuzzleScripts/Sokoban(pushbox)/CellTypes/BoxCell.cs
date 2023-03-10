using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class BoxCell : SokobanCell
{
        public override List<SokobanCell> AdjacentList => throw new System.NotImplementedException();

        public override bool isFloor()
        {
            return false;
        }


    }
}

