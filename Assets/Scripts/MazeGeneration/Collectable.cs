using ABOGGUS.Gameplay;
using ABOGGUS.MazeGeneration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject surface;
    public string colour;

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
            switch (colour)
            {
                case "Red": MazeController.Red = true; break;
                case "Green": MazeController.Green = true; break;
                case "Blue": MazeController.Blue = true; break;
                case "Black": MazeController.Black = true; break;
            }
        }
    }
}
