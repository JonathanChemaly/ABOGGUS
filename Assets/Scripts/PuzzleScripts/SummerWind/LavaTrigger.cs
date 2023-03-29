using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrigger : MonoBehaviour
{
    public BlockHole blockhole;
    // Start is called before the first frame update
    void Start()
    {
        blockhole = FindObjectOfType<BlockHole>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(blockhole.restart());
        }
    }
}