using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.Input;

namespace ABOGGUS.Interact
{
    public class PlayAudioOnInteract : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("interact to watch")]
        private Interactable interact;

        [SerializeField]
        [Tooltip("Audio to play when interacted with")]
        private AudioSource audioToPlay;

        [SerializeField]
        [Tooltip("Whether You want the animation to play multiple times")]
        private bool playMultipleTimes = true;

        [SerializeField]
        [Tooltip("Give Input manager if you want player actions to be disabled on interact")]
        private InputManager IM = null;

        private void Start()
        {
            interact.InteractAction += PlayAudio;
        }

        //Plays audio on interact
        private void PlayAudio()
        {
            if (IM != null) IM.InputScheme.Player.Disable();//if given IM disable player actions
            Debug.Log("playing audio for " + this.gameObject.name);
            audioToPlay.Play();
            StartCoroutine(disableWhileAudioPlaying());

        }

        //disables the interactable while it is animating so we do not have worry about more the one input messing with animation
        IEnumerator disableWhileAudioPlaying()
        {
            interact.enabled = false;
            while (audioToPlay.isPlaying)
            {
                yield return null;
            }
            if (!playMultipleTimes) interact.enabled = true; //do not renable if we only want to play once
            if (IM != null) IM.InputScheme.Player.Enable();//if given IM disable player actions
        }
    }
}

