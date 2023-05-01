using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.Interact;

namespace ABOGGUS.Interact.Puzzles
{
    public class TractorLoader : MonoBehaviour
    {
        [SerializeField] private Interactable tractor0;
        [SerializeField] private Interactable tractor1;
        [SerializeField] private Interactable tractor2;
        [SerializeField] private Interactable tractor3;
        [SerializeField] private GameObject mana;

        // Start is called before the first frame update
        public void LoadPuzzle(int newState)
        {
            if (newState > 0)
            StartCoroutine(WaitToLoadTractor(newState));
        }

        IEnumerator WaitToLoadTractor(int newState)
        {
            yield return new WaitForSeconds(1.0f);
            if (newState > 0) tractor0.DoSuccesAction();
            yield return new WaitForSeconds(0.5f);
            if (newState > 1) tractor1.DoSuccesAction();
            yield return new WaitForSeconds(0.5f);
            if (newState > 2) tractor2.DoSuccesAction();
            yield return new WaitForSeconds(0.5f);
            if (newState > 3)
            {
                tractor3.DoSuccesAction();
                Destroy(mana);
            }

        }

        public int SavePuzzle()
        {
            int stateCnt = 0;
            if (!CheckExists(tractor0)) stateCnt++;
            if (!CheckExists(tractor1)) stateCnt++;
            if (!CheckExists(tractor2)) stateCnt++;
            if (!CheckExists(tractor3)) stateCnt++;
            return stateCnt;
        }

        private bool CheckExists(Interactable tractor)
        {
            bool result = true;
            try
            {
                GameObject test = tractor.gameObject;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
