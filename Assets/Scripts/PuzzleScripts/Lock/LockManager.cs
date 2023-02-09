using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ABOGGUS.Interact.Statics;

namespace ABOGGUS.Interact.Puzzles
{
    public class LockManager : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("List of dials we ar managing in order")]
        private LockRotator[] allLockDials;

        [SerializeField]
        [Tooltip("Password this lock has")]
        private int[] password = { 0, 0, 0, 0 };

        [SerializeField]
        [Tooltip("Event for what happens when we get a correct password")]
        public event Action DoOnCorrectPassword;

        // Start is called before the first frame update
        void Start()
        {
            //sets all dials to intial rotation of 0.
            //And beings listening for click events on all dials so we can check if our password is correct
            foreach (LockRotator dial in allLockDials)
            {
                dial.lockDial.ClickEvent += CheckPassword;
                dial.gameObject.transform.Rotate(-144, 0, 0, Space.Self);
            }
        }

        private bool passwordHasBeenCorrect = false;
        /**
         * Checks for whether the password is correct
         */
        private void CheckPassword()
        {
            //if password hasn't been correct before... do work
            if (!passwordHasBeenCorrect)
            {
                StartCoroutine(WaitForRotate());
            }
            
        }

        /**
         * Wait for rotators to catch up
         */
        IEnumerator WaitForRotate()
        {
            yield return null;

            bool passwordIsCorrect = true;

            //checks if all dials have same value as our password and create a boolean for it
            for (int i = 0; i < allLockDials.Length; i++)
            {
                passwordIsCorrect &= allLockDials[i].CurrentValueShown == password[i];
            }

            //if password is correct do this.
            if (passwordIsCorrect)
            {
                Debug.Log("Password Correct!");
                /*
                * When we get the correct password stop registering further clicks
                */
                foreach (LockRotator dial in allLockDials)
                {
                    dial.lockDial.enabled = false;
                }
                DoOnCorrectPassword?.Invoke();

                InteractStatics.interactActionSuccess = true;
                passwordHasBeenCorrect = true; //tell our manager that we already got the password back as true once
    }
        }
    }

}

