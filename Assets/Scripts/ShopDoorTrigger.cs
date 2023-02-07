using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDoorTrigger : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDoor.Play("ShopDoorOpen", 0, 0.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDoor.Play("ShopDoorClose", 0, 0.0f);
        }
    }
}
