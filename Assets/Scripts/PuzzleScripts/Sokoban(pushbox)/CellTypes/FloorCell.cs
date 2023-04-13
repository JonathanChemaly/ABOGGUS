using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class FloorCell : SokobanCell
    {
        public override List<SokobanCell> AdjacentList => adjacencyList;

        public override bool IsFloor()
        {
            return true;
        }

        public override bool PlayerIsHere { get => playerIsHere; set => playerIsHere = value; }

        private bool playerIsHere = false;

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

