using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ABOGGUS.Interact.Puzzles
{
    public class Clickable : MonoBehaviour
    {
        public event Action ClickEvent;
        
        private void OnMouseDown()
        {
            //Debug.Log("click detected on " + gameObject.name);
            ClickEvent?.Invoke();
        }
    }
}

