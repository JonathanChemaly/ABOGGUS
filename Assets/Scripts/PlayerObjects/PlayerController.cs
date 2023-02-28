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
        //Change to event triggers later
        private GameObject grimoire;
        private GameObject sword;
        private InputAction moveAction;

        public static float speed = PlayerConstants.SPEED_DEFAULT;
        private float jumpHeight = PlayerConstants.JUMP_HEIGHT_DEFAULT;
        private float totalJumpTime = PlayerConstants.JUMP_TIME_DEFAULT;
        private float dodgeLength = PlayerConstants.DODGE_LENGTH_DEFAULT;
        private float totalDodgeTime = PlayerConstants.DODGE_TIME_DEFAULT;
        private float cumulativeJumpTime = PlayerConstants.JUMP_TIME_CUMULATIVE_DEFAULT;
        private float cumulativeDodgeTime = PlayerConstants.DODGE_TIME_CUMULATIVE_DEFAULT;
        private Vector3 target;
        private bool jumping = false;
        private bool dodging = false;
        private bool attacking = false;
        private bool sprinting = false;
        private int count = 0;
        private bool crouching = false;
        private bool casting = false;
        private bool aoe = false;
        private bool transitioning = false;
        private PlayerConstants.Magic castType = PlayerConstants.Magic.Wind;
        private PlayerConstants.Weapon weaponEquipped = PlayerConstants.Weapon.Sword;
        public GameObject windAttackPrefab;
        public GameObject windAOEPrefab;

        private IPlayerState playerState;
        enum FacingDirection { Forward, Backward, Left, Right, FrontRight, FrontLeft, BackRight, BackLeft, Idle };
        private FacingDirection facingDirection;

        public void InitializeForPlayer()
        {
            GameController.player.SetController(this);
            facingDirection = FacingDirection.Forward;
        }

        public void InitializeInput(Input.InputActions playerActions)
        {
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

            playerActions.Player.Attack.performed += DoAttack;
            playerActions.Player.Attack.canceled += StopAttack;
            playerActions.Player.Attack.Enable();

            playerActions.Player.Cast.performed += DoCast;
            playerActions.Player.Cast.Enable();

            playerActions.Player.CastAOE.performed += DoCastAOE;
            playerActions.Player.CastAOE.Enable();

            playerActions.Player.EquipSword.performed += DoEquipSword;
            playerActions.Player.EquipSword.Enable();

            playerActions.Player.EquipGrimoire.performed += DoEquipGrimoire;
            playerActions.Player.EquipGrimoire.Enable();

            playerActions.Player.Dequip.performed += DoDequip;
            playerActions.Player.Dequip.Enable();

            PlayerAnimationStateController.SetArmedStatus(true);
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

        private void DoDequip(InputAction.CallbackContext obj)
        {
            if (weaponEquipped == PlayerConstants.Weapon.Sword)
            {
                PlayerAnimationStateController.StartDequipSwordAnimation();
                sword.SetActive(false);
            }
            else if (weaponEquipped == PlayerConstants.Weapon.Grimoire)
            {
                PlayerAnimationStateController.StartDequipGrimoireAnimation();
                grimoire.SetActive(false);
            }
            weaponEquipped = PlayerConstants.Weapon.Unarmed;
            PlayerAnimationStateController.SetArmedStatus(false);
            transitioning = true;
        }

        private void DoEquipGrimoire(InputAction.CallbackContext obj)
        {
            if (weaponEquipped == PlayerConstants.Weapon.Sword)
            {
                PlayerAnimationStateController.StartDequipSwordAnimation();
                sword.SetActive(false);
            }
            PlayerAnimationStateController.StartEquipGrimoireAnimation();
            weaponEquipped = PlayerConstants.Weapon.Grimoire;
            PlayerAnimationStateController.SetArmedStatus(true);
            grimoire.SetActive(true);
            transitioning = true;
        }

        private void DoEquipSword(InputAction.CallbackContext obj)
        {
            if (weaponEquipped == PlayerConstants.Weapon.Grimoire)
            {
                PlayerAnimationStateController.StartDequipGrimoireAnimation();
                grimoire.SetActive(false);
            }
            PlayerAnimationStateController.StartEquipSwordAnimation();
            weaponEquipped = PlayerConstants.Weapon.Sword;
            PlayerAnimationStateController.SetArmedStatus(true);
            sword.SetActive(true);
            transitioning = true;
        }

        private void DoCast(InputAction.CallbackContext obj)
        {
            if (weaponEquipped == PlayerConstants.Weapon.Grimoire && !jumping && !dodging && !casting)
            {
                casting = true;
                aoe = false;
            }
        }

        private void DoCastAOE(InputAction.CallbackContext obj)
        {
            if (weaponEquipped == PlayerConstants.Weapon.Grimoire && !jumping && !dodging)
            {
                casting = true;
                aoe = true;
            }
        }

        private void StopAttack(InputAction.CallbackContext obj)
        {
            attacking = false;
        }

        private void DoAttack(InputAction.CallbackContext obj)
        {
            if (weaponEquipped == PlayerConstants.Weapon.Sword && !dodging)
            {
                attacking = true;
            }
        }

        private void StopSprint(InputAction.CallbackContext obj)
        {
            speed /= PlayerConstants.SPRINT_MULTIPLIER_DEFAULT;
            PlayerAnimationStateController.StopSprintAnimation();
            sprinting = false;
        }

        private void DoSprint(InputAction.CallbackContext obj)
        {
            speed *= PlayerConstants.SPRINT_MULTIPLIER_DEFAULT;
            PlayerAnimationStateController.StartSprintAnimation();
            sprinting = true;
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
                PlayerAnimationStateController.StartIdleAnimation();
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
                if (grimoire == null)
                {
                    grimoire = GameObject.FindGameObjectWithTag("Grimoire");
                    grimoire.SetActive(false);
                }
                if (sword == null)
                {
                    sword = GameObject.FindGameObjectWithTag("Grimoire");
                }
                MovementHandler(moveAction);

                //Check if jumping and in what stage
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
                }
                else if (jumping)
                {
                    cumulativeJumpTime = 0;
                    jumping = false;
                }

                //Check if dodging and in what stage
                if (dodging)
                {
                    if (Time.fixedDeltaTime + cumulativeDodgeTime < (totalDodgeTime / 2))
                    {
                        Debug.Log("Dodge time: " + Time.fixedDeltaTime);
                        cumulativeDodgeTime += Time.fixedDeltaTime;
                        Vector3 target = physicalGameObject.transform.position + physicalGameObject.transform.forward * speed;
                        physicalGameObject.transform.localPosition = Vector3.MoveTowards(physicalGameObject.transform.localPosition, target, dodgeLength * cumulativeDodgeTime);
                    }
                    else if (Time.fixedDeltaTime + cumulativeDodgeTime < totalDodgeTime)
                    {
                        Debug.Log("End Dodge time: " + Time.fixedDeltaTime);
                        cumulativeDodgeTime += Time.fixedDeltaTime;
                    }
                    else
                    {
                        cumulativeDodgeTime = 0;
                        dodging = false;
                    }
                }

                if (attacking)
                {
                    //Jump attack
                    if (jumping)
                    {
                        PlayerAnimationStateController.StartJumpAttackAnimation();
                    }
                    //Run Attack
                    else if (sprinting)
                    {
                        PlayerAnimationStateController.StartRunAttackAnimation();
                    }
                    //Normal Attack Sequence
                    else
                    {
                        PlayerAnimationStateController.StartAttackAnimation();
                    }
                }


                if (casting)
                {
                    PlayerAnimationStateController.StartMagicAttackAnimation(castType, aoe);
                    CastMagic(castType, aoe);
                    casting = false;
                }

                //If not making any action then become idle
                if (!jumping && !dodging && !attacking && !casting && !crouching && moveAction.ReadValue<Vector2>().magnitude == 0)
                {
                    PlayerAnimationStateController.StartIdleAnimation();
                }
            }
        }
        public void MovementHandler(InputAction moveAction)
        {
            Vector2 movement = moveAction.ReadValue<Vector2>();
            //Debug.Log("Movement vector: " + movement);
            FacingDirection currentFacingDirection;
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

           // Debug.Log("Old facing direction: " + facingDirection + ", new facing direction: " + currentFacingDirection);

            //Check if the facing direction is the same and not idle to move
            if (currentFacingDirection.Equals(facingDirection) && !currentFacingDirection.Equals(FacingDirection.Idle))
            {
                playerState.Move();
                PlayerAnimationStateController.StartMoveAnimation();
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

        public void CastMagic(PlayerConstants.Magic castType, bool aoe)
        {
            if (castType == PlayerConstants.Magic.Wind && aoe)
                playerState.CastMagic(windAOEPrefab, aoe, castType);
            else if (castType == PlayerConstants.Magic.Wind && !aoe)
                playerState.CastMagic(windAttackPrefab, aoe, castType);
        }

    }
}
