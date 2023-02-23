using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ABOGGUS.Sound.Dialogue
{
    public class Dialogue: MonoBehaviour
    {
        [Tooltip("Holder of the text data for this dialogue")]
        public List<DialogueLine> subtitleText;

        [Tooltip("")]
        public string locationOfSoundFile;

        /*
        [SerializeField]
        [Tooltip("What action will be taken when item is interacted with")]
        public UnityAction playAudioAndDisplaySubtitles;
        */

        /**
         * Creates new dialogue object and plays audio and 
         */
        private void Start()
        {
            StartCoroutine(playAudioAndDisplaySubtitles());
        }

        IEnumerator playAudioAndDisplaySubtitles()
        {
            int lineNumBeingRead = 0;


            AudioClip clip = Resources.Load<AudioClip>(locationOfSoundFile);
            Debug.Log(clip);

            AudioSource dialogue = gameObject.AddComponent<AudioSource>();

            
            dialogue.clip = clip;

            Debug.Log("Play Audio at: " + locationOfSoundFile);
            dialogue.Play();

            float startTime = Time.time;
            float timeTillNextLine = subtitleText[lineNumBeingRead].timeToPlay;

            if (DialogueController.SubtitlesEnabled)
            {
                DialogueController.subtitleTMP.text = subtitleText[lineNumBeingRead].line;
            }
            while (dialogue.isPlaying)
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

            Destroy(dialogue);
            Destroy(this);
        }

    }
}

