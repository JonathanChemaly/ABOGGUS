using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ABOGGUS.Interact.Puzzles.Sokoban.SokobanStructs;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public static class SokobanTemplates
    {
        /*
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
        */
        
        /*
            Five by five templates
         */

        public static List<List<SCells>> template0 = new List<List<SCells>>()
        {
            new List<SCells>(){SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty },
            new List<SCells>(){SCells.Empty, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty },
            new List<SCells>(){SCells.Empty, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty },
            new List<SCells>(){SCells.Empty, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty },
            new List<SCells>(){SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty },
        };

        public static List<List<SCells>> template1 = new List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
        };

        public static List<List<SCells>> template2 = new List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Floor, SCells.Floor,
            SCells.Empty, SCells.Wall, SCells.Wall, SCells.Floor, SCells.Floor,
            SCells.Empty, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
        };

        public static List<List<SCells>> template3 = new List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Wall, SCells.Wall, SCells.Empty,
            SCells.Empty, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
        };

        public static List<List<SCells>> template4 = new List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Wall, SCells.Wall, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
        };

        public static List<List<SCells>> template5 = new List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Floor, SCells.Empty, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Floor, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Floor, SCells.Floor, SCells.Wall, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
        };

        public static List<List<SCells>> template6 = new List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Floor, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
        };

        public static List<List<SCells>> template7 = new List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Floor, SCells.Empty, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Floor, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Wall, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Floor, SCells.Empty, SCells.Empty,
        };

        public static List<List<SCells>> template8 = List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Floor, SCells.Empty, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Wall, SCells.Empty,
            SCells.Floor, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Floor,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Wall, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Floor, SCells.Empty, SCells.Empty,
        };
    }
}
