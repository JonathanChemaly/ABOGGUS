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
        [SerializeField] private LargeEEDrop manaDrop;
        private ParticleSystem particles;

        public enum Status {DIRT, SPROUT, SAPLING, TREE, FINAL};
        public static Status status = Status.DIRT;

        public static int latestRun = -1;

        // Start is called before the first frame update
        void Awake()
        {
            dirt.InteractAction += InteractDirt;
            sprout.InteractAction += InteractSprout;
            sapling.InteractAction += InteractSapling;
            tree.InteractAction += InteractTree;
            particles = this.transform.Find("Particles").GetComponent<ParticleSystem>();
            manaDrop.eventOnPickup += PuzzleComplete;
        }

        public void LoadPuzzle(Status newStatus, int newRuns)
        {
            status = Status.DIRT;
            if (GameConstants.puzzleStatus["TreeGrowPuzzle"])
            {
                Destroy(manaDrop.transform.parent.gameObject);
            }
            StartCoroutine(WaitToLoadTree(newStatus, newRuns));
        }

        IEnumerator WaitToLoadTree(Status newStatus, int newRuns)
        {
            Debug.Log("test1");
            yield return new WaitForSeconds(1.0f);
            Debug.Log("test2");
            if (newStatus > Status.DIRT && status == Status.DIRT) InteractDirt();
            yield return new WaitForSeconds(0.5f);
            if (newStatus > Status.SPROUT && status == Status.SPROUT) InteractSprout();
            yield return new WaitForSeconds(0.5f);
            if (newStatus > Status.SAPLING && status == Status.SAPLING) InteractSapling();
            yield return new WaitForSeconds(0.5f);
            if (newStatus > Status.TREE && status == Status.TREE) InteractTree();
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
            //Debug.Log("dirt interaction, updated status: " + status);
        }
        private void InteractSprout()
        {
            UpdateRun();
            PlayParticles();
            sprout.DoSuccesAction();
            status = Status.SAPLING;
            //Debug.Log("sprout interaction, updated status: " + status);
        }
        private void InteractSapling()
        {
            UpdateRun();
            PlayParticles();
            sapling.DoSuccesAction();
            status = Status.TREE;
            //Debug.Log("sapling interaction, updated status: " + status);
        }
        private void InteractTree()
        {
            UpdateRun();
            PlayParticles();
            tree.DoSuccesAction();
            status = Status.FINAL;
            //Debug.Log("tree interaction, updated status: " + status);

            if (GameConstants.puzzleStatus["TreeGrowPuzzle"])
            {
                Destroy(manaDrop.transform.parent.gameObject);
            }
        }

        public void PuzzleComplete()
        {
            GameConstants.puzzleStatus["TreeGrowPuzzle"] = true;    // puzzle is complete
        }
    }
}
