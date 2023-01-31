using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ABOGGUS.Input;
using System;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private GameObject player;
    public float speed = 0.05f;
    public float jumpSpeed = 0.01f;
    public int jumpTime = 5;
    private InputAction moveAction;
    private Vector3 target;
    private bool jumping = false;
    private int count = 0;
    private bool crouching = false;

    public void Initialize(InputActions playerActions)
    {
        moveAction = playerActions.Player.Move;
        moveAction.Enable();

        playerActions.Player.Jump.performed += DoJump;
        playerActions.Player.Jump.Enable();

        playerActions.Player.Crouch.performed += DoCrouch;
        playerActions.Player.Crouch.Enable();

        playerActions.Player.Sprint.performed += DoSprint;
        playerActions.Player.Sprint.canceled += StopSprint;
        playerActions.Player.Sprint.Enable();
    }

    private void StopSprint(InputAction.CallbackContext obj)
    {
        speed /= 2;
    }

    private void DoSprint(InputAction.CallbackContext obj)
    {
        speed *= 2;
    }

    private void DoCrouch(InputAction.CallbackContext obj)
    {
        crouching = !crouching;
        if (crouching)
        {
            player.transform.localScale = new Vector3(1, 0.6f * player.transform.localScale.y, 1);
        }
        else
        {
            player.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        if (player.transform.localPosition.y <= 0.5f)
        {
            jumping = true;
        }
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    void FixedUpdate()
    {
        Vector3 forward = new Vector3(player.transform.forward.x, 0f, player.transform.forward.z);
        Vector3 right = new Vector3(player.transform.right.x, 0f, player.transform.right.z);
        if (jumping)
        {
            count++;
            Vector3 up = new Vector3(player.transform.position.x, jumpSpeed + player.transform.position.y, player.transform.position.z);
            player.transform.localPosition = Vector3.MoveTowards(player.transform.localPosition, up, jumpSpeed);
            if (count >= jumpTime)
            {
                jumping = false;
                count = 0;
            }
            crouching = false;
            player.transform.localScale = new Vector3(1, 1, 1);
        }
        target = player.transform.localPosition + (moveAction.ReadValue<Vector2>().x * right) + (moveAction.ReadValue<Vector2>().y * forward);
        player.transform.localPosition = Vector3.MoveTowards(player.transform.localPosition, target, speed);
    }

}
