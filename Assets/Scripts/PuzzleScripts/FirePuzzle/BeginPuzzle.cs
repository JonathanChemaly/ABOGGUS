using ABOGGUS.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPuzzle : MonoBehaviour
{
    public GameObject block;
    public Material completedMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstants.NAME_PLAYERGAMEOBJECT))
        {
            block.GetComponent<Renderer>().material = completedMaterial;
            FirePuzzle.Instance.fireActive = true;
        }
    }
}
