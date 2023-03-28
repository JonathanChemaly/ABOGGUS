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

        [Tooltip("Dialogue File with text and clip to play")]
        public Dialogue dialogue;

        [SerializeField]
        [Tooltip("Dialogue File with text and clip to play")]
        private DialoguePath nextDialoguePath = null;

        private DialoguePlayer dialPlayer;

        private void Start()
        {
            interact.InteractAction += PlayAudio;
        }

        //Plays audio on interact
        private void PlayAudio()
        {
            //To play the audio we create a dialogue player object and give it the dialogue to play which plays on start
            dialPlayer = gameObject.AddComponent<DialoguePlayer>();
            dialPlayer.dialogue = dialogue; //giving the player what to play

            //we then in this case what to disable the interactable while this is playing
            StartCoroutine(DisableWhileAudioPlaying());
        }

        IEnumerator DisableWhileAudioPlaying()
        {
            interact.enabled = false;
            //Dialogue player destorys itself after it is finished playing so we can reenabel the interact after
            //the object is null
            while (dialPlayer != null)
            {
                yield return null;
            }

            //Check if we have another dialogue to play in our sequence
            if (nextDialoguePath != null) 
            {
                interact.enabled = true;
                dialogue = nextDialoguePath.curDialogue; //sets dialogue on the path as we should
                nextDialoguePath = nextDialoguePath.nextPath; //recursively gets next path (could be null or another path)
            } //do not renable if we only want to play once
        }

    }
}

