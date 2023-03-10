using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class GoalCell : SokobanCell
    {
        public override List<SokobanCell> AdjacentList => adjacencyList;

        public override bool isFloor()
        {
            return true;
        }

        private List<SokobanCell> adjacencyList;

        //Takes existing floor cell and makes it a goal cell
        public GoalCell(FloorCell fc)
        {
            adjacencyList = fc.AdjacentList;
        }
    }
}

