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
        [Tooltip("path to audio to play when interacted with")]
        private string pathToAudio;

        [SerializeField]
        [Tooltip("Text of dialogue to ouput as subtitles")]
        private List<DialogueRecord> subtitleText;

        [SerializeField]
        [Tooltip("Whether You want the animation to play multiple times")]
        private bool playMultipleTimes = true;

        private Dialogue dial;

        private void Start()
        {
            interact.InteractAction += PlayAudio;
        }

        //Plays audio on interact
        private void PlayAudio()
        {
            dial = gameObject.AddComponent<Dialogue>();
            dial.locationOfSoundFile = pathToAudio;
            dial.subtitleText = subtitleText;

            StartCoroutine(disableWhileAudioPlaying());
        }

        IEnumerator disableWhileAudioPlaying()
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

