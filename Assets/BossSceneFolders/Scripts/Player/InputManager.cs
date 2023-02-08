using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private MovementControl movementController;
    [SerializeField] private RotateControl rotateController;
    private InputActions inputScheme;


    void Awake()
    {
        inputScheme = new InputActions();
        movementController.Initialize(inputScheme.Player.Move);
        rotateController.Initialize(inputScheme.Player.Rotate);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        var _ = new QuitHandler(inputScheme.Player.Quit);
    }
}
