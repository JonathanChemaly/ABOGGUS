using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moveable")) {
            Destroy(other.gameObject);
            BlockHole.blocksLeft--;
        }
    }
}
