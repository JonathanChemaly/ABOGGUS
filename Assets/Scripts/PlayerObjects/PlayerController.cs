using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ABOGGUS.Input;
using System;
using UnityEngine.Playables;
using ABOGGUS.Gameplay;
using ABOGGUS.PlayerObjects.Items;
using ABOGGUS.Interact.Puzzles;

namespace ABOGGUS.PlayerObjects
{
    public class PlayerController : MonoBehaviour
    {

        private GameObject physicalGameObject;
        //Change to event triggers later
        private Grimoire grimoire;
        private SwordAttack sword;
        private SpearAttack spear;
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
        private bool attack = false;
        private bool isAttacking = false;
        private bool attackInvoked = false;
        private bool sprinting = false;
        private bool crouching = false;
        private bool casting = false;
        private bool magicInvoked = false;
        private bool aoe = false;
        private bool transitioning = false;
        private bool transitionInvoked = false;
        private bool animationSelectedThisFrame = false;
        private bool firstTransition = true;
        private int attackIdx = 0;
        private PlayerConstants.Magic castType = PlayerConstants.Magic.Wind;
        private PlayerConstants.Weapon weaponEquipped = PlayerConstants.Weapon.Sword;
        private PlayerConstants.Weapon lastWeaponEquipped;

        private IPlayerState playerState;
        public enum FacingDirection { Forward, Backward, Left, Right, FrontRight, FrontLeft, BackRight, BackLeft, Idle };
        private FacingDirection facingDirection;

        // disables forward movement if the player is facing a wall
        private bool canMoveWalls = true;
        private LayerMask walls;
        private float checkDistance = 2.0f;
        //adjusts raycast to start at a different height
        private Vector3 yWallCheck = new Vector3(0, 1, 0);

        public void InitializeForPlayer()
        {
            walls = LayerMask.GetMask("Wall");
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

            playerActions.Player.EquipSpear.performed += DoEquipSpear;
            playerActions.Player.EquipSpear.Enable();

            playerActions.Player.Dequip.performed += DoDequip;
            playerActions.Player.Dequip.Enable();

            playerActions.Player.EquipWind.performed += DoEquipWind;
            playerActions.Player.EquipWind.Enable();

            playerActions.Player.EquipFire.performed += DoEquipFire;
            playerActions.Player.EquipFire.Enable();

            playerActions.Player.EquipWater.performed += DoEquipWater;
            playerActions.Player.EquipWater.Enable();

            playerActions.Player.EquipNature.performed += DoEquipNature;
            playerActions.Player.EquipNature.Enable();

            playerActions.Player.EquipLightning.performed += DoEquipLightning;
            playerActions.Player.EquipLightning.Enable();

            playerActions.Player.NextRun.performed += DoNextRun;
            playerActions.Player.NextRun.Enable();
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
                lastWeaponEquipped = PlayerConstants.Weapon.Sword;
            }
            else if (weaponEquipped == PlayerConstants.Weapon.Grimoire)
            {
                lastWeaponEquipped = PlayerConstants.Weapon.Grimoire;
            }
            else if (weaponEquipped == PlayerConstants.Weapon.Spear)
            {
                lastWeaponEquipped = PlayerConstants.Weapon.Spear;
            }
            weaponEquipped = PlayerConstants.Weapon.Unarmed;
            transitioning = true;
        }

        private void DoEquipGrimoire(InputAction.CallbackContext obj)
        {
            if (GameController.player.inventory.HasItem(ItemLookup.GrimoireName))
            {
                if (weaponEquipped == PlayerConstants.Weapon.Sword)
                {
                    lastWeaponEquipped = PlayerConstants.Weapon.Sword;
                }
                else if (weaponEquipped == PlayerConstants.Weapon.Spear)
                {
                    lastWeaponEquipped = PlayerConstants.Weapon.Spear;
                }
                else
                {
                    lastWeaponEquipped = PlayerConstants.Weapon.Unarmed;
                }
                weaponEquipped = PlayerConstants.Weapon.Grimoire;
                transitioning = true;
            }
        }

