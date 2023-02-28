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
                nonTraversed.Add((i, j));
            }
        }

        
        RoomNode startRoom = new RoomNode(RoomNode.WALL, RoomNode.DOOR, RoomNode.WALL, RoomNode.WALL, RoomNode.MAIN_ROOM);
        RoomArr[2, 0] = startRoom;
        nonTraversed.Remove((2, 0));
        traversed.Add((2, 0));

        // Maze Generation Algo (Prim's)
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
                GameObject room;
                Vector3 pos = new Vector3(i * 29.5f, 0, j * 29.5f);

                int rand = Random.Range(0, mainRooms.Length);

                room = GameObject.Instantiate(mainRooms[rand], pos, Quaternion.identity);
                
                

                SetDoorType(RoomArr[i, j].GetDoor(RoomNode.NORTH), room.transform.Find("Walls/NorthWall/DungeonLayer1Door"));
                SetDoorType(RoomArr[i, j].GetDoor(RoomNode.SOUTH), room.transform.Find("Walls/SouthWall/DungeonLayer1Door"));
                SetDoorType(RoomArr[i, j].GetDoor(RoomNode.EAST), room.transform.Find("Walls/EastWall/DungeonLayer1Door"));
                SetDoorType(RoomArr[i, j].GetDoor(RoomNode.WEST), room.transform.Find("Walls/WestWall/DungeonLayer1Door"));
            }
        }
    }

    private void SetDoorType(int doorType, Transform door) {
        foreach (Transform trans in door)
        {
            trans.gameObject.SetActive(false);
        }

        switch (doorType)
        {
            case RoomNode.DOOR:
                door.Find("LeftArch").gameObject.SetActive(true);
                door.Find("RightArch").gameObject.SetActive(true);
                break;
            case RoomNode.WALL:
                door.Find("Wall").gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    private void ConnectRooms((int, int) room1, (int, int)room2)
    {
       if(room1.Item1 == room2.Item1)
       {
            if(room1.Item2 < room2.Item2)
            {
                RoomArr[room1.Item1, room1.Item2].SetDoor(RoomNode.DOOR, RoomNode.NORTH);
                RoomArr[room2.Item1, room2.Item2].SetDoor(RoomNode.DOOR, RoomNode.SOUTH);
            }
            else if(room1.Item2 > room2.Item2)
            {
                RoomArr[room1.Item1, room1.Item2].SetDoor(RoomNode.DOOR, RoomNode.SOUTH);
                RoomArr[room2.Item1, room2.Item2].SetDoor(RoomNode.DOOR, RoomNode.NORTH);
            }
       } else if(room1.Item2 == room2.Item2)
        {
            if (room1.Item1 < room2.Item1)
            {
                RoomArr[room1.Item1, room1.Item2].SetDoor(RoomNode.DOOR, RoomNode.EAST);
                RoomArr[room2.Item1, room2.Item2].SetDoor(RoomNode.DOOR, RoomNode.WEST);
            }
            else if (room1.Item1 > room2.Item1)
            {
                RoomArr[room1.Item1, room1.Item2].SetDoor(RoomNode.DOOR, RoomNode.WEST);
                RoomArr[room2.Item1, room2.Item2].SetDoor(RoomNode.DOOR, RoomNode.EAST);
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


    


}

