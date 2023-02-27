using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDungeon : MonoBehaviour
{
    public GameObject[] mainRooms;
    public GameObject[] optionalRooms;
    public int numOfRooms;

    private RoomNode[,] RoomArr;

    private bool generating = true;
    private int roomNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        RoomArr = GenerateFloor();
    }

    private RoomNode[,] GenerateFloor()
    {
        RoomNode[,] RoomArr = new RoomNode[5, 5];
        RoomNode startRoom = new RoomNode(RoomNode.UNLINKED_DOOR, RoomNode.ELEVATOR, RoomNode.WALL, RoomNode.WALL, RoomNode.MAIN_ROOM);
        RoomArr[2, 0] = startRoom;

        
        return RoomArr;

    }


    private void ExpandRoom(int row, int column)
    {
        RoomNode room = RoomArr[row, column];
        for (int i = 0; i < 4; i++)
        {
            if (room.GetDoor(i) == RoomNode.UNLINKED_DOOR)
            {
                // Get which direction the entrance door of the new room is
                int entrance = 0;
                switch (i)
                {
                    case RoomNode.NORTH:
                        entrance = RoomNode.SOUTH;
                        break;
                    case RoomNode.SOUTH:
                        entrance = RoomNode.NORTH;
                        break;
                    case RoomNode.EAST:
                        entrance = RoomNode.WEST;
                        break;
                    case RoomNode.WEST:
                        entrance = RoomNode.EAST;
                        break;
                }
                RoomNode newRoom = GenerateNewRoom(RoomNode.MAIN_ROOM, entrance);
            }
        }

    }

    private RoomNode GenerateNewRoom(int roomType, int entranceDoor)
    {
        return new RoomNode(0, 0, 0, 0, 0);
    }

}

/*
 * if (i == RoomNode.NORTH && row >= RoomArr.GetLength(0)) room.SetDoor(RoomNode.WALL, i);
                if (i == RoomNode.SOUTH && row <= 0) room.SetDoor(RoomNode.WALL, i);
                if (i == RoomNode.EAST && column >= RoomArr.GetLength(1)) room.SetDoor(RoomNode.WALL, i);
                if (i == RoomNode.WEST && column <= 0) room.SetDoor(RoomNode.WALL, i);
*/
