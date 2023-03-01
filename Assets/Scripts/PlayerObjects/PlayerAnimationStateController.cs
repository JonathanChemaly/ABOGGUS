using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class PlayerAnimationStateController : MonoBehaviour
    {
        static Animator animator;
        private static int idleHash, moveHash, sprintHash, crouchHash, jumpHash, dodgeHash, attackHash, magicAttackHash, airAOEHash, equipGrimoireHash, dequipGrimoireHash, equipSwordHash, dequipSwordHash;
        private static int aIdleHash, aMoveHash, aLandHash, aCrouchHash, aJumpHash, aDodgeHash, jumpAttackHash, runAttackHash;
        private static int currentState = idleHash;
        private static string[] attackAnims = { "Sword-Attack-L1", "Sword-Attack-L2", "Sword-Attack-L3", "Sword-Attack-L4", "Sword-Attack-L5", "Sword-Attack-L6" };
        private static int attackIdx;
        private static bool animationToFinish, isSprinting, armed;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            animator.fireEvents = false;
            idleHash = Animator.StringToHash("Unarmed-Idle");
            aIdleHash = Animator.StringToHash("Armed-Idle");
            moveHash = Animator.StringToHash("Unarmed-Run-Forward");
            aMoveHash = Animator.StringToHash("Armed-Run-Forward");
            sprintHash = Animator.StringToHash("Unarmed-Sprint");
            crouchHash = Animator.StringToHash("Unarmed-Idle-Crouch");
            aCrouchHash = Animator.StringToHash("Armed-Idle-Crouch");
            jumpHash = Animator.StringToHash("Unarmed-Jump");
            aJumpHash = Animator.StringToHash("Armed-Jump");
            dodgeHash = Animator.StringToHash("Unarmed-DiveRoll-Forward1");
            aDodgeHash = Animator.StringToHash("Armed-DiveRoll-Forward1");
            attackHash = Animator.StringToHash("Sword-Attack-L1");
            magicAttackHash = Animator.StringToHash("Staff-Cast-Attack3_start");
            airAOEHash = Animator.StringToHash("Staff-Cast-L-Summon1_start");
            equipGrimoireHash = Animator.StringToHash("Armed-WeaponSwitch-R-Hips");
            dequipGrimoireHash = Animator.StringToHash("Armed-Sheath-R-Hips-Unarmed");
            equipSwordHash = Animator.StringToHash("Armed-WeaponSwitch-L-Back");
            dequipSwordHash = Animator.StringToHash("Armed-Sheath-L-Back-Unarmed");
            jumpAttackHash = Animator.StringToHash("Armed-Air-Attack-L1");
            runAttackHash = Animator.StringToHash("Armed-Run-Attack-L1");
            attackIdx = 0;
        }

        public static void SetArmedStatus(bool a)
        {
            armed = a;
        }

        public static void StartIdleAnimation()
        {
            if (!animationToFinish || CheckIfAnimationIsOver())
            {
                if (armed)
                {
                    animator.Play(aIdleHash);
                    currentState = aIdleHash;
                }
                else
                {
                    animator.Play(idleHash);
                    currentState = idleHash;
                }
                animationToFinish = false;
            }
        }

        public static void StartMoveAnimation()
        {
            if (!isSprinting && (!animationToFinish || CheckIfAnimationIsOver()))
            {
                if (armed)
                {
                    animator.Play(aMoveHash);
                    currentState = aMoveHash;
                }
                else
                {
                    animator.Play(moveHash);
                    currentState = moveHash;
                }
                animationToFinish = false;
            }
            else if (isSprinting)
            {
                StartSprintAnimation();
            }
        }

        public static void StartSprintAnimation()
        {
            if (!animationToFinish || CheckIfAnimationIsOver())
            {
                animator.Play(sprintHash);
                animationToFinish = false;
                isSprinting = true;
                currentState = sprintHash;
            }
        }

        public static void StopSprintAnimation()
        {
            isSprinting = false;
            if (!animationToFinish || CheckIfAnimationIsOver())
            {
                currentState = moveHash;
                animationToFinish = false;
                animator.Play(moveHash);
            }
        }

        public static void StartAttackAnimation()
        {
            if (!animationToFinish || CheckIfAnimationIsOver())
            {
                animator.Play(attackAnims[SwitchAttackAnimation()]);
                animationToFinish = true;
                currentState = attackHash;
            }
        }

        public static void StartCrouchAnimation()
        {

            if (!animationToFinish || CheckIfAnimationIsOver())
            {
                if (armed)
                {
                    animator.Play(aCrouchHash);
                    currentState = aCrouchHash;
                }
                else
                {
                    animator.Play(crouchHash);
                    currentState = crouchHash;
                }
                animationToFinish = true;
            }
        }

        public static void StartJumpAnimation()
        {
            if (!animationToFinish || CheckIfAnimationIsOver())
            {
                if (armed)
                {
                    animator.Play(aJumpHash);
                    currentState = aJumpHash;
                }
                else
                {
                    animator.Play(jumpHash);
                    currentState = jumpHash;
                }
                animationToFinish = true;
            }
        }

        public static void StartDodgeAnimation()
        {
            if (!animationToFinish || CheckIfAnimationIsOver())
            {
                if (armed)
                {
                    animator.Play(aDodgeHash);
                    currentState = aDodgeHash;
                }
                else
                {
                    animator.Play(dodgeHash);
                    currentState = dodgeHash;
                }
                animationToFinish = true;
            }
        }

        public static void StartMagicAttackAnimation(PlayerConstants.Magic castType, bool aoe)
        {
            if ((!animationToFinish || CheckIfAnimationIsOver()) && !aoe)
            {
                animator.Play(magicAttackHash);
                currentState = magicAttackHash;
            }
            else if ((!animationToFinish || CheckIfAnimationIsOver()) && aoe && castType == PlayerConstants.Magic.Wind)
            {
                animator.Play(airAOEHash);
                currentState = airAOEHash;
            }
            animationToFinish = true;
        }

        public static void StartEquipGrimoireAnimation()
        {
            if (!animationToFinish || CheckIfAnimationIsOver())
            {
                animator.Play(equipGrimoireHash);
                animationToFinish = true;
                currentState = equipGrimoireHash;
            }
            else if (currentState == dequipSwordHash)
            {
                animator.CrossFade(equipGrimoireHash, 1f, 0);
                animationToFinish = true;
                currentState = equipGrimoireHash;
            }
        }

        public static void StartDequipGrimoireAnimation()
        {
            if (!animationToFinish || CheckIfAnimationIsOver())
            {
                animator.Play(dequipGrimoireHash);
                animationToFinish = true;
                currentState = dequipGrimoireHash;
            }
        }

        public static void StartEquipSwordAnimation()
        {
            if (!animationToFinish || CheckIfAnimationIsOver())
            {
                animator.Play(equipSwordHash);
                animationToFinish = true;
                currentState = equipSwordHash;
            }
            else if (currentState == dequipGrimoireHash)
            {
                animator.CrossFade(equipSwordHash, 1f, 0);
                animationToFinish = true;
                currentState = equipSwordHash;
            }
        }

        public static void StartDequipSwordAnimation()
        {
            if (!animationToFinish || CheckIfAnimationIsOver())
            {
                animator.Play(dequipSwordHash);
                animationToFinish = true;
                currentState = dequipSwordHash;
            }
        }

        public static void StartJumpAttackAnimation()
        {
            animator.Play(jumpAttackHash);
            animationToFinish = true;
            currentState = jumpAttackHash;
        }

        public static void StartRunAttackAnimation()
        {
            animator.Play(runAttackHash);
            animationToFinish = true;
            currentState = runAttackHash;
        }

        public static void StopAnimation()
        {
            animator.StopPlayback();
            animationToFinish = false;
            currentState = idleHash;
        }


        private static bool CheckIfAnimationIsOver()
        {
            animationToFinish = false;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
            {
                animationToFinish = true;
            }
            return !animationToFinish;
        }

        private static int SwitchAttackAnimation()
        {
            if (!(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f))
            {
                attackIdx++;
                if (attackIdx >= attackAnims.Length)
                    attackIdx = 0;
            }
            return attackIdx;
        }
    }
}
