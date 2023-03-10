using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ABOGGUS.Interact.Puzzles.Sokoban.SokobanStructs;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    /**
     * Class to hold all the room templates for rooms with 3x3 interiors or 5x5 total space
     * 
     */
    public static class Sokoban3x3Templates
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

        //TODO: Fix out of bounds property
        /**
         * checks all cardinal directions for a floor cell and if there is one puts it into the adjaceny list
         * for the specficed cell
         */
        private static void addNeighorsToList(SokobanCell[,] room, SokobanCell sc, int row, int col)
        {
            if (room[row -1, col].isFloor()) sc.AdjacentList.Add(room[row, col]); //north
            if (room[row +1, col].isFloor()) sc.AdjacentList.Add(room[row, col]); //south
            if (room[row, col +1].isFloor()) sc.AdjacentList.Add(room[row, col]); //east
            if (room[row, col -1].isFloor()) sc.AdjacentList.Add(room[row, col]); //west
        }

        //Template Propeties
        public static SokobanCell[,] Template0 { get => makeTemplate0(); }
        public static SokobanCell[,] Template1 { get => makeTemplate1(); }
        public static SokobanCell[,] Template2 { get => makeTemplate2(); }
        public static SokobanCell[,] Template3 { get => makeTemplate3(); }
        public static SokobanCell[,] Template4 { get => makeTemplate4(); }
        public static SokobanCell[,] Template5 { get => makeTemplate5(); }
        public static SokobanCell[,] Template6 { get => makeTemplate6(); }
        public static SokobanCell[,] Template7 { get => makeTemplate7(); }
        public static SokobanCell[,] Template8 { get => makeTemplate8(); }

        /*
            Five by five templates
         */

        //All Floor room
        private static SokobanCell[,] makeTemplate0()
        {
            SokobanCell[,] room = new SokobanCell[,] {
            
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() }
            
            };

            //set up adjaceny lists of ..

            //first row
            addNeighorsToList(room, room[1, 1], 1, 1);
            addNeighorsToList(room, room[1, 2], 1, 2);
            addNeighorsToList(room, room[1, 3], 1, 3);

            //second row
            addNeighorsToList(room, room[2, 1], 2, 1);
            addNeighorsToList(room, room[2, 2], 2, 2);
            addNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            addNeighorsToList(room, room[3, 1], 3, 1);
            addNeighorsToList(room, room[3, 2], 3, 2);
            addNeighorsToList(room, room[3, 3], 3, 3);

            return room;
        }

        /*
        public static List<List<SCells>> template1 = new List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
        };
        */

        private static SokobanCell[,] makeTemplate1()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //first row
            addNeighorsToList(room, room[1, 1], 1, 1);
            addNeighorsToList(room, room[1, 2], 1, 2);
            addNeighorsToList(room, room[1, 3], 1, 3);

            //second row
            addNeighorsToList(room, room[2, 1], 2, 1);
            addNeighorsToList(room, room[2, 2], 2, 2);
            addNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            addNeighorsToList(room, room[3, 1], 3, 1);
            addNeighorsToList(room, room[3, 2], 3, 2);
            addNeighorsToList(room, room[3, 3], 3, 3);

            return room;
        }

        /*
        public static List<List<SCells>> template2 = new List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Floor, SCells.Floor,
            SCells.Empty, SCells.Wall, SCells.Wall, SCells.Floor, SCells.Floor,
            SCells.Empty, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
        };
        */

        //TODO: Implement
        private static SokobanCell[,] makeTemplate2()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //first row
            addNeighorsToList(room, room[1, 1], 1, 1);
            addNeighorsToList(room, room[1, 2], 1, 2);
            addNeighorsToList(room, room[1, 3], 1, 3);

            //second row
            addNeighorsToList(room, room[2, 1], 2, 1);
            addNeighorsToList(room, room[2, 2], 2, 2);
            addNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            addNeighorsToList(room, room[3, 1], 3, 1);
            addNeighorsToList(room, room[3, 2], 3, 2);
            addNeighorsToList(room, room[3, 3], 3, 3);

            return room;
        }
        /*
        public static List<List<SCells>> template3 = new List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Wall, SCells.Wall, SCells.Empty,
            SCells.Empty, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
        };
        */
        //TODO: Implement
        private static SokobanCell[,] makeTemplate3()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //first row
            addNeighorsToList(room, room[1, 1], 1, 1);
            addNeighorsToList(room, room[1, 2], 1, 2);
            addNeighorsToList(room, room[1, 3], 1, 3);

            //second row
            addNeighorsToList(room, room[2, 1], 2, 1);
            addNeighorsToList(room, room[2, 2], 2, 2);
            addNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            addNeighorsToList(room, room[3, 1], 3, 1);
            addNeighorsToList(room, room[3, 2], 3, 2);
            addNeighorsToList(room, room[3, 3], 3, 3);

            return room;
        }
        /*
        public static List<List<SCells>> template4 = new List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Wall, SCells.Wall, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
        };
        */
        //TODO: Implement
        private static SokobanCell[,] makeTemplate4()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //first row
            addNeighorsToList(room, room[1, 1], 1, 1);
            addNeighorsToList(room, room[1, 2], 1, 2);
            addNeighorsToList(room, room[1, 3], 1, 3);

            //second row
            addNeighorsToList(room, room[2, 1], 2, 1);
            addNeighorsToList(room, room[2, 2], 2, 2);
            addNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            addNeighorsToList(room, room[3, 1], 3, 1);
            addNeighorsToList(room, room[3, 2], 3, 2);
            addNeighorsToList(room, room[3, 3], 3, 3);

            return room;
        }
        /*
        public static List<List<SCells>> template5 = new List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Floor, SCells.Empty, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Floor, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Floor, SCells.Floor, SCells.Wall, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
        };
        */
        //TODO: Implement
        private static SokobanCell[,] makeTemplate5()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //first row
            addNeighorsToList(room, room[1, 1], 1, 1);
            addNeighorsToList(room, room[1, 2], 1, 2);
            addNeighorsToList(room, room[1, 3], 1, 3);

            //second row
            addNeighorsToList(room, room[2, 1], 2, 1);
            addNeighorsToList(room, room[2, 2], 2, 2);
            addNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            addNeighorsToList(room, room[3, 1], 3, 1);
            addNeighorsToList(room, room[3, 2], 3, 2);
            addNeighorsToList(room, room[3, 3], 3, 3);

            return room;
        }
        /*
        public static List<List<SCells>> template6 = new List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Floor, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty, SCells.Empty,
        };
        */
        //TODO: Implement
        private static SokobanCell[,] makeTemplate6()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //first row
            addNeighorsToList(room, room[1, 1], 1, 1);
            addNeighorsToList(room, room[1, 2], 1, 2);
            addNeighorsToList(room, room[1, 3], 1, 3);

            //second row
            addNeighorsToList(room, room[2, 1], 2, 1);
            addNeighorsToList(room, room[2, 2], 2, 2);
            addNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            addNeighorsToList(room, room[3, 1], 3, 1);
            addNeighorsToList(room, room[3, 2], 3, 2);
            addNeighorsToList(room, room[3, 3], 3, 3);

            return room;
        }
        /*
        public static List<List<SCells>> template7 = new List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Floor, SCells.Empty, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Floor, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Wall, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Floor, SCells.Empty, SCells.Empty,
        };
        */
        //TODO: Implement
        private static SokobanCell[,] makeTemplate7()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //first row
            addNeighorsToList(room, room[1, 1], 1, 1);
            addNeighorsToList(room, room[1, 2], 1, 2);
            addNeighorsToList(room, room[1, 3], 1, 3);

            //second row
            addNeighorsToList(room, room[2, 1], 2, 1);
            addNeighorsToList(room, room[2, 2], 2, 2);
            addNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            addNeighorsToList(room, room[3, 1], 3, 1);
            addNeighorsToList(room, room[3, 2], 3, 2);
            addNeighorsToList(room, room[3, 3], 3, 3);

            return room;
        }
        /*
        public static List<List<SCells>> template8 = List<List<SCells>>()
        {
            SCells.Empty, SCells.Empty, SCells.Floor, SCells.Empty, SCells.Empty,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Wall, SCells.Empty,
            SCells.Floor, SCells.Floor, SCells.Floor, SCells.Floor, SCells.Floor,
            SCells.Empty, SCells.Wall, SCells.Floor, SCells.Wall, SCells.Empty,
            SCells.Empty, SCells.Empty, SCells.Floor, SCells.Empty, SCells.Empty,
        };
        */
        //TODO: Implement
        private static SokobanCell[,] makeTemplate8()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //first row
            addNeighorsToList(room, room[1, 1], 1, 1);
            addNeighorsToList(room, room[1, 2], 1, 2);
            addNeighorsToList(room, room[1, 3], 1, 3);

            //second row
            addNeighorsToList(room, room[2, 1], 2, 1);
            addNeighorsToList(room, room[2, 2], 2, 2);
            addNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            addNeighorsToList(room, room[3, 1], 3, 1);
            addNeighorsToList(room, room[3, 2], 3, 2);
            addNeighorsToList(room, room[3, 3], 3, 3);

            return room;
        }
    }
}
