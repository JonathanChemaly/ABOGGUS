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
        public Player player;
        public GameObject physicalGameObject;

        public static float speed = PlayerConstants.SPEED_DEFAULT;
        public float jumpHeight = PlayerConstants.JUMP_HEIGHT_DEFAULT;
        public float totalJumpTime = PlayerConstants.JUMP_TIME_DEFAULT;
        public float dodgeLength = PlayerConstants.DODGE_LENGTH_DEFAULT;
        public float totalDodgeTime = PlayerConstants.DODGE_TIME_DEFAULT;
        private float cumulativeJumpTime = PlayerConstants.JUMP_TIME_CUMULATIVE_DEFAULT;
        private float cumulativeDodgeTime = PlayerConstants.DODGE_TIME_CUMULATIVE_DEFAULT;
        private InputAction moveAction;
        private Vector3 target;
        private bool jumping = false;
        private bool dodging = false;
        private int count = 0;
        private bool crouching = false;

        public void Initialize(Input.InputActions playerActions)
        {
            player.SetController(this);
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

        public void SetGameObject(GameObject physicalGameObject)
        {
            this.physicalGameObject = physicalGameObject;
        }

        private void StopSprint(InputAction.CallbackContext obj)
        {
            speed /= PlayerConstants.SPRINT_MULTIPLIER_DEFAULT;
        }

        private void DoSprint(InputAction.CallbackContext obj)
        {
            speed *= PlayerConstants.SPRINT_MULTIPLIER_DEFAULT;
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

        public void _FixedUpdate()
        {
            player.MovementHandler(moveAction);
            if (jumping && Time.fixedDeltaTime + cumulativeJumpTime < (totalJumpTime / 2))
            {
                Debug.Log("Jump time: " + Time.fixedDeltaTime);
                cumulativeJumpTime += Time.fixedDeltaTime;
                Vector3 up = new Vector3(physicalGameObject.transform.position.x, jumpHeight * cumulativeJumpTime + physicalGameObject.transform.position.y, physicalGameObject.transform.position.z);
                physicalGameObject.transform.localPosition = Vector3.MoveTowards(physicalGameObject.transform.localPosition, up, jumpHeight * cumulativeJumpTime);
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
                Vector3 target = physicalGameObject.transform.position + physicalGameObject.transform.forward * speed;
                physicalGameObject.transform.localPosition = Vector3.MoveTowards(physicalGameObject.transform.localPosition, target, dodgeLength * cumulativeDodgeTime);
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
