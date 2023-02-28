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

        [SerializeField]
        [Tooltip("Whether You want to evoke the success actions associated with this after animation is done")]
        private bool triggerSuccessOnFinishing = true;

        private bool playFoward = true;
        // Start is called before the first frame update
        private void Start()
        {
            interact.InteractAction += PlayAni;
        }

        //Plays animation on 
        private void PlayAni()
        {
            aniToPlay.Play();
            StartCoroutine(disableWhileAniPlaying());
            interact.enabled = false;

        }

        //disables the interactable while it is animating so we do not have worry about more the one input messing with animation
        IEnumerator disableWhileAniPlaying()
        {
            while (aniToPlay.isPlaying)
            {
                yield return null;
            }
            if (triggerSuccessOnFinishing) interact.DoSuccesAction();
            if (playMultipleTimes)interact.enabled = true; //do not renable if we only want to play once
            
        }
        
    }
}