        private void DoEquipSword(InputAction.CallbackContext obj)
        {
            if (weaponEquipped == PlayerConstants.Weapon.Grimoire)
            {
                lastWeaponEquipped = PlayerConstants.Weapon.Grimoire;
            }
            else if (weaponEquipped == PlayerConstants.Weapon.Spear)
            {
                lastWeaponEquipped = PlayerConstants.Weapon.Spear;
            }
            else
            {
                lastWeaponEquipped = PlayerConstants.Weapon.Unarmed;
            }
            weaponEquipped = PlayerConstants.Weapon.Sword;
            transitioning = true;
        }

        private void DoEquipSpear(InputAction.CallbackContext obj)
        {
            if (weaponEquipped == PlayerConstants.Weapon.Grimoire)
            {
                lastWeaponEquipped = PlayerConstants.Weapon.Grimoire;
            }
            else if (weaponEquipped == PlayerConstants.Weapon.Sword)
            {
                lastWeaponEquipped = PlayerConstants.Weapon.Sword;
            }
            else
            {
                lastWeaponEquipped = PlayerConstants.Weapon.Unarmed;
            }
            weaponEquipped = PlayerConstants.Weapon.Spear;
            transitioning = true;
        }

        private void DoEquipWind(InputAction.CallbackContext obj)
        {
            Player.WeaponChanged();
            castType = PlayerConstants.Magic.Wind;
            grimoire.SetNewMaterial(castType);
        }

        private void DoEquipFire(InputAction.CallbackContext obj)
        {
            Player.WeaponChanged();
            castType = PlayerConstants.Magic.Fire;
            grimoire.SetNewMaterial(castType);
        }

        private void DoEquipWater(InputAction.CallbackContext obj)
        {
            Player.WeaponChanged();
            castType = PlayerConstants.Magic.Water;
            grimoire.SetNewMaterial(castType);
        }

        private void DoEquipNature(InputAction.CallbackContext obj)
        {
            Player.WeaponChanged();
            castType = PlayerConstants.Magic.Nature;
            grimoire.SetNewMaterial(castType);
        }

        private void DoEquipLightning(InputAction.CallbackContext obj)
        {
            Player.WeaponChanged();
            castType = PlayerConstants.Magic.Lightning;
            grimoire.SetNewMaterial(castType);
        }

        private void DoCast(InputAction.CallbackContext obj)
        {
            if (weaponEquipped == PlayerConstants.Weapon.Grimoire && !jumping && !dodging && !casting && SpellUnlocked())
            {
                casting = true;
                aoe = false;
            }
        }

        private void DoCastAOE(InputAction.CallbackContext obj)
        {
            if (weaponEquipped == PlayerConstants.Weapon.Grimoire && !jumping && !dodging && AOESpellUnlocked())
            {
                casting = true;
                aoe = true;
            }
        }

        private void StopAttack(InputAction.CallbackContext obj)
        {
            attack = false;
        }

        private void DoAttack(InputAction.CallbackContext obj)
        {
            if ((weaponEquipped == PlayerConstants.Weapon.Sword || weaponEquipped == PlayerConstants.Weapon.Spear) && !dodging)
            {
                attack = true;
                sword.Attacking(attack);
                spear.Attacking(attack);
            }
        }

        private void StopSprint(InputAction.CallbackContext obj)
        {
            speed = PlayerConstants.SPEED_DEFAULT;
            sprinting = false;
        }

        private void DoSprint(InputAction.CallbackContext obj)
        {
            speed = PlayerConstants.SPRINT_MULTIPLIER_DEFAULT*PlayerConstants.SPEED_DEFAULT;
            sprinting = true;
        }

        private void DoCrouch(InputAction.CallbackContext obj)
        {
            crouching = !crouching;
        }

        private void DoJump(InputAction.CallbackContext obj)
        {
            if (!jumping && !dodging)
            {
                jumping = true;
            }
        }

        private void DoDodge(InputAction.CallbackContext obj)
        {
            if (!dodging && !jumping)
            {
                dodging = true;
            }
        }

        private void DoNextRun(InputAction.CallbackContext obj)
        {
            UpgradeStats.runs++;
            //TreePuzzle.test++;
        }

        private void OnDisable()
        {
            moveAction.Disable();
        }

