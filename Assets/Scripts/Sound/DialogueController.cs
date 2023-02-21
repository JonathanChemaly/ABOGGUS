using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ABOGGUS.Sound.Dialogue
{
    /**
     * Controller for dialogue so subtitles can work
     */
    public class DialogueController : MonoBehaviour
    {
        //Global available reference to subtitle 
        public static TextMeshProUGUI subtitleTMP;

        //Global available reference to whether subtitles are enbabled
        public static bool SubtitlesEnabled = true;

        [SerializeField]
        [Tooltip("Canvas Display location for the subtitle text")]
        private TextMeshProUGUI subtitleTextMeshPro;

        private void Start()
        {
            subtitleTMP = subtitleTextMeshPro;
        }
    }
}
