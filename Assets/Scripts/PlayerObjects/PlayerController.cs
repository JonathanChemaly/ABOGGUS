using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ABOGGUS.Input;
using System;

namespace ABOGGUS.PlayerObjects
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private Player player;
        public static float speed = 0.6f;
        public float jumpHeight = 3.0f;
        public float totalJumpTime = 40;
        public float dodgeLength = 0.1f;
        public float totalDodgeTime = 5;
        private float cumulativeJumpTime = 0;
        private float cumulativeDodgeTime = 0;
        private InputAction moveAction;
        private Vector3 target;
        private bool jumping = false;
        private bool dodging = false;
        private int count = 0;
        private bool crouching = false;

        public void Initialize(InputActions playerActions)
        {
            player.Initialize();
            moveAction = playerActions.Player.Move;
            moveAction.Enable();

            playerActions.Player.Jump.performed += DoJump;
            playerActions.Player.Jump.Enable();

            playerActions.Player.Dodge.performed += DoDodge;
            playerActions.Player.Dodge.Enable();

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
                PlayerAnimationStateController.StartCrouchAnimation();
            }
            else
            {
                PlayerAnimationStateController.StopCrouchAnimation();
            }
        }

        private void DoJump(InputAction.CallbackContext obj)
        {
            if (!jumping && !dodging)
            {
                jumping = true;
                PlayerAnimationStateController.StartJumpAnimation();
            }
        }

        private void DoDodge(InputAction.CallbackContext obj)
        {
            if (!dodging && !jumping)
            {
                dodging = true;
                PlayerAnimationStateController.StartDodgeAnimation();
            }
        }

        private void OnDisable()
        {
            moveAction.Disable();
        }

        void FixedUpdate()
        {
            player.MovementHandler(moveAction);
            if (jumping && Time.fixedDeltaTime + cumulativeJumpTime < (totalJumpTime / 2))
            {
                Debug.Log("Jump time: " + Time.fixedDeltaTime);
                cumulativeJumpTime += Time.fixedDeltaTime;
                Vector3 up = new Vector3(player.transform.position.x, jumpHeight * cumulativeJumpTime + player.transform.position.y, player.transform.position.z);
                player.transform.localPosition = Vector3.MoveTowards(player.transform.localPosition, up, jumpHeight * cumulativeJumpTime);
            }
            else if (jumping && Time.fixedDeltaTime + cumulativeJumpTime < totalJumpTime)
            {
                Debug.Log("End jump time: " + Time.fixedDeltaTime);
                cumulativeJumpTime += Time.fixedDeltaTime;
                PlayerAnimationStateController.StopJumpAnimation();
            }
            else
            {
                cumulativeJumpTime = 0;
                jumping = false;
            }

            if (dodging && Time.fixedDeltaTime + cumulativeDodgeTime < (totalDodgeTime / 2))
            {
                Debug.Log("Dodge time: " + Time.fixedDeltaTime);
                cumulativeDodgeTime += Time.fixedDeltaTime;
                Vector3 target = player.transform.position + player.transform.forward * speed;
                player.transform.localPosition = Vector3.MoveTowards(player.transform.localPosition, target, dodgeLength * cumulativeDodgeTime);
            }
            else if (dodging && Time.fixedDeltaTime + cumulativeDodgeTime < totalDodgeTime)
            {
                Debug.Log("End Dodge time: " + Time.fixedDeltaTime);
                cumulativeDodgeTime += Time.fixedDeltaTime;
                PlayerAnimationStateController.StopDodgeAnimation();
            }
            else
            {
                cumulativeDodgeTime = 0;
                dodging = false;
            }
        }

    }
}
