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
        [Tooltip("Interactables to enable when all other interacts are sucessful")]
        private Interactable toEnable;

        private List<bool> successList; 

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

        public void SetRuneActive(int runeNum)
        {
            successList[runeNum] = true;
            CheckSucess();
        }

        public void CheckSucess()
        {
            if (AreAllRunesActive())
            {
                toEnable.enabled = true;
            }
        }

        public bool AreAllRunesActive()
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

