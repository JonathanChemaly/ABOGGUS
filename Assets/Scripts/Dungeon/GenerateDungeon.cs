using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDungeon : MonoBehaviour
{
    public GameObject[] mainRooms;
    public GameObject[] optionalRooms;
    public GameObject hallway;
    public int height = 5;
    public int width = 5;
    public int loops = 2;

    private RoomNode[,] RoomArr;
    private List<(int, int)> traversed;
    private List<(int, int)> nonTraversed;
    private List<((int, int), (int, int), int)> connectedRooms;

    void Start()
    {
        traversed = new List<(int, int)>();
        nonTraversed = new List<(int, int)>();
        connectedRooms = new List<((int, int), (int, int), int)>();
        RoomArr = GenerateFloor();
        CreateLoops();
        BuildFloor();
        AddHallways();
    }

    private RoomNode[,] GenerateFloor()
    {
        RoomArr = new RoomNode[width, height];
        for (int i = 0; i < RoomArr.GetLength(0); i++)
        {
            for (int j = 0; j < RoomArr.GetLength(1); j++)
            {
                RoomArr[i, j] = new RoomNode();
                nonTraversed.Add((i, j));
            }
        }

        
        RoomNode startRoom = new RoomNode(RoomNode.WALL, RoomNode.DOOR, RoomNode.WALL, RoomNode.WALL, RoomNode.MAIN_ROOM);
        RoomArr[width/2, 0] = startRoom;
        nonTraversed.Remove((width/2, 0));
        traversed.Add((width/2, 0));

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
        RoomArr[width / 2, height-1].SetDoor(RoomNode.DOOR, RoomNode.NORTH);

        return RoomArr;

    }

    private void AddHallways()
    {
        foreach(((int, int), (int, int), int) connection in connectedRooms)
        {
            int dir = connection.Item3;
            bool rotate = false;
            Vector3 pos = new Vector3(connection.Item1.Item1 * 59f, 0, connection.Item1.Item2 * 59f);
            switch (dir)
            {
                case 0:
                    pos.z += 29.5f;
                    rotate = true;
                    break;
                case 1:
                    pos.z -= 29.5f;
                    rotate = true;
                    break;
                case 2:
                    pos.x += 29.5f;
                    break;
                case 3:
                    pos.x -= 29.5f;
                    break;
            }
            
            if (rotate) Instantiate(hallway, pos, Quaternion.identity);
            else Instantiate(hallway, pos, Quaternion.Euler(new Vector3(0, 90, 0)));

        }
    }

    private void BuildFloor() {
        for (int i = 0; i < RoomArr.GetLength(0); i++)
        {
            for (int j = 0; j < RoomArr.GetLength(1); j++)
            {
                GameObject room;
                Vector3 pos = new Vector3(i * 59f, 0, j * 59f);

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
        int dir = 0 ;
       if(room1.Item1 == room2.Item1)
       {
            if(room1.Item2 < room2.Item2)
            {
                RoomArr[room1.Item1, room1.Item2].SetDoor(RoomNode.DOOR, RoomNode.NORTH);
                RoomArr[room2.Item1, room2.Item2].SetDoor(RoomNode.DOOR, RoomNode.SOUTH);
                dir = 0;
            }
            else if(room1.Item2 > room2.Item2)
            {
                RoomArr[room1.Item1, room1.Item2].SetDoor(RoomNode.DOOR, RoomNode.SOUTH);
                RoomArr[room2.Item1, room2.Item2].SetDoor(RoomNode.DOOR, RoomNode.NORTH);
                dir = 1;
            }
       } else if(room1.Item2 == room2.Item2)
        {
            if (room1.Item1 < room2.Item1)
            {
                RoomArr[room1.Item1, room1.Item2].SetDoor(RoomNode.DOOR, RoomNode.EAST);
                RoomArr[room2.Item1, room2.Item2].SetDoor(RoomNode.DOOR, RoomNode.WEST);
                dir = 2;
            }
            else if (room1.Item1 > room2.Item1)
            {
                RoomArr[room1.Item1, room1.Item2].SetDoor(RoomNode.DOOR, RoomNode.WEST);
                RoomArr[room2.Item1, room2.Item2].SetDoor(RoomNode.DOOR, RoomNode.EAST);
                dir = 3;
            }
        }
        connectedRooms.Add((room1, room2, dir));
    }

    IEnumerable GetNeighbors(int x, int y)
    {
        if (x > 0) yield return (x-1,y);
        if (x < RoomArr.GetLength(0) -1) yield return (x+1,y);
        if (y > 0) yield return (x, y-1);
        if (y < RoomArr.GetLength(1) - 1) yield return (x, y + 1);
    }
    IEnumerable GetNonConnectedNeightbors(int x, int y)
    {
        if (x > 0 && RoomArr[x, y].GetDoor(RoomNode.WEST) == RoomNode.WALL) yield return (x - 1, y);
        if (x < RoomArr.GetLength(0) - 1 && RoomArr[x, y].GetDoor(RoomNode.EAST) == RoomNode.WALL) yield return (x + 1, y);
        if (y > 0 && RoomArr[x, y].GetDoor(RoomNode.SOUTH) == RoomNode.WALL) yield return (x, y - 1);
        if (y < RoomArr.GetLength(1) - 1 && RoomArr[x, y].GetDoor(RoomNode.NORTH) == RoomNode.WALL) yield return (x, y + 1);
    }

    private void CreateLoops()
    {
        
        for(int i = 0; i < loops; i++)
        {
            bool loopCreated = false;
            while (!loopCreated)
            {
                int x = Random.Range(0, RoomArr.GetLength(0));
                int y = Random.Range(0, RoomArr.GetLength(1));
                RoomNode room = RoomArr[x, y];

                List<(int, int)> neighborList = new List<(int, int)>();
                foreach ((int, int) neighbor in GetNonConnectedNeightbors(x, y))
                {
                    neighborList.Add(neighbor);
                }
                if(neighborList.Count > 0)
                {
                    int wallNum = Random.Range(0, neighborList.Count);
                    ConnectRooms((x, y), neighborList[wallNum]);
                    Debug.Log("Connected " + (x, y) + neighborList[wallNum]);
                    loopCreated = true;
                }
            }

        }
    }

    


}

