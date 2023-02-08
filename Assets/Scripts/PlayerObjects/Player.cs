using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ABOGGUS.Input;

namespace ABOGGUS.PlayerObjects
{
    public class Player : MonoBehaviour
    {
        public bool key = true;
        private IPlayerState playerState;
        enum FacingDirection { Forward, Backward, Left, Right, FrontRight, FrontLeft, BackRight, BackLeft, Idle };
        private FacingDirection facingDirection;

        public void Initialize()
        {
            playerState = new PlayerFacingForward(this);
            facingDirection = FacingDirection.Forward;
        }

        public void MovementHandler(InputAction moveAction)
        {
            Vector2 movement = moveAction.ReadValue<Vector2>();
            FacingDirection currentFacingDirection;
            //Get the current facing direction
            if (movement.x > 0 && movement.y > 0)
            {
                currentFacingDirection = FacingDirection.FrontRight;
            }
            else if (movement.x < 0 && movement.y > 0)
            {
                currentFacingDirection = FacingDirection.FrontLeft;
            }
            else if (movement.x == 0 && movement.y > 0)
            {
                currentFacingDirection = FacingDirection.Forward;
            }
            else if (movement.x > 0 && movement.y < 0)
            {
                currentFacingDirection = FacingDirection.BackRight;
            }
            else if (movement.x < 0 && movement.y < 0)
            {
                currentFacingDirection = FacingDirection.BackLeft;
            }
            else if (movement.x == 0 && movement.y < 0)
            {
                currentFacingDirection = FacingDirection.Backward;
            }
            else if (movement.x > 0 && movement.y == 0)
            {
                currentFacingDirection = FacingDirection.Right;
            }
            else if (movement.x < 0 && movement.y == 0)
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
