using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact
{
    public class PlayAnimationOnSuccess : MonoBehaviour
    {

        [Tooltip("interact to watch")]
        public Interactable interact;

        [SerializeField]
        [Tooltip("Animation to play when interacted with")]
        private Animation aniToPlay;


        // Start is called before the first frame update
        private void Start()
        {
            interact.SuccessAction += PlayAni;
        }

        //Plays animation on 
        private void PlayAni()
        {
            aniToPlay.Play();
            interact.enabled = false;

        }
        
    }
}

