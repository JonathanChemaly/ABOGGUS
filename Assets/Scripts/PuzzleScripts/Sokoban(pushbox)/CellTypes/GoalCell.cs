using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class GoalCell : SokobanCell
    {
        public override List<SokobanCell> AdjacentList => adjacencyList;

        public override bool PlayerIsHere { get => playerIsHere; set => playerIsHere = value; }

        private bool playerIsHere = false;

        public override bool IsFloor()
        {
            return true;
        }

        

        private List<SokobanCell> adjacencyList;

        //Takes existing floor cell and makes it a goal cell
        public GoalCell(SokobanCell fc)
        {
            adjacencyList = fc.AdjacentList;
        }

        public GoalCell()
        {
            
        }
    }
}

