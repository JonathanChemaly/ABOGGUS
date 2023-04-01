using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;
public class TopWinterElevatorDoorTrigger : MonoBehaviour
{
    private Vector3 topDoorRP;
    private Vector3 topDoorLP;
    private Vector3 topDoorRPO;
    private Vector3 topDoorLPO;

    private float doorSpeed = 0.02f;
    private float timer = 2.0f;

    public GameObject topDoorR;
    public GameObject topDoorL;

    public bool openTopDoor = false;
    public bool flipOrientation = false;
    [SerializeField] private bool transition = false;

    void Start()
    {
        topDoorRP = topDoorR.transform.position;
        topDoorLP = topDoorL.transform.position;

        if (flipOrientation)
        {
            topDoorRPO = topDoorRP - new Vector3(1.7f, 0.0f, 0.0f);
            topDoorLPO = topDoorLP + new Vector3(1.7f, 0.0f, 0.0f);
        }else
        {
            topDoorRPO = topDoorRP + new Vector3(1.7f, 0.0f, 0.0f);
            topDoorLPO = topDoorLP - new Vector3(1.7f, 0.0f, 0.0f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (openTopDoor)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                topDoorR.transform.position = Vector3.MoveTowards(topDoorR.transform.position, topDoorRPO, doorSpeed);
                topDoorL.transform.position = Vector3.MoveTowards(topDoorL.transform.position, topDoorLPO, doorSpeed);
            }
        }
        else
        {
            topDoorR.transform.position = Vector3.MoveTowards(topDoorR.transform.position, topDoorRP, doorSpeed);
            topDoorL.transform.position = Vector3.MoveTowards(topDoorL.transform.position, topDoorLP, doorSpeed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (transition && other.CompareTag("Player"))
        {
            openTopDoor = true;
            if (GameController.scene == GameConstants.SCENE_MAINLOBBY)
            {
                GameController.ChangeScene("Elevator to Autumn Room", GameConstants.SCENE_WINTERROOM, false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer = 2.0f;
            openTopDoor = false;
            ThirdPersonCameraController.preAnimCam = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ThirdPersonCameraController.preAnimCam = true;
        }
    }

}
