using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Sound.Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue/Dialogue Container", fileName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject
    {
        [Tooltip("Text of dialogue to ouput as subtitles")]
        public List<DialogueLine> subtitleText;

        [Tooltip("Text of dialogue to ouput as subtitles")]
        public AudioClip audioClip;
    }
}