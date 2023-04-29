using ABOGGUS.Interact;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Sound.Dialogue
{
    public class OnlyPlayOnce : MonoBehaviour
    {
        private static bool playedOnce = false;

        [SerializeField]
        [Tooltip("Dialogue to only play once during game")]
        private PlayDialogueAfterTime thingToPlayOnce;

        private void Awake()
        {
            if (playedOnce)
            {
                thingToPlayOnce.enabled = false;
            }
            else
            {
                playedOnce = true;
            }
        }


    }
}

