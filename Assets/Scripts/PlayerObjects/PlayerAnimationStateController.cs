using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class PlayerAnimationStateController : MonoBehaviour
    {
        static Animator animator;
        private static string currentState = "";
        private static int attackIdx;
        private static bool animationToFinish, isSprinting, armed;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            animator.fireEvents = false;
            attackIdx = 0;
        }

        public static void ChangeAnimationState(string newState)
        {
            Debug.Log("New State: " + newState);
            if (!currentState.Equals(newState))
            {
                animator.Play(newState);
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
