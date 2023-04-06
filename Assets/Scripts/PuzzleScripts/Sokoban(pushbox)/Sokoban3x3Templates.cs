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

        /**
         * checks all cardinal directions for a floor cell and if there is one puts it into the adjaceny list
         * for the specficed cell
         */
        private static void AddNeighorsToList(SokobanCell[,] room, SokobanCell sc, int row, int col)
        {
            if (row -1 > 0 && room[row -1, col].IsFloor()) sc.AdjacentList.Add(room[row -1, col]); //north
            if (row + 1 < room.GetLength(0) && room[row +1, col].IsFloor()) sc.AdjacentList.Add(room[row+1, col]); //south
            if (col + 1 < room.GetLength(1) && room[row, col +1].IsFloor()) sc.AdjacentList.Add(room[row, col+1]); //east
            if (col - 1 > 0 && room[row, col -1].IsFloor()) sc.AdjacentList.Add(room[row, col-1]); //west
        }

        //Template Propeties
        public static SokobanCell[,] Template0 { get => MakeTemplate0(); }
        public static SokobanCell[,] Template1 { get => MakeTemplate1(); }
        public static SokobanCell[,] Template2 { get => MakeTemplate2(); }
        public static SokobanCell[,] Template3 { get => MakeTemplate3(); }
        public static SokobanCell[,] Template4 { get => MakeTemplate4(); }
        public static SokobanCell[,] Template5 { get => MakeTemplate5(); }
        public static SokobanCell[,] Template6 { get => MakeTemplate6(); }
        public static SokobanCell[,] Template7 { get => MakeTemplate7(); }
        public static SokobanCell[,] Template8 { get => MakeTemplate8(); }

        /*
            Five by five templates
         */

        //All Floor room
        private static SokobanCell[,] MakeTemplate0()
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
            AddNeighorsToList(room, room[1, 1], 1, 1);
            AddNeighorsToList(room, room[1, 2], 1, 2);
            AddNeighorsToList(room, room[1, 3], 1, 3);

            //second row
            AddNeighorsToList(room, room[2, 1], 2, 1);
            AddNeighorsToList(room, room[2, 2], 2, 2);
            AddNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            AddNeighorsToList(room, room[3, 1], 3, 1);
            AddNeighorsToList(room, room[3, 2], 3, 2);
            AddNeighorsToList(room, room[3, 3], 3, 3);

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

        private static SokobanCell[,] MakeTemplate1()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new WallCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //first row
            AddNeighorsToList(room, room[1, 2], 1, 2);
            AddNeighorsToList(room, room[1, 3], 1, 3);

            //second row
            AddNeighorsToList(room, room[2, 1], 2, 1);
            AddNeighorsToList(room, room[2, 2], 2, 2);
            AddNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            AddNeighorsToList(room, room[3, 1], 3, 1);
            AddNeighorsToList(room, room[3, 2], 3, 2);
            AddNeighorsToList(room, room[3, 3], 3, 3);

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
        private static SokobanCell[,] MakeTemplate2()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new FloorCell(), new FloorCell() },
            { new EmptyCell(), new WallCell(), new WallCell(), new FloorCell(), new FloorCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //first row
            AddNeighorsToList(room, room[1, 3], 1, 3);

            //second row
            AddNeighorsToList(room, room[2, 1], 2, 1);
            AddNeighorsToList(room, room[2, 2], 2, 2);
            AddNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            AddNeighorsToList(room, room[3, 1], 3, 1);
            AddNeighorsToList(room, room[3, 2], 3, 2);
            AddNeighorsToList(room, room[3, 3], 3, 3);

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
        private static SokobanCell[,] MakeTemplate3()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new WallCell(), new WallCell(), new WallCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //second row
            AddNeighorsToList(room, room[2, 1], 2, 1);
            AddNeighorsToList(room, room[2, 2], 2, 2);
            AddNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            AddNeighorsToList(room, room[3, 1], 3, 1);
            AddNeighorsToList(room, room[3, 2], 3, 2);
            AddNeighorsToList(room, room[3, 3], 3, 3);

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
        private static SokobanCell[,] MakeTemplate4()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new WallCell(), new WallCell(), new WallCell(), new EmptyCell() },
            { new EmptyCell(), new WallCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new WallCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //second row
            AddNeighorsToList(room, room[2, 2], 2, 2);
            AddNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            AddNeighorsToList(room, room[3, 2], 3, 2);
            AddNeighorsToList(room, room[3, 3], 3, 3);

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
        private static SokobanCell[,] MakeTemplate5()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new FloorCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new WallCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new FloorCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new FloorCell(), new FloorCell(), new WallCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //first row
            AddNeighorsToList(room, room[1, 2], 1, 2);
            AddNeighorsToList(room, room[1, 3], 1, 3);

            //second row
            AddNeighorsToList(room, room[2, 1], 2, 1);
            AddNeighorsToList(room, room[2, 2], 2, 2);
            AddNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            AddNeighorsToList(room, room[3, 1], 3, 1);
            AddNeighorsToList(room, room[3, 2], 3, 2);

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
        private static SokobanCell[,] MakeTemplate6()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new WallCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new FloorCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new WallCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //first row
            AddNeighorsToList(room, room[1, 2], 1, 2);
            AddNeighorsToList(room, room[1, 3], 1, 3);

            //second row
            AddNeighorsToList(room, room[2, 1], 2, 1);
            AddNeighorsToList(room, room[2, 2], 2, 2);
            AddNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            AddNeighorsToList(room, room[3, 2], 3, 2);
            AddNeighorsToList(room, room[3, 3], 3, 3);

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
        private static SokobanCell[,] MakeTemplate7()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new FloorCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new WallCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new FloorCell(), new FloorCell(), new FloorCell(), new FloorCell(), new EmptyCell() },
            { new EmptyCell(), new WallCell(), new FloorCell(), new WallCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new FloorCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //first row
            AddNeighorsToList(room, room[1, 2], 1, 2);
            AddNeighorsToList(room, room[1, 3], 1, 3);

            //second row
            AddNeighorsToList(room, room[2, 1], 2, 1);
            AddNeighorsToList(room, room[2, 2], 2, 2);
            AddNeighorsToList(room, room[2, 3], 2, 3);

            //third row

            AddNeighorsToList(room, room[3, 2], 3, 2);


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
        private static SokobanCell[,] MakeTemplate8()
        {
            SokobanCell[,] room = new SokobanCell[,] {

            { new EmptyCell(), new EmptyCell(), new FloorCell(), new EmptyCell(), new EmptyCell() },
            { new EmptyCell(), new WallCell(), new FloorCell(), new WallCell(), new EmptyCell() },
            { new FloorCell(), new FloorCell(), new FloorCell(), new FloorCell(), new FloorCell() },
            { new EmptyCell(), new WallCell(), new FloorCell(), new WallCell(), new EmptyCell() },
            { new EmptyCell(), new EmptyCell(), new FloorCell(), new EmptyCell(), new EmptyCell() }

            };

            //set up adjaceny lists of ..

            //first row
            AddNeighorsToList(room, room[1, 2], 1, 2);

            //second row
            AddNeighorsToList(room, room[2, 1], 2, 1);
            AddNeighorsToList(room, room[2, 2], 2, 2);
            AddNeighorsToList(room, room[2, 3], 2, 3);

            //third row
            AddNeighorsToList(room, room[3, 2], 3, 2);

            return room;
        }
    }
}
