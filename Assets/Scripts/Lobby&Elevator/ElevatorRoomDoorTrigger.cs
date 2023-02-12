using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorRoomDoorTrigger : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDoor.Play("ElevatorRoomDoorOpen", 0, 0.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDoor.Play("ElevatorRoomDoorClose", 0, 0.0f);
        }
    }
}
