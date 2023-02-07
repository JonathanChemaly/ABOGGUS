using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotElevatorDoorTrigger : MonoBehaviour
{
    private Vector3 botDoorRP;
    private Vector3 botDoorLP;
    private Vector3 botDoorRPO;
    private Vector3 botDoorLPO;

    private float doorSpeed = 0.01f;

    public GameObject botDoorR;
    public GameObject botDoorL;

    public bool openBottomDoor = false;

    void Start()
    {
        botDoorRP = botDoorR.transform.position;
        botDoorLP = botDoorL.transform.position;

        botDoorRPO = botDoorRP + new Vector3(1.3f, 0.0f, 0.0f);
        botDoorLPO = botDoorLP - new Vector3(1.3f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (openBottomDoor)
        {
            botDoorR.transform.position = Vector3.MoveTowards(botDoorR.transform.position, botDoorRPO, doorSpeed);
            botDoorL.transform.position = Vector3.MoveTowards(botDoorL.transform.position, botDoorLPO, doorSpeed);
        }
        else
        {
            botDoorR.transform.position = Vector3.MoveTowards(botDoorR.transform.position, botDoorRP, doorSpeed);
            botDoorL.transform.position = Vector3.MoveTowards(botDoorL.transform.position, botDoorLP, doorSpeed);
        }
    }
    public void OpenBottomDoor()
    {
        openBottomDoor = !openBottomDoor;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenBottomDoor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenBottomDoor();
        }
    }

}
