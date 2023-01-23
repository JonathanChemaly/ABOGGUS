using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EllisonJosh.Input;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private InputActions inputScheme;
    private string str;

    private void Awake()
    {
        inputScheme = new InputActions();
        playerController.Initialize(inputScheme);
    }
}
