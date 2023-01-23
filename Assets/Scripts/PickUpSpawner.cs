using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pickUpPrefab;
    [SerializeField] private int numPickUps;
    List<PickUp> pickUps;
    // Start is called before the first frame update
    void Start()
    {
        pickUps = new List<PickUp>();
        for (int i = 0; i < numPickUps; i++)
        {
            pickUps.Add(new PickUp(transform, pickUpPrefab, i, numPickUps));
        }
    }
}
