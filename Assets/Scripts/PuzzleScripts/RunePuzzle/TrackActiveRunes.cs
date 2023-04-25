using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.RunePuzzle
{
    public class TrackActiveRunes : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Interactables Associated with this class that need to watched for interaction")]
        private List<Interactable> runeInteractables;

        [SerializeField]
        [Tooltip("Rays associated with each rune")]
        private List<PlayAudioOnInteract> audioToDisableIfLoad;

        [SerializeField]
        [Tooltip("Interactables to enable when all other interacts are sucessful")]
        private Interactable toEnable;

        [SerializeField]
        [Tooltip("")]
        private EnableGameObject dragonRayEnable;

        private List<bool> successList;

        public void LoadFromList(List<bool> activeRunesList, bool complete)
        {
            string debugString = "";
            for (int i = 0; i < activeRunesList.Count; i++)
            {
                bool currentStatus = activeRunesList[i];
                audioToDisableIfLoad[i].enabled = currentStatus;
                Interactable curInteractable = runeInteractables[i];
                if (currentStatus) curInteractable.DoAction();
                curInteractable.enabled = currentStatus;
                debugString += currentStatus.ToString();
            }
            successList = activeRunesList;
            Debug.Log(debugString);
            if(complete)
            {
                toEnable.enabled = false;
                dragonRayEnable.enabled = false;
            }
        }

        /**
         * replaces list with our current rune status on which our active
         */
        public void SaveFromList(List<bool> activeRunesList)
        {
            activeRunesList.Clear();
            activeRunesList.AddRange(successList);
        }

        private void Start()
        {
            successList = new List<bool>();

            for (int i = 0; i < runeInteractables.Count; i++)
            {
                int param = i;
                runeInteractables[i].InteractAction += delegate() { SetRuneActive(param); };
                successList.Add(false);
            }

            

            toEnable.enabled = false;
        }

        private void SetRuneActive(int runeNum)
        {
            successList[runeNum] = true;
            CheckSucess();
        }

        private void CheckSucess()
        {
            if (AreAllRunesActive())
            {
                toEnable.enabled = true;
            }
        }

        private bool AreAllRunesActive()
        {
            foreach (bool b in successList)
            {
                if (!b)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

