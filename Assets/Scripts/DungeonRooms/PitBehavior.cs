using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player" || other.transform.name == "Sword")
        {
            GameObject.Find("Floor").GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Player" || other.transform.name == "Sword")
        {
            GameObject.Find("Floor").GetComponent<BoxCollider>().enabled = true;
        }
    }

}
