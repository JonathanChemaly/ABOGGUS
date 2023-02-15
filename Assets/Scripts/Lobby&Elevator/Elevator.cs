using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;

public class Elevator : MonoBehaviour
{
    private Vector3 playerDown;
    private Vector3 playerUp;
    private Vector3 upPosition;
    private Vector3 downPosition;

    public GameObject player;
    public GameObject elevator;
    public GameObject elevatorShaftFloor;

    private float elevatorSpeed = 0.05f;
    private float timer = 8.0f;
    private bool elevatorMoving = false;
    private bool trueUpFalseDown = true;

    private void Start()
    {
        
        upPosition = elevator.transform.position;
        downPosition = elevator.transform.position;
        downPosition.y -= 15.1f;
        
        playerDown = elevatorShaftFloor.transform.position;
        playerUp = playerDown;
        playerUp.y += 15.1f;
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
            if (!trueUpFalseDown && timer <= 0.0f)
            {
                elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, downPosition, elevatorSpeed);
                player.transform.position = Vector3.MoveTowards(player.transform.position, playerDown, elevatorSpeed);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (player.transform.position.y < (playerUp.y + playerDown.y) / 2)
            {
                trueUpFalseDown = true;
            }
            else if (player.transform.position.y > (playerUp.y + playerDown.y) / 2)
            {
                trueUpFalseDown = false;
            }
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
        if (other.CompareTag("Player"))
        {
            elevatorMoving = true;
        }
    }
}
