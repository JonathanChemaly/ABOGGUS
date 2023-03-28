using ABOGGUS.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halfway : MonoBehaviour
{
    public GameObject block;
    public GameObject water;

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
            water.SetActive(false);
            FirePuzzle.NextHalf();
        }
    }

    private void Prepare()
    {
        ready = true;
        water.SetActive(true);
    }
}
