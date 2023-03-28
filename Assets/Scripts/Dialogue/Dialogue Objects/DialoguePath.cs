using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Sound.Dialogue
{
    /**
     * Object that contains the next dialogue to play
     */
    [CreateAssetMenu(menuName = "Dialogue/Dialogue Path", fileName = "Dialogue Path", order = 0)]
    public class DialoguePath : ScriptableObject
    {
        public DialoguePath nextPath = null;

        public Dialogue curDialogue;
    }
}