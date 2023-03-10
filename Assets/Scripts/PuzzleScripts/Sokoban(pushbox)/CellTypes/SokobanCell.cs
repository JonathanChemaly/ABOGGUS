using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ABOGGUS.Interact.Puzzles.Sokoban.SokobanStructs;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public abstract class SokobanCell
    {
        public abstract List<SokobanCell> AdjacentList { get; }

        public abstract bool isFloor();

    }
}

