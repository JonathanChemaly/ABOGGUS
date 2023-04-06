using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIceEntrance : MonoBehaviour
{
    private Vector3 startPoistion;
    private Vector3 tempPoistion;
    private float timer = 20f;
    private bool moved = false;

    void Start()
    {
        startPoistion = transform.position;
        tempPoistion = startPoistion + new Vector3(0f, 20f, 0f);
    }
    private void FixedUpdate()
    {

        if (moved)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 20f && timer > 19f)
        {
            transform.position = tempPoistion;
        }
        if (timer <= 0)
        {
            moved = false;
            timer = 10f;
            transform.position = startPoistion;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            moved = true;
            FallingIceFloorPuzzle.CreateFloor();
        }
    }
}
