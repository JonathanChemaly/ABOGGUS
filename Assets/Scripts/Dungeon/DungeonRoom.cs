using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    public GameObject[] Doors;
    public Vector3 roomPos;

    // Start is called before the first frame update
    void Start()
    {
        roomPos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