        public void _FixedUpdate()
        {
            if (physicalGameObject != null)
            {
                if (!transitioning)
                    CheckIfProperWeaponEquipped();
                if (GameController.scene.Equals(GameConstants.SCENE_BOSS) && firstTransition)
                {
                    playerState = new PlayerFacingBackward(this);
                    firstTransition = false;
                }
                else if (!GameController.scene.Equals(GameConstants.SCENE_BOSS))
                {
                    firstTransition = true;
                }
                // create a raycast that detects walls
                RaycastHit hit;
                if (Physics.Raycast(physicalGameObject.transform.position, physicalGameObject.transform.forward + yWallCheck, out hit, checkDistance, walls))
                {
                    // if a wall is detected, disable forward movement
                    canMoveWalls = false;
                    Debug.Log("Wall");
                }
                else
                {
                    // else enable forward movement
                    canMoveWalls = true;
                    Debug.Log("NO");
                }
                if (transitioning)
                {
                    if (!transitionInvoked)
                    {
                        //Add logic for playing equipping/dequipping weapons
                        if (lastWeaponEquipped == PlayerConstants.Weapon.Sword)
                        {
                            PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.DEQUIP_SWORD);
                            Invoke(nameof(EquipWeapon), PlayerConstants.TRANSITION_DELAY);
                        }
                        else if (lastWeaponEquipped == PlayerConstants.Weapon.Spear)
                        {
                            PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.DEQUIP_SPEAR);
                            Invoke(nameof(EquipWeapon), PlayerConstants.TRANSITION_DELAY);
                        }
                        else if (lastWeaponEquipped == PlayerConstants.Weapon.Grimoire)
                        {
                            PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.DEQUIP_GRIMOIRE);
                            Invoke(nameof(EquipWeapon), PlayerConstants.TRANSITION_DELAY);
                        }
                        else
                        {
                            if (weaponEquipped == PlayerConstants.Weapon.Sword)
                            {
                                PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.EQUIP_SWORD);
                            }
                            else if (weaponEquipped == PlayerConstants.Weapon.Spear)
                            {
                                PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.EQUIP_SPEAR);
                            }
                            else if (weaponEquipped == PlayerConstants.Weapon.Grimoire)
                            {
                                PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.EQUIP_GRIMOIRE);
                            }
                            Invoke(nameof(EndTransition), PlayerConstants.TRANSITION_DELAY);
                        }
                        transitionInvoked = true;
                    }
                }
                else
                {
                    if (grimoire == null)
                        grimoire = physicalGameObject.GetComponentInChildren<Grimoire>();
                    if (sword == null)
                        sword = physicalGameObject.GetComponentInChildren<SwordAttack>();
                    if (spear == null)
                        spear = physicalGameObject.GetComponentInChildren<SpearAttack>();

                    MovementHandler(moveAction);

                    animationSelectedThisFrame = false;

                    //Check if jumping and in what stage
                    if (jumping)
                    {
                        if (Time.fixedDeltaTime + cumulativeJumpTime < (totalJumpTime / 2))
                        {
                            //Jump Up
                            //Debug.Log("Jump time: " + Time.fixedDeltaTime);
                            cumulativeJumpTime += Time.fixedDeltaTime;
                            Vector3 up = new Vector3(physicalGameObject.transform.position.x, jumpHeight * cumulativeJumpTime + physicalGameObject.transform.position.y, physicalGameObject.transform.position.z);
                            physicalGameObject.transform.localPosition = Vector3.MoveTowards(physicalGameObject.transform.localPosition, up, jumpHeight * cumulativeJumpTime);
                            //Jump Anim
                            if (!attack && !isAttacking)
                            {
                                if (weaponEquipped == PlayerConstants.Weapon.Unarmed)
                                {
                                    PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.UNARMED_JUMP);
                                }
                                else
                                {
                                    PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.ARMED_JUMP);
                                }
                            }
                            animationSelectedThisFrame = true;
                        }
                        else if (Time.fixedDeltaTime + cumulativeJumpTime < totalJumpTime)
                        {
                            //Fall
                            //Debug.Log("End jump time: " + Time.fixedDeltaTime);
                            cumulativeJumpTime += Time.fixedDeltaTime;
                            //Jump Anim
                            animationSelectedThisFrame = true;
                        }
                        else
                        {
                            //On the ground
                            cumulativeJumpTime = 0;
                            jumping = false;
                        }
                    }

                    //Check if dodging and in what stage
                    if (dodging)
                    {
                        if (Time.fixedDeltaTime + cumulativeDodgeTime < (totalDodgeTime / 2))
                        {
                            //Dive
                            //Debug.Log("Dodge time: " + Time.fixedDeltaTime);
                            cumulativeDodgeTime += Time.fixedDeltaTime;
                            Vector3 target = physicalGameObject.transform.position + physicalGameObject.transform.forward * speed;
                            physicalGameObject.transform.localPosition = Vector3.MoveTowards(physicalGameObject.transform.localPosition, target, dodgeLength * cumulativeDodgeTime);
                            //Dodge Anim
                            if (weaponEquipped == PlayerConstants.Weapon.Unarmed)
                            {
                                PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.UNARMED_DODGE);
                            }
                            else
                            {
                                PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.ARMED_DODGE);
                            }
                            animationSelectedThisFrame = true;
                        }
                        else if (Time.fixedDeltaTime + cumulativeDodgeTime < totalDodgeTime)
                        {
                            //Roll
                            //Debug.Log("End Dodge time: " + Time.fixedDeltaTime);
                            cumulativeDodgeTime += Time.fixedDeltaTime;
                            //Dodge Anim
                            animationSelectedThisFrame = true;
                        }
                        else
                        {
                            //End
                            cumulativeDodgeTime = 0;
                            dodging = false;
                        }
                    }

                    if (attack)
                    {
                        //Jump attack
                        if (jumping)
                        {
                            //Jump attack Anim
                            if (weaponEquipped == PlayerConstants.Weapon.Sword)
                                PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.JUMP_SWORD_ATTACK);
                            else
                                PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.JUMP_SPEAR_ATTACK);
                            isAttacking = true;
                            if (!attackInvoked)
                            {
                                Invoke(nameof(AttackAgain), PlayerConstants.JUMP_ATTACK_DELAY);
                                attackInvoked = true;
                            }
                        }
                        //Run Attack
                        else if (moveAction.ReadValue<Vector2>().magnitude > 0.5)
                        {
                            //Run Attack Anim
                            if (weaponEquipped == PlayerConstants.Weapon.Sword)
                                PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.DASH_SWORD_ATTACK);
                            else
                                PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.DASH_SPEAR_ATTACK);
                            isAttacking = true;
                            if (!attackInvoked)
                            {
                                Invoke(nameof(AttackAgain), PlayerConstants.ATTACK_DELAY);
                                attackInvoked = true;
                            }
                        }
                        //Normal Attack Sequence
                        else
                        {
                            //Attack Anim
                            if (weaponEquipped == PlayerConstants.Weapon.Sword)
                                PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.SWORD_ATTACKS[attackIdx]);
                            else
                                PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.SPEAR_ATTACKS[attackIdx]);
                            isAttacking = true;
                            if (!attackInvoked)
                            {
                                Invoke(nameof(ChangeAttack), PlayerConstants.ATTACK_DELAY);
                                attackInvoked = true;
                            }
                        }
                        animationSelectedThisFrame = true;
                    }
                    else if (isAttacking)
                    {
                        animationSelectedThisFrame = true;
                    }
                    else
                    {
                        attackIdx = 0;
                        sword.Attacking(isAttacking);
                        spear.Attacking(isAttacking);
                    }

                    if (casting)
                    {
                        float delay;
                        Debug.Log("Entered");
                        if (aoe)
                        {
                            //AOE Animation
                            PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.MAGIC_AOE_ATTACK);
                            delay = PlayerConstants.MAGIC_AOE_ATTACK_DELAY;
                        }
                        else
                        {
                            //Directional Animation
                            PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.MAGIC_ATTACK);
                            delay = PlayerConstants.MAGIC_ATTACK_DELAY;
                        }
                        if (!magicInvoked)
                        {
                            Invoke("CastMagic", delay);
                            magicInvoked = true;
                        }
                        animationSelectedThisFrame = true;
                    }

                    if (!animationSelectedThisFrame && moveAction.ReadValue<Vector2>().magnitude > 0.5 && sprinting)
                    {
                        //Sprint
                        PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.SPRINT_ANIMATION);
                    }
                    else if (!animationSelectedThisFrame && moveAction.ReadValue<Vector2>().magnitude > 0.5)
                    {
                        //Run
                        if (weaponEquipped == PlayerConstants.Weapon.Unarmed)
                        {
                            PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.UNARMED_RUN_F);
                        }
                        else
                        {
                            PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.ARMED_RUN_F);
                        }
                    }
                    else if (!animationSelectedThisFrame && crouching)
                    {
                        //Idle
                        if (weaponEquipped == PlayerConstants.Weapon.Unarmed)
                        {
                            PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.UNARMED_CROUCH);
                        }
                        else
                        {
                            PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.ARMED_CROUCH);
                        }
                    }
                    else if (!animationSelectedThisFrame)
                    {
                        //Idle
                        if (weaponEquipped == PlayerConstants.Weapon.Unarmed)
                        {
                            PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.UNARMED_IDLE);
                        }
                        else
                        {
                            PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.ARMED_IDLE);
                        }
                    }
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
            if (currentFacingDirection.Equals(facingDirection) && !currentFacingDirection.Equals(FacingDirection.Idle) && canMoveWalls)
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

        public void CastMagic()
        {
            if (castType == PlayerConstants.Magic.Wind && aoe)
                playerState.CastMagic(grimoire.windAOEPrefab, aoe, castType);
            else if (castType == PlayerConstants.Magic.Wind && !aoe)
                playerState.CastMagic(grimoire.windAttackPrefab, aoe, castType);
            else if (castType == PlayerConstants.Magic.Fire && aoe)
                playerState.CastMagic(grimoire.fireAOEPrefab, aoe, castType);
            else if (castType == PlayerConstants.Magic.Fire && !aoe)
                playerState.CastMagic(grimoire.fireAttackPrefab, aoe, castType);
            else if (castType == PlayerConstants.Magic.Nature && aoe)
                UnityEngine.Object.Instantiate(grimoire.natureAOEPrefab, physicalGameObject.transform.position, physicalGameObject.transform.rotation);
            else if (castType == PlayerConstants.Magic.Nature && !aoe)
            {
                UnityEngine.Object.Instantiate(grimoire.natureAttackPrefab, physicalGameObject.transform.position, physicalGameObject.transform.rotation, physicalGameObject.transform);
                GameController.player.SetResistance(true);
            }
            else if (castType == PlayerConstants.Magic.Water && aoe)
                playerState.CastMagic(grimoire.waterAOEPrefab, aoe, castType);
            else if (castType == PlayerConstants.Magic.Water && !aoe)
                playerState.CastMagic(grimoire.waterAttackPrefab, aoe, castType);
            magicInvoked = false;
            casting = false;
        }

        private void StopAttacking()
        {
            isAttacking = false;
        }

        private void ChangeAttack()
        {
            if (attack)
            {
                Debug.Log("Entering new attack");
                attackIdx = (attackIdx + 1) % PlayerConstants.SWORD_ATTACKS.Length;
            }
            else
            {
                StopAttacking();
                attackIdx = 0;
            }
            attackInvoked = false;
        }

        private void AttackAgain()
        {
            if (attack)
            {
                PlayerAnimationStateController.PlayCurrentState();
            }
            else
            {
                StopAttacking();
            }
            attackInvoked = false;
        }

        private void EquipWeapon()
        {
            if (lastWeaponEquipped == PlayerConstants.Weapon.Sword)
            {
                sword.Unequip();
            }
            else if (lastWeaponEquipped == PlayerConstants.Weapon.Grimoire)
            {
                grimoire.Unequip();
            }
            else if (lastWeaponEquipped == PlayerConstants.Weapon.Spear)
            {
                spear.Unequip();
            }

            if (weaponEquipped == PlayerConstants.Weapon.Sword)
            {
                Player.WeaponChanged();
                PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.EQUIP_SWORD);
            }
            else if (weaponEquipped == PlayerConstants.Weapon.Grimoire)
            {
                Player.WeaponChanged();
                PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.EQUIP_GRIMOIRE);
            }
            else if (weaponEquipped == PlayerConstants.Weapon.Spear)
            {
                Player.WeaponChanged();
                PlayerAnimationStateController.ChangeAnimationState(PlayerConstants.EQUIP_SPEAR);
            }
            Invoke(nameof(EndTransition), PlayerConstants.TRANSITION_DELAY);
        }

        private void EndTransition()
        {
            if (weaponEquipped == PlayerConstants.Weapon.Sword)
            {
                sword.Equip();
            }
            else if (weaponEquipped == PlayerConstants.Weapon.Grimoire)
            {
                grimoire.Equip();
            }
            else if (weaponEquipped == PlayerConstants.Weapon.Spear)
            {
                spear.Equip();
            }
            transitioning = false;
            transitionInvoked = false;
        }

        private void ChangeMaterial()
        {
            physicalGameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = grimoire.normalMaterial;
            GameController.player.SetResistance(false);
        }

        public PlayerConstants.Weapon GetCurrentWeapon()
        {
            return this.weaponEquipped;
        }

        public PlayerConstants.Magic GetCurrentMagic()
        {
            return this.castType;
        }

        private bool AOESpellUnlocked()
        {
            bool unlocked = false;
            if (castType == PlayerConstants.Magic.Wind && GameConstants.windAOEUnlocked)
            {
                unlocked = true;
            }
            else if (castType == PlayerConstants.Magic.Water && GameConstants.waterAOEUnlocked)
            {
                unlocked = true;
            }
            else if (castType == PlayerConstants.Magic.Nature && GameConstants.natureAOEUnlocked)
            {
                unlocked = true;
            }
            else if  (castType == PlayerConstants.Magic.Fire && GameConstants.fireAOEUnlocked)
            {
                unlocked = true;
            }
            return unlocked;
        }

        private bool SpellUnlocked()
        {
            bool unlocked = false;
            if (castType == PlayerConstants.Magic.Wind && GameConstants.windUnlocked)
            {
                unlocked = true;
            }
            else if (castType == PlayerConstants.Magic.Water && GameConstants.waterUnlocked)
            {
                unlocked = true;
            }
            else if (castType == PlayerConstants.Magic.Nature && GameConstants.natureUnlocked)
            {
                unlocked = true;
            }
            else if (castType == PlayerConstants.Magic.Fire && GameConstants.fireUnlocked)
            {
                unlocked = true;
            }
            return unlocked;
        }

        public FacingDirection GetFacingDirection()
        {
            return facingDirection;
        }

        private void CheckIfProperWeaponEquipped()
        {
            if (grimoire != null && weaponEquipped == PlayerConstants.Weapon.Grimoire && !grimoire.GetStatus())
            {
                /*
                transitioning = true;
                if (sword.GetStatus())
                {
                    lastWeaponEquipped = PlayerConstants.Weapon.Sword;
                }
                else if (spear.GetStatus())
                {
                    lastWeaponEquipped = PlayerConstants.Weapon.Spear;
                }
                else
                {
                    lastWeaponEquipped = PlayerConstants.Weapon.Unarmed;
                }
                */
                grimoire.Equip();
                grimoire.SetNewMaterial(castType);
                sword.Unequip();
                spear.Unequip();
            }
            else if (sword != null && weaponEquipped == PlayerConstants.Weapon.Sword && !sword.GetStatus())
            {
                /*
                transitioning = true;
                if (grimoire.GetStatus())
                {
                    lastWeaponEquipped = PlayerConstants.Weapon.Grimoire;
                }
                else if (spear.GetStatus())
                {
                    lastWeaponEquipped = PlayerConstants.Weapon.Spear;
                }
                else
                {
                    lastWeaponEquipped = PlayerConstants.Weapon.Unarmed;
                }
                */
                grimoire.Unequip();
                sword.Equip();
                spear.Unequip();
            }
            else if (spear != null && weaponEquipped == PlayerConstants.Weapon.Spear && !spear.GetStatus())
            {
                /*
                transitioning = true;
                if (sword.GetStatus())
                {
                    lastWeaponEquipped = PlayerConstants.Weapon.Sword;
                }
                else if (grimoire.GetStatus())
                {
                    lastWeaponEquipped = PlayerConstants.Weapon.Grimoire;
                }
                else
                {
                    lastWeaponEquipped = PlayerConstants.Weapon.Unarmed;
                }
                */
                grimoire.Unequip();
                sword.Unequip();
                spear.Equip();
            }
        }


    }
}
