using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopElevatorDoorTrigger : MonoBehaviour
{
    private Vector3 topDoorRP;
    private Vector3 topDoorLP;
    private Vector3 topDoorRPO;
    private Vector3 topDoorLPO;

    private float doorSpeed = 0.08f;

    public GameObject topDoorR;
    public GameObject topDoorL;

    public bool openTopDoor = false;

    void Start()
    {
        topDoorRP = topDoorR.transform.position;
        topDoorLP = topDoorL.transform.position;

        topDoorRPO = topDoorRP + new Vector3(10.4f, 0.0f, 0.0f);
        topDoorLPO = topDoorLP - new Vector3(10.4f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (openTopDoor)
        {
            topDoorR.transform.position = Vector3.MoveTowards(topDoorR.transform.position, topDoorRPO, doorSpeed);
            topDoorL.transform.position = Vector3.MoveTowards(topDoorL.transform.position, topDoorLPO, doorSpeed);
        }
        else
        {
            topDoorR.transform.position = Vector3.MoveTowards(topDoorR.transform.position, topDoorRP, doorSpeed);
            topDoorL.transform.position = Vector3.MoveTowards(topDoorL.transform.position, topDoorLP, doorSpeed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openTopDoor = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openTopDoor = false;
        }
    }

}
