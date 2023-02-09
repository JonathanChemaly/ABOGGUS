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

        }

        //disables the interactable while it is animating so we do not have worry about more the one input messing with animation
        IEnumerator disableWhileAniPlaying()
        {
            interact.enabled = false;
            while (aniToPlay.isPlaying)
            {
                yield return null;
            }
            if(!playMultipleTimes)interact.enabled = true; //do not renable if we only want to play once
            aniToPlay.Rewind();
        }
        
    }
}

