using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class PlayerSpawnCell : SokobanCell
    {
        public override List<SokobanCell> AdjacentList => adjacencyList;

        public override bool PlayerIsHere { get => playerIsHere; set => playerIsHere = value; }

        private bool playerIsHere = true;

        public override bool IsFloor()
        {
            return true;
        }

        private List<SokobanCell> adjacencyList;

        public PlayerSpawnCell(SokobanCell fc)
        {
            adjacencyList = fc.AdjacentList;
            playerIsHere = true;
        }

        public PlayerSpawnCell()
        {

        }
    }
}