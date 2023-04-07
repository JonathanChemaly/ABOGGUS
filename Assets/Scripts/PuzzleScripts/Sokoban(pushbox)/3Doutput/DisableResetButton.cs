using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class DisableResetButton : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Time To disable Button")]
        private float secondsToDisable = 1f;

        [SerializeField]
        [Tooltip("button To disable")]
        private Button button;

        public void DisableButton()
        {
            button.enabled = false;
            StartCoroutine(EnableButtonAfterDelay());
        }

        IEnumerator EnableButtonAfterDelay()
        {
            yield return new WaitForSeconds(secondsToDisable);
            button.enabled = true;
        }
    }
}

