using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private Vector3 playerDown;
    private Vector3 playerUp;
    private Vector3 upPosition;
    private Vector3 downPosition;

    public GameObject player;
    public GameObject elevator;

    private float elevatorSpeed = 0.03f;
    public float timer = 4.0f;
    public bool elevatorMoving = false;
    public bool trueUpFalseDown = true;
    

    private void Start()
    {
        
        upPosition = elevator.transform.position;
        downPosition = elevator.transform.position;
        downPosition.y = -22.2f;
        playerUp = upPosition;
        playerDown = downPosition;
        
    }
    void FixedUpdate()
    {
        if (elevatorMoving)
        {
            timer -= Time.deltaTime;
            playerUp.x = player.transform.position.x;
            playerUp.z = player.transform.position.z;
            playerDown.x = player.transform.position.x;
            playerDown.z = player.transform.position.z;
            if (trueUpFalseDown && timer <= 0.0f)
            {
                elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, upPosition, elevatorSpeed);
                player.transform.position = Vector3.MoveTowards(player.transform.position, playerUp, elevatorSpeed);
            }
            else if (!trueUpFalseDown && timer <= 0.0f)
            {
                elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, downPosition, elevatorSpeed);
                player.transform.position = Vector3.MoveTowards(player.transform.position, playerDown, elevatorSpeed);
            }
        }
    }

    public void MoveElevator()
    {
        trueUpFalseDown = !trueUpFalseDown;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MoveElevator();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer = 4.0f;
            elevatorMoving = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        elevatorMoving = true;
    }
}
