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
        private ParticleSystem particles;

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

        private void UpdateRun()
        {
            latestRun = UpgradeStats.runs;
        }
        private void PlayParticles()
        {
            particles.Play();
        }

        private void InteractDirt()
        {
            UpdateRun();
            PlayParticles();
            dirt.DoSuccesAction();
        }
        private void InteractSprout()
        {
            UpdateRun();
            PlayParticles();
            sprout.DoSuccesAction();
        }
        private void InteractSapling()
        {
            UpdateRun();
            PlayParticles();
            sapling.DoSuccesAction();
        }
        private void InteractTree()
        {
            UpdateRun();
            PlayParticles();
            tree.DoSuccesAction();
        }
    }
}
