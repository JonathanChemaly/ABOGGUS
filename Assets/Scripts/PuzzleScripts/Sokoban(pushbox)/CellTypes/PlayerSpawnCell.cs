using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class PlayerSpawnCell : SokobanCell
    {
        public override List<SokobanCell> AdjacentList => adjacencyList;

        public override bool isFloor()
        {
            return true;
        }

        private List<SokobanCell> adjacencyList;

        public PlayerSpawnCell(FloorCell fc)
        {
            adjacencyList = fc.AdjacentList;
        }
    }
}
