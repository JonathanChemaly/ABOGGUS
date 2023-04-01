using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ABOGGUS.Sound.Dialogue
{
    /**
     * Class to be created with a dialogue should be played
     * 
     * see "PlayDialogueOnInteract.cs" for how to use
     */
    public class DialoguePlayer: MonoBehaviour
    {
        [HideInInspector]
        public Dialogue dialogue;

        /**
         * Creates new dialogue object and plays audio and 
         */
        private void Start()
        {
            StartCoroutine(PlayAudioAndDisplaySubtitles());
        }

        IEnumerator PlayAudioAndDisplaySubtitles()
        {
            int lineNumBeingRead = 0;

            AudioSource source = gameObject.AddComponent<AudioSource>();

            source.clip = dialogue.audioClip;

            source.Play();

            List<DialogueLine> subtitleText = dialogue.subtitleText;

            float startTime = Time.time;
            float timeTillNextLine = subtitleText[lineNumBeingRead].timeToPlay;

            if (DialogueController.SubtitlesEnabled)
            {
                DialogueController.subtitleTMP.text = subtitleText[lineNumBeingRead].line;
            }

            while (source.isPlaying)
            {
                yield return null;
                if(DialogueController.SubtitlesEnabled && lineNumBeingRead != subtitleText.Count - 1 && Time.time >= startTime + timeTillNextLine )
                {
                    lineNumBeingRead++;
                    DialogueController.subtitleTMP.text = subtitleText[lineNumBeingRead].line;
                    timeTillNextLine = subtitleText[lineNumBeingRead].timeToPlay;
                }
            }
            if (DialogueController.SubtitlesEnabled)DialogueController.subtitleTMP.text = "";

            Destroy(source);
            Destroy(this);
        }

    }
}

