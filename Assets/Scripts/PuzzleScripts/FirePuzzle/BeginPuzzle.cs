using ABOGGUS.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPuzzle : MonoBehaviour
{
    public GameObject block;
    public GameObject water;
    public Material completedMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstants.NAME_PLAYERGAMEOBJECT))
        {
            water.SetActive(false);
            FirePuzzle.Instance.fireActive = true;
        }
    }
}
