using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitBehavior : MonoBehaviour
{
    public GameObject floor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player" || other.transform.name == "Sword")
        {
            floor.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Player" || other.transform.name == "Sword")
        {
            floor.GetComponent<BoxCollider>().enabled = true;
        }
    }

}
