using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class FloorCell : SokobanCell
    {
        public override List<SokobanCell> AdjacentList => adjacencyList;

        public override bool isFloor()
        {
            return true;
        }

        private List<SokobanCell> adjacencyList;

        public FloorCell()
        {
            adjacencyList = new List<SokobanCell>();
        }

        public FloorCell(SokobanCell sc)
        {
            adjacencyList = sc.AdjacentList;
        }
    }
}

