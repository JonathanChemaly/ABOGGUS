using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDungeon : MonoBehaviour
{
    public GameObject[] mainRooms;
    public GameObject[] optionalRooms;
    public int numOfRooms;

    private RoomNode[,] RoomArr;
    private List<(int, int)> traversed;
    private List<(int, int)> nonTraversed;

    private bool generating = true;
    private int roomNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        traversed = new List<(int, int)>();
        nonTraversed = new List<(int, int)>();
        RoomArr = GenerateFloor();
        BuildFloor();
    }

    private RoomNode[,] GenerateFloor()
    {
        RoomArr = new RoomNode[5, 5];
        foreach ((int, int) neighbor in GetNeighbors(4, 4))
        {
            Debug.Log(neighbor);
        }
        for (int i = 0; i < RoomArr.GetLength(0); i++)
        {
            for (int j = 0; j < RoomArr.GetLength(1); j++)
            {
                RoomArr[i, j] = new RoomNode();
            }
        }

        for (int i = 0; i < RoomArr.GetLength(0) -1; i++)
        {
            for (int j = 0; j < RoomArr.GetLength(1) - 1; j++)
            {
                nonTraversed.Add((i, j));
            }
        }

        
        RoomNode startRoom = new RoomNode(RoomNode.WALL, RoomNode.ELEVATOR, RoomNode.WALL, RoomNode.WALL, RoomNode.MAIN_ROOM);
        RoomArr[2, 0] = startRoom;
        nonTraversed.Remove((2, 0));
        traversed.Add((2, 0));

        while(nonTraversed.Count > 0)
        {
            int index = Random.Range(0, traversed.Count);
            (int, int) node = traversed[index];
            List<(int, int)> neighborList = new List<(int, int)>();
            foreach ((int, int) neighbor in GetNeighbors(node.Item1, node.Item2))
            {
                neighborList.Add(neighbor);
            }
            if(neighborList.Count > 0)
            {
                int rand = Random.Range(0, neighborList.Count);
                (int, int) newNode = neighborList[rand];
                //Debug.Log(newNode);
                if(!traversed.Contains(newNode))
                {
                    // create room
                    ConnectRooms(node, newNode);
                    nonTraversed.Remove(newNode);
                    traversed.Add(newNode);
                }
            }
        }

        
        return RoomArr;

    }

    private void BuildFloor() {
        for (int i = 0; i < RoomArr.GetLength(0); i++)
        {
            for (int j = 0; j < RoomArr.GetLength(1); j++)
            {
                int rand = Random.Range(0, mainRooms.Length);
                Vector3 pos = new Vector3(i * 29, 0, j * 29);
                GameObject room = GameObject.Instantiate(mainRooms[rand], pos, Quaternion.identity);

                SetDoorType(RoomArr[i, j].GetDoor(RoomNode.NORTH), room.transform.GetChild(0).GetChild(0));
                SetDoorType(RoomArr[i, j].GetDoor(RoomNode.SOUTH), room.transform.GetChild(0).GetChild(1));
                SetDoorType(RoomArr[i, j].GetDoor(RoomNode.EAST), room.transform.GetChild(0).GetChild(2));
                SetDoorType(RoomArr[i, j].GetDoor(RoomNode.WEST), room.transform.GetChild(0).GetChild(3));
            }
        }
    }

    private void SetDoorType(int doorType, Transform door) {
        switch(doorType)
        {
            case RoomNode.LINKED_DOOR:
                door.GetChild(0).GetChild(0).gameObject.SetActive(true);
                door.GetChild(0).GetChild(1).gameObject.SetActive(false);
                door.GetChild(0).GetChild(2).gameObject.SetActive(true);
                door.GetChild(0).GetChild(3).gameObject.SetActive(false);
                door.GetChild(0).GetChild(4).gameObject.SetActive(false);
                break;
            case RoomNode.WALL:
                door.GetChild(0).GetChild(0).gameObject.SetActive(false);
                door.GetChild(0).GetChild(1).gameObject.SetActive(false);
                door.GetChild(0).GetChild(2).gameObject.SetActive(false);
                door.GetChild(0).GetChild(3).gameObject.SetActive(true);
                door.GetChild(0).GetChild(4).gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    private void ConnectRooms((int, int) room1, (int, int)room2)
    {
        //Debug.Log("Connecting " + room1 + room2);
       if(room1.Item1 == room2.Item1)
       {
            if(room1.Item2 < room2.Item2)
            {
                RoomArr[room1.Item1, room1.Item2].SetDoor(RoomNode.LINKED_DOOR, RoomNode.NORTH);
                RoomArr[room2.Item1, room2.Item2].SetDoor(RoomNode.LINKED_DOOR, RoomNode.SOUTH);
            }
            else if(room1.Item2 > room2.Item2)
            {
                RoomArr[room1.Item1, room1.Item2].SetDoor(RoomNode.LINKED_DOOR, RoomNode.SOUTH);
                RoomArr[room2.Item1, room2.Item2].SetDoor(RoomNode.LINKED_DOOR, RoomNode.NORTH);
            }
       } else if(room1.Item2 == room2.Item2)
        {
            if (room1.Item1 < room2.Item1)
            {
                RoomArr[room1.Item1, room1.Item2].SetDoor(RoomNode.LINKED_DOOR, RoomNode.EAST);
                RoomArr[room2.Item1, room2.Item2].SetDoor(RoomNode.LINKED_DOOR, RoomNode.WEST);
            }
            else if (room1.Item1 > room2.Item1)
            {
                RoomArr[room1.Item1, room1.Item2].SetDoor(RoomNode.LINKED_DOOR, RoomNode.WEST);
                RoomArr[room2.Item1, room2.Item2].SetDoor(RoomNode.LINKED_DOOR, RoomNode.EAST);
            }
        }
    }

    IEnumerable GetNeighbors(int x, int y)
    {
        if (x > 0) yield return (x-1,y);
        if (x < RoomArr.GetLength(0) -1) yield return (x+1,y);
        if (y > 0) yield return (x, y-1);
        if (y < RoomArr.GetLength(1) - 1) yield return (x, y + 1);
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
        RoomNode newRoom =  new RoomNode(0, 0, 0, 0, 0);
        newRoom.SetDoor(RoomNode.LINKED_DOOR, entranceDoor);
        return new RoomNode(0, 0, 0, 0, 0);
    }


}

/*
 * if (i == RoomNode.NORTH && row >= RoomArr.GetLength(0)) room.SetDoor(RoomNode.WALL, i);
                if (i == RoomNode.SOUTH && row <= 0) room.SetDoor(RoomNode.WALL, i);
                if (i == RoomNode.EAST && column >= RoomArr.GetLength(1)) room.SetDoor(RoomNode.WALL, i);
                if (i == RoomNode.WEST && column <= 0) room.SetDoor(RoomNode.WALL, i);
*/
