using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.Interact;
using ABOGGUS.Gameplay;

namespace ABOGGUS.Interact.Puzzles
{
    public class TreePuzzle : MonoBehaviour
    {
        [SerializeField] private Interactable dirt;
        [SerializeField] private Interactable sprout;
        [SerializeField] private Interactable sapling;
        [SerializeField] private Interactable tree;
        private ParticleSystem particles;

        public enum Status {DIRT, SPROUT, SAPLING, TREE, FINAL};
        public static Status status = Status.DIRT;
        private Status oldStatus = Status.DIRT;

        public static int latestRun = -1;

        // Start is called before the first frame update
        void Awake()
        {
            dirt.InteractAction += InteractDirt;
            sprout.InteractAction += InteractSprout;
            sapling.InteractAction += InteractSapling;
            tree.InteractAction += InteractTree;
            particles = this.transform.Find("Particles").GetComponent<ParticleSystem>();
        }

        public void LoadPuzzle(Status newStatus, int newRuns)
        {
            int temp = (int)newStatus;
            if (temp >= 1) InteractDirt();
            if (temp >= 2) InteractSprout();
            if (temp >= 3) InteractSapling();
            if (temp >= 4) InteractTree();

            status = newStatus;

            latestRun = newRuns;
        }

        private void UpdateRun()
        {
            latestRun = UpgradeStats.runs;
        }
        private void PlayParticles()
        {
            particles?.Play();
        }

        private void InteractDirt()
        {
            UpdateRun();
            PlayParticles();
            dirt.DoSuccesAction();
            status = Status.SPROUT;
        }
        private void InteractSprout()
        {
            UpdateRun();
            PlayParticles();
            sprout.DoSuccesAction();
            status = Status.SAPLING;
        }
        private void InteractSapling()
        {
            UpdateRun();
            PlayParticles();
            sapling.DoSuccesAction();
            status = Status.TREE;
        }
        private void InteractTree()
        {
            UpdateRun();
            PlayParticles();
            tree.DoSuccesAction();
            status = Status.FINAL;

            GameConstants.puzzleStatus["TreeGrowPuzzle"] = true;    // puzzle is complete
        }
    }
}
