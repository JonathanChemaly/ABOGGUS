using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.Input;
using ABOGGUS.Sound.Dialogue;
using System;

namespace ABOGGUS.Interact
{
    public class PlayDialogueOnInteract : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("interact to watch")]
        private Interactable interact;

        [SerializeField]
        [Tooltip("Dialogue File with text and clip to play")]
        private Dialogue dialogue;

        [SerializeField]
        [Tooltip("Whether You want the animation to play multiple times")]
        private bool playMultipleTimes = true;

        private DialoguePlayer dial;

        private void Start()
        {
            interact.InteractAction += PlayAudio;
        }

        //Plays audio on interact
        private void PlayAudio()
        {
            dial = gameObject.AddComponent<DialoguePlayer>();
            dial.audioClip = dialogue.audioClip;
            dial.subtitleText = dialogue.subtitleText;

            StartCoroutine(DisableWhileAudioPlaying());
        }

        IEnumerator DisableWhileAudioPlaying()
        {
            interact.enabled = false;
            while (dial != null)
            {
                yield return null;
            }
            if (playMultipleTimes) interact.enabled = true; //do not renable if we only want to play once
        }

    }
}

