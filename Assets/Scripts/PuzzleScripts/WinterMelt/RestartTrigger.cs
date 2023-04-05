using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartTrigger : MonoBehaviour
{
    public MeltPuzzle meltpuzzle;
    // Start is called before the first frame update
    void Start()
    {
        meltpuzzle = FindObjectOfType<MeltPuzzle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meltpuzzle.Restart();
        }
    }
}
