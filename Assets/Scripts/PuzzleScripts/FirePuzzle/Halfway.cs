using ABOGGUS.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halfway : MonoBehaviour
{
    public GameObject block;
    public Material readyMaterial;
    public Material completedMaterial;

    public static Action Ready;
    private bool ready = false;

    private void Start()
    {
        Ready += Prepare;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ready && other.CompareTag(GameConstants.NAME_PLAYERGAMEOBJECT))
        {
            block.GetComponent<Renderer>().material = completedMaterial;
            FirePuzzle.NextHalf();
        }
    }

    private void Prepare()
    {
        ready = true;
        block.GetComponent<Renderer>().material = readyMaterial;
    }
}
