using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIceFloorPuzzle : MonoBehaviour
{
    [SerializeField] public GameObject iceFall;
    [SerializeField] public GameObject iceWall;
    [SerializeField] public GameObject iceExit;
    [SerializeField] public GameObject iceFloor;
    [SerializeField] public int rows;
    [SerializeField] public int columns;
    private static GameObject[] iceFloors;
    public static Vector3 startPoint;
    private static GameObject fall;
    private static GameObject wall;
    private static GameObject exit;
    private static int row;
    private static int col;
    private static int times = 0;
    private static bool puzzleStart = false;
    private static float timer = 10f;
    private static bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        fall = iceFall;
        wall = iceWall;
        row = rows;
        col = columns;
        exit = iceExit;

        for (int c = 0; c < col; c++)
        {
            for (int r = 0; r < row; r++)
            {
                if ((r == 0 && c == col / 2) || (r == row - 1 && c == col / 2))
                {
                    Instantiate(iceFloor, startPoint + new Vector3(c * 4f, 0f, r * 4f), Quaternion.identity);
                }
                else if ((r == 0 || r == row - 1 || c == 0 || c == col - 1) || (r == row / 2 && c == col / 2))
                {
                    Instantiate(wall, startPoint + new Vector3(c * 4f, 0f, r * 4f), Quaternion.identity);
                }
            }
        }
        
    }

    public void FixedUpdate()
    {
        if (puzzleStart)
        {
            timer -= Time.deltaTime;
            iceFloors = GameObject.FindGameObjectsWithTag("iceFloor");
            if (iceFloors.Length == 0)
            {
                Debug.Log("puzzlecomplete");
                Destroy(GameObject.Find("iceExit"));
                puzzleStart = false;
            }
            if (timer < 0f)
            {
                timer = 10f;
                once = true;
            }
        }
    }

    public static void CreateFloor()
    {
        if (once)
        {
            times++;
            Debug.Log(times);
            Instantiate(exit, startPoint + new Vector3((col / 2) * 4f, 0f, -4f), Quaternion.identity);
            for (int c = 1; c < col - 1; c++)
            {
                for (int r = 1; r < row - 1; r++)
                {
                    if (r == 1 || r == row - 2 || c == 1 || c == col - 2)
                    {
                        Instantiate(fall, startPoint + new Vector3(c * 4f, 0f, r * 4f), Quaternion.identity);
                    }
                    else if (r == row / 2 && c == col / 2)
                    {
                        //do nothing
                    }
                    else if (Random.Range(0, 4) == 0)
                    {
                        Instantiate(wall, startPoint + new Vector3(c * 4f, 0f, r * 4f), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(fall, startPoint + new Vector3(c * 4f, 0f, r * 4f), Quaternion.identity);
                    }

                }
            }
            puzzleStart = true;
            once = false;
        }
    }

}