using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.BossObjects
{
    public class BossAnimator : MonoBehaviour
    {
        //public static Action AttackAnimationFinished;
        static Animator animator;
        public static string currentState = "";
        private static int direction;
        private static int attackType;
        private static bool animationToFinish, isMoving, isAttacking;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            animator.fireEvents = false;
            attackType = 0;
        }

        private void FixedUpdate()
        {
        }

        public static void UpdateAnimation(bool finish, bool move, bool attack, int test)
        {
            if (move)
            {
                isMoving = move;
                isAttacking = false;
                direction = test;
                attackType = -1;
            } else if (attack)
            {
                isMoving = false;
                isAttacking = attack;
                direction = -1;
                attackType = test;
            } else
            {
                isMoving = false;
                isAttacking = false;
                direction = -1;
                attackType = -1;
            }

            UpdateAnimator();
        }

        private static void UpdateAnimator()
        {
            animator.SetBool(BossConstants.ANIMATOR_MOVE, isMoving);
            animator.SetBool(BossConstants.ANIMATOR_ATTACK, isAttacking);
            animator.SetInteger(BossConstants.ANIMATOR_DIRECTION, direction);
            animator.SetInteger(BossConstants.ANIMATOR_ATTACKTYPE, attackType);
        }

        public static void ChangeAnimationState(string newState)
        {
            //Debug.Log("New State: " + newState);
            if (!currentState.Equals(newState))
            {
                animator.Play(newState);
                currentState = newState;
            }
        }

        public static float GetCurrentAnimLength()
        {
            return animator.GetCurrentAnimatorStateInfo(0).length;
        }

        public static void PlayCurrentState()
        {
            animator.Play(currentState);
        }
    }
}