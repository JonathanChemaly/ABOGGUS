using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.Interact;

namespace ABOGGUS.Interact.Puzzles
{
    public class TreePuzzle : MonoBehaviour
    {
        [SerializeField] private Interactable dirt;
        [SerializeField] private Interactable sprout;
        [SerializeField] private Interactable sapling;
        [SerializeField] private Interactable tree;

        public static int latestRun = -1;

        // Start is called before the first frame update
        void Awake()
        {
            dirt.InteractAction += InteractDirt;
            sprout.InteractAction += InteractSprout;
            sapling.InteractAction += InteractSapling;
            tree.InteractAction += InteractTree;
        }

        private void UpdateRun()
        {
            //latestRun = UpgradeStats.runs;
        }

        private void InteractDirt()
        {
            UpdateRun();
            dirt.DoSuccesAction();
        }
        private void InteractSprout()
        {
            UpdateRun();
            sprout.DoSuccesAction();
        }
        private void InteractSapling()
        {
            UpdateRun();
            sapling.DoSuccesAction();
        }
        private void InteractTree()
        {
            UpdateRun();
            tree.DoSuccesAction();
        }
    }
}
