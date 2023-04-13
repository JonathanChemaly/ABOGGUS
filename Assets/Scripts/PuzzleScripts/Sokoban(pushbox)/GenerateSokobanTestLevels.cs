using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public static class GenerateSokobanTestLevels
    {
        public static SokobanCell[,] TwoByTwoTestLevel1= {
            
            { new WallCell(), new FloorCell(), new FloorCell(), new FloorCell(), new FloorCell() , new FloorCell() },
            { new WallCell(), new FloorCell(), new FloorCell(), new FloorCell(), new FloorCell() , new FloorCell() },
            { new WallCell(), new FloorCell(), new FloorCell(), new WallCell(), new WallCell() , new WallCell() },
            { new WallCell(), new FloorCell(), new FloorCell(), new FloorCell(), new FloorCell() , new FloorCell() },
            { new WallCell(), new FloorCell(), new FloorCell(), new FloorCell(), new FloorCell() , new FloorCell() },
            { new WallCell(), new WallCell(), new WallCell(), new FloorCell(), new FloorCell() , new FloorCell() },

            };

        public static SokobanCell[,] TwoByTwoTestLevel1WithGoal = {

            { new WallCell(), new FloorCell(), new FloorCell(), new FloorCell(), new FloorCell() , new FloorCell() },
            { new WallCell(), new FloorCell(), new FloorCell(), new FloorCell(), new FloorCell() , new FloorCell() },
            { new WallCell(), new FloorCell(), new FloorCell(), new WallCell(), new WallCell() , new WallCell() },
            { new WallCell(), new FloorCell(), new FloorCell(), new FloorCell(), new FloorCell() , new FloorCell() },
            { new WallCell(), new FloorCell(), new FloorCell(), new FloorCell(), new FloorCell() , new GoalCell() },
            { new WallCell(), new WallCell(), new WallCell(), new FloorCell(), new FloorCell() , new FloorCell() },

            };

        public static SokobanCell[,] TwoByTwoTestLevel2WithGoal = {

            { new WallCell(), new WallCell(), new WallCell(), new FloorCell(), new FloorCell() , new FloorCell() },
            { new WallCell(), new FloorCell(), new FloorCell(), new FloorCell(), new FloorCell() , new FloorCell() },
            { new WallCell(), new FloorCell(), new FloorCell(), new FloorCell(), new FloorCell() , new FloorCell() },
            { new WallCell(), new FloorCell(), new WallCell(), new WallCell(), new FloorCell() , new FloorCell() },
            { new FloorCell(), new FloorCell(), new FloorCell(), new WallCell(), new FloorCell() , new FloorCell() },
            { new GoalCell(), new FloorCell(), new FloorCell(), new WallCell(), new FloorCell() , new FloorCell() },

            };
    }

}

