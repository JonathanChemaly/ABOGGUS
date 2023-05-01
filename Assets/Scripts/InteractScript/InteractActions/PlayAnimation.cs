using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact
{
    public class PlayAnimation : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("interact to watch")]
        private Interactable interact;

        [SerializeField]
        [Tooltip("Animation to play when interacted with")]
        private Animation aniToPlay;

        [SerializeField]
        [Tooltip("Whether You want the animation to play multiple times")]
        private bool playMultipleTimes = false;

        [HideInInspector]
        [Tooltip("What action will be taken when animation is finished")]
        public event Action AnimationFinishAction;

        [SerializeField]
        [Tooltip("Whether You want to evoke the success actions associated with this after animation is done")]
        private bool triggerSuccessOnFinishing = true;


        [SerializeField]
        [Tooltip("Time to wait before animation plays")]
        private float delay = 0f;
        // Start is called before the first frame update
        private void Start()
        {
            interact.InteractAction += PlayAni;
        }

        //Plays animation on 
        private void PlayAni()
        {
            StartCoroutine(PlayAnimationAfterDelay());
        }

        IEnumerator PlayAnimationAfterDelay()
        {
            yield return new WaitForSeconds(delay);
            AnimationFinishAction += DebugEvent;
            aniToPlay.Play();
            StartCoroutine(disableWhileAniPlaying());
            interact.enabled = false;
        }

        private void DebugEvent()
        {
            //Debug.Log("Play Animation Event invoked");
        }

        //disables the interactable while it is animating so we do not have worry about more the one input messing with animation
        IEnumerator disableWhileAniPlaying()
        {
            while (aniToPlay.isPlaying)
            {
                yield return null;
            }
            AnimationFinishAction?.Invoke();//invoke action after finished
            if (triggerSuccessOnFinishing) interact.DoSuccesAction();
            if (playMultipleTimes)
            {
                interact.enabled = true; //do not renable if we only want to play once
            }
        }
        
    }
}

