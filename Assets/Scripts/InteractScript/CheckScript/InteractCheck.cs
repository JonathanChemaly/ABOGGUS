using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Checks {

    public interface InteractCheck
    {
        /**
         * does the check to see if action can be taken. 
         * 
         * returns true if action should be taken and false otherwise
         */
        public bool DoCheck();
        
        /**
         * Returns the text to output on failure
         */
        public string GetFailureText();
    }
}

