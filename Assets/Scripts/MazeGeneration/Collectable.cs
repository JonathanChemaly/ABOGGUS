using ABOGGUS.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject surface;

    // Start is called before the first frame update
    void Start()
    {
        surface.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstants.NAME_PLAYERGAMEOBJECT))
        {
            surface.SetActive(true);
        }
    }
}
