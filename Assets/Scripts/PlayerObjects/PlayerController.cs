using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ABOGGUS.Input;
using System;
using UnityEngine.Playables;
using ABOGGUS.Gameplay;

namespace ABOGGUS.PlayerObjects
{
    public class PlayerController : MonoBehaviour
    {
        private GameObject physicalGameObject;
        private InputAction moveAction;

        public static float speed = PlayerConstants.SPEED_DEFAULT;
        public float jumpHeight = PlayerConstants.JUMP_HEIGHT_DEFAULT;
        public float totalJumpTime = PlayerConstants.JUMP_TIME_DEFAULT;
        public float dodgeLength = PlayerConstants.DODGE_LENGTH_DEFAULT;
        public float totalDodgeTime = PlayerConstants.DODGE_TIME_DEFAULT;
        private float cumulativeJumpTime = PlayerConstants.JUMP_TIME_CUMULATIVE_DEFAULT;
        private float cumulativeDodgeTime = PlayerConstants.DODGE_TIME_CUMULATIVE_DEFAULT;
        private Vector3 target;
        private bool jumping = false;
        private bool dodging = false;
        private int count = 0;
        private bool crouching = false;

        private IPlayerState playerState;
        enum FacingDirection { Forward, Backward, Left, Right, FrontRight, FrontLeft, BackRight, BackLeft, Idle };
        private FacingDirection facingDirection;

        public void Initialize(Input.InputActions playerActions)
        {
            GameController.player.SetController(this);
            facingDirection = FacingDirection.Forward;

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

        public void InitializePlayerState(GameObject physicalGameObject)
        {
            SetGameObject(physicalGameObject);
            playerState = new PlayerFacingForward(this);
        }

        public void SetGameObject(GameObject physicalGameObject)
        {
            this.physicalGameObject = physicalGameObject;
        }
        public GameObject GetGameObject()
        {
            return this.physicalGameObject;
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
            if (physicalGameObject != null)
            {
                MovementHandler(moveAction);
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
        public void MovementHandler(InputAction moveAction)
        {
            Vector2 movement = moveAction.ReadValue<Vector2>();
            //Debug.Log("Movement vector: " + movement);
            FacingDirection currentFacingDirection;
            //Debug.Log("Facing direction: " + facingDirection);
            //Get the current facing direction
            if (movement.x > 0.5 && movement.y > 0.5)
            {
                currentFacingDirection = FacingDirection.FrontRight;
            }
            else if (movement.x < -0.5 && movement.y > 0.5)
            {
                currentFacingDirection = FacingDirection.FrontLeft;
            }
            else if (movement.y > 0.5)
            {
                currentFacingDirection = FacingDirection.Forward;
            }
            else if (movement.x > 0.5 && movement.y < -0.5)
            {
                currentFacingDirection = FacingDirection.BackRight;
            }
            else if (movement.x < -0.5 && movement.y < -0.5)
            {
                currentFacingDirection = FacingDirection.BackLeft;
            }
            else if (movement.y < -0.5)
            {
                currentFacingDirection = FacingDirection.Backward;
            }
            else if (movement.x > 0.5)
            {
                currentFacingDirection = FacingDirection.Right;
            }
            else if (movement.x < -0.5)
            {
                currentFacingDirection = FacingDirection.Left;
            }
            else
            {
                currentFacingDirection = FacingDirection.Idle;
            }

            //Check if the facing direction is the same and not idle to move
            if (currentFacingDirection.Equals(facingDirection) && !currentFacingDirection.Equals(FacingDirection.Idle))
            {
                playerState.Move();
            }
            //Otherwise if the facing direction is not the same then change to new state
            else if (!currentFacingDirection.Equals(facingDirection))
            {
                SetFacingState(currentFacingDirection);
            }
        }
        //Set player state to new facing direction state
        private void SetFacingState(FacingDirection newFacingDirection)
        {
            if (newFacingDirection.Equals(FacingDirection.Forward))
            {
                playerState = new PlayerFacingForward(this);
            }
            else if (newFacingDirection.Equals(FacingDirection.Backward))
            {
                playerState = new PlayerFacingBackward(this);
            }
            else if (newFacingDirection.Equals(FacingDirection.Left))
            {
                playerState = new PlayerFacingLeft(this);
            }
            else if (newFacingDirection.Equals(FacingDirection.Right))
            {
                playerState = new PlayerFacingRight(this);
            }
            else if (newFacingDirection.Equals(FacingDirection.FrontRight))
            {
                playerState = new PlayerFacingFrontRight(this);
            }
            else if (newFacingDirection.Equals(FacingDirection.FrontLeft))
            {
                playerState = new PlayerFacingFrontLeft(this);
            }
            else if (newFacingDirection.Equals(FacingDirection.BackRight))
            {
                playerState = new PlayerFacingBackRight(this);
            }
            else if (newFacingDirection.Equals(FacingDirection.BackLeft))
            {
                playerState = new PlayerFacingBackLeft(this);
            }
            facingDirection = newFacingDirection;
        }

    }
}
