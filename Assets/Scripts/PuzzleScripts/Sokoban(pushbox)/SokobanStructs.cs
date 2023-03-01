using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public static class SokobanStructs
    {
        public enum SCells
        {
            Wall,
            Floor,
            NoBoxFloor, //Floor that a box can not go into
            Empty,
            Box,
            Goal, //Goal where box needs to be pushed
            PlayerSpawn
        }
    }

}
