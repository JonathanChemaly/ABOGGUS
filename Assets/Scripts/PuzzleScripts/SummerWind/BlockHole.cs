using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class BlockHole : MonoBehaviour
{
    private List<int> holes = new List<int>();
    private List<int> blocks = new List<int>();
    private List<int> wallsToDelete = new List<int>();
    private GameObject[] objects = new GameObject[1000];

    [SerializeField] GameObject stagePrefab;
    [SerializeField] GameObject wallPrefab;
    [SerializeField] GameObject wall2Prefab;
    [SerializeField] GameObject blockPrefab;
    [SerializeField] GameObject lavaPrefab;
    [SerializeField] GameObject player;
    [SerializeField] GameObject trigger;

    public int boardSize = 6;
    public int numHoles = 5;
    public int numBlocks = 4;
    private int boardArea;
    public static int blocksLeft;
    public GameObject blackout;
    GameObject lava;
    int numObjects = 0;
    float speed = 1;

    bool cleared = false;
    bool restarting = false;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = new Vector3(0, 8, 0) + transform.position;
        boardArea = boardSize * boardSize;
        blocksLeft = numBlocks;
        generateLevel();
        StartCoroutine(BlackOut(false));
    }

    void Update()
    {
        //game cleared
        if (blocksLeft == 0)
        {
            cleared = true;
            foreach (GameObject i in objects)
            {
                Destroy(i);
            }
            Destroy(lava);
            player.transform.position = new Vector3(114, 6, -192);
            player.transform.rotation = new Quaternion(0, 60, 0, 0);
            this.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (!cleared && !restarting && lava.transform.localPosition.y < 12.5)
        {
            lava.transform.Translate(Vector3.up * speed * Time.deltaTime / 10);
        }
    }

    //restarts the level after fail
    public IEnumerator restart()
    {
        StartCoroutine(BlackOut());
        yield return new WaitForSecondsRealtime(1f);

        restarting = true;
        lava.transform.localPosition = new Vector3(0, -4, 0);

        for (int i = 0; i < numObjects; i++)
        {
            Destroy(objects[i].gameObject);
        }
        holes.Clear();
        blocks.Clear();
        Destroy(lava.gameObject);
        numObjects = 0;
        blocksLeft = numBlocks;
        cleared = false;

        generateLevel();

        player.transform.position = new Vector3(0, 8, 0) + transform.position;
        StartCoroutine(BlackOut(false));
        restarting = false;
        yield return null;
    }

    //Instantiates the puzzle level
    void generateLevel()
    {
        placeHoles();
        placeBlocks();
        int tempCellNum = 0;
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (!holes.Contains(tempCellNum))
                {
                    Vector3 pos = new Vector3();

                    pos.x = transform.position.x + (i - boardSize / 2) * 4;
                    pos.z = transform.position.z + (j - boardSize / 2) * 4;
                    pos.y = transform.position.y - 2;

                    objects[numObjects] = Instantiate(stagePrefab, pos, Quaternion.identity);
                    numObjects++;
                }
                if (blocks.Contains(tempCellNum))
                {
                    Vector3 pos = new Vector3();

                    pos.x = transform.position.x + (i - boardSize / 2) * 4;
                    pos.z = transform.position.z + (j - boardSize / 2) * 4;
                    pos.y = transform.position.y + 2;

                    objects[numObjects] = Instantiate(blockPrefab, pos, Quaternion.identity);
                    numObjects++;
                }
                placeWalls(i, j);
                tempCellNum++;
            }
        }

        placeLava();
    }

    //Initializes the locations of the holes randomly
    void placeHoles()
    {
        int tempHoleCount = 0;
        System.Random random = new System.Random();
        int holeIndex = random.Next(boardArea);

        while (tempHoleCount < numHoles)
        {
            while (holes.Contains(holeIndex)) {
                holeIndex = random.Next(boardArea);
            }
            holes.Add(holeIndex);
            tempHoleCount++;
        }
    }

    //Initializes the locations of the blocks randomly
    void placeBlocks()
    {
        int tempBlockCount = 0;
        System.Random random = new System.Random();
        int blockIndex = random.Next(boardArea);

        while (tempBlockCount < numBlocks)
        {
            //checks to make sure there are no other blocks or holes there, also dont place on edge
            while (blocks.Contains(blockIndex) || holes.Contains(blockIndex) || blockIndex < boardSize || (blockIndex+1)%boardSize == 0 || blockIndex%boardSize == 0 || (boardArea - blockIndex) <= boardSize)
            {
                blockIndex = random.Next(boardArea);
            }
            blocks.Add(blockIndex);
            tempBlockCount++;
        }
    }

    //Lava mechanics
    void placeLava()
    {
        Vector3 pos = new Vector3();

        pos.x = transform.position.x;
        pos.z = transform.position.z;
        pos.y = transform.position.y - 4;

        lava = Instantiate(lavaPrefab, pos, Quaternion.identity);
    }

    //blackout animation taken from https://turbofuture.com/graphic-design-video/How-to-Fade-to-Black-in-Unity
    public IEnumerator BlackOut(bool fadeToBlack = true, int fadeSpeed = 1)
    {
        UnityEngine.Color objectColor = blackout.GetComponent<RawImage>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackout.GetComponent<RawImage>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new UnityEngine.Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackout.GetComponent<RawImage>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (blackout.GetComponent<RawImage>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new UnityEngine.Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackout.GetComponent<RawImage>().color = objectColor;
                yield return null;
            }
        }
    }

    void placeWalls(int i, int j)
    {
        if (i == 0)
        {
            Vector3 pos = new Vector3();

            pos.x = transform.position.x + (i - boardSize / 2) * 4 - 2;
            pos.z = transform.position.z + (j - boardSize / 2) * 4;
            pos.y = transform.position.y + 2;
            Quaternion rot = Quaternion.Euler(0, 90, 0);

            if (i == boardSize / 2)
            {
                wallsToDelete.Add(numObjects);
            }

            objects[numObjects] = Instantiate(wallPrefab, pos, rot);
            numObjects++;
        }
        if (i == boardSize - 1)
        {
            Vector3 pos = new Vector3();

            pos.x = transform.position.x + (i - boardSize / 2) * 4 + 2;
            pos.z = transform.position.z + (j - boardSize / 2) * 4;
            pos.y = transform.position.y + 2;
            Quaternion rot = Quaternion.Euler(0, 90, 0);

            objects[numObjects] = Instantiate(wallPrefab, pos, rot);
            numObjects++;
        }       
        if (j == 0)
        {
            Vector3 pos = new Vector3();

            pos.x = transform.position.x + (i - boardSize / 2) * 4;
            pos.z = transform.position.z + (j - boardSize / 2) * 4 - 2;
            pos.y = transform.position.y + 2;
            Quaternion rot = Quaternion.Euler(90, 0, 0);
           
            objects[numObjects] = Instantiate(wall2Prefab, pos, rot);
            numObjects++;
        }
        if (j == boardSize - 1)
        {
            Vector3 pos = new Vector3();

            pos.x = transform.position.x + (i - boardSize / 2) * 4;
            pos.z = transform.position.z + (j - boardSize / 2) * 4 + 2;
            pos.y = transform.position.y + 2;
            Quaternion rot = Quaternion.Euler(0, 0, 0);
            if (i == boardSize / 2 - 1)
            {
                wallsToDelete.Add(numObjects);
            }
            objects[numObjects] = Instantiate(wallPrefab, pos, rot);
            numObjects++;
        }
    }
}
