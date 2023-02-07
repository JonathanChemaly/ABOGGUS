using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles
{
    public class LockRotator : MonoBehaviour
    {
        [Tooltip("Clickable roller that we spin on click")]
        public Clickable lockDial;

        [HideInInspector]
        [Tooltip("Value Currently shown on dial")]
        public int CurrentValueShown { get => curValOnDial; }

        private int curValOnDial = 0;

        private const int amountToRotateDial = -36;

        // Start is called before the first frame update
        void Start()
        {
            lockDial.ClickEvent += RotateOnClick; //when clicked rotates
        }

        /**
         * Rotates the dial one tick to the next number then updates the currentValue shown to the new value
         */
        private void RotateOnClick()
        {
            gameObject.transform.Rotate(amountToRotateDial, 0, 0, Space.Self);

            curValOnDial += 1;

            if (curValOnDial > 9)
            {
                curValOnDial = 0;
            }
        }
    }
}

