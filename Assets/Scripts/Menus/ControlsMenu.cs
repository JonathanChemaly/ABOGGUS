using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    [SerializeField] private GameObject controlsBox;

    public void ExitControlsMenu()
    {
        controlsBox.SetActive(false);
    }
}
