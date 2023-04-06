using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIceEntrance : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FallingIceFloorPuzzle.CreateFloor();
        }
    }
}
