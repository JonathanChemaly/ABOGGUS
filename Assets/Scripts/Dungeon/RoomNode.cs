using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode : MonoBehaviour
{
    public const int WALL = 0;
    public const int DOOR = 1;
    public const int VINE = 2;
    public const int ELEVATOR = 3;

    public const int MAIN_ROOM = 0;
    public const int OPT_ROOM = 1;


    public const int NORTH = 0;
    public const int SOUTH = 1;
    public const int EAST = 2;
    public const int WEST = 3;

    private int northDoor;
    private int southDoor;
    private int eastDoor;
    private int westDoor;
    private int roomType;
    public RoomNode(int northDoor, int southDoor, int eastDoor, int westDoor, int roomType) {
        this.northDoor = northDoor;
        this.southDoor = southDoor;
        this.eastDoor = eastDoor;
        this.westDoor = westDoor;
        this.roomType = roomType;
    }

    public RoomNode()
    {
        northDoor = WALL;
        southDoor = WALL;
        eastDoor = WALL;
        westDoor = WALL;
        roomType = MAIN_ROOM;
    }

    public int GetDoor(int dir)
    {
        switch(dir)
        {
            case NORTH:
                return northDoor;
            case SOUTH:
                return southDoor;
            case EAST:
                return eastDoor;
            case WEST:
                return westDoor;
            default:
                return 97;
        }
    }

    public void SetDoor(int doorType, int dir)
    {
        switch (dir)
        {
            case NORTH:
                northDoor = doorType;
                break;
            case SOUTH:
                southDoor = doorType;
                break;
            case EAST:
                eastDoor = doorType;
                break;
            case WEST:
                westDoor = doorType;
                break;
            default:
                Debug.LogError("Cannot set door");
                break;
        }
    }

    public int GetRoomType()
    {
        return roomType;
    }

    public IEnumerable GetWalls()
    {
        for(int i = 0; i < 4; i ++)
        {
            if (GetDoor(i) == WALL) yield return i;
        }
    }
}
