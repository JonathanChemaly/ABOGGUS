using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp
{
    private GameObject pickUp;
    
    public PickUp(Transform parent, GameObject pickUpPrefab, int numPickUp, int total)
    {
        pickUp = Object.Instantiate(pickUpPrefab, new Vector3(4, 0.5f, 4), Quaternion.Euler(45, 45, 45));
        pickUp.transform.parent = parent;
        pickUp.transform.RotateAround(new Vector3(0,0,0), new Vector3(0, 1, 0), -(360 / total * numPickUp));
    }
}
