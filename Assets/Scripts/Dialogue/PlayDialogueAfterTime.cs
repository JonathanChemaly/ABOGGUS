using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.Input;
using ABOGGUS.Sound.Dialogue;
using System;

namespace ABOGGUS.Interact
{
    public class PlayDialogueAfterTime : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Time until the dialogue should play")]
        private float timeUntilPlay;

        [SerializeField]
        [Tooltip("Dialogue File with text and clip to play")]
        private Dialogue dialogue;

        [SerializeField]
        [Tooltip("Dialogue File with text and clip to play")]
        private DialoguePath nextDialoguePath = null;

        private DialoguePlayer dialPlayer;

        private void Start()
        {
            StartCoroutine(WaitThenPlay());
        }

        IEnumerator WaitThenPlay()
        {
            yield return new WaitForSeconds(timeUntilPlay);            

            //To play the audio we create a dialogue player object and give it the dialogue to play which plays on start
            dialPlayer = gameObject.AddComponent<DialoguePlayer>();
            dialPlayer.dialogue = dialogue; //giving the player what to play

            //if we do not have another dialogue to play in our sequence
            if (nextDialoguePath == null)
            {
                //destory this object
                Destroy(this);
            }
            else //we do have another dialogue on our path
            {
                dialogue = nextDialoguePath.curDialogue; //sets dialogue on the path as we should
                nextDialoguePath = nextDialoguePath.nextPath; //recursively gets next path (could be null or another path)
            }
        }

    }
}

