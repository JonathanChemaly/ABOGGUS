using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ABOGGUS.Interact
{
    public class SetPuzzleStatusToTrue : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("interact to watch")]
        private Interactable interact;

        [SerializeField]
        [Tooltip("name of puzzle status to set to true")]
        private string puzzleName;


        // Start is called before the first frame update
        void Start()
        {
            interact.InteractAction += SetPuzzleStatus;
        }

        private void SetPuzzleStatus()
        {
            ABOGGUS.Gameplay.GameConstants.puzzleStatus[puzzleName] = true;
        }
    }
}

