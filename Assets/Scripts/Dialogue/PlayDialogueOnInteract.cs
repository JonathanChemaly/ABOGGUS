using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ABOGGUS.Input;
using ABOGGUS.Sound.Dialogue;
using ABOGGUS.Gameplay;
using System;

namespace ABOGGUS.Interact
{
    public class PlayDialogueOnInteract : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("interact to watch")]
        private Interactable interact;

        [Tooltip("Dialogue File with text and clip to play")]
        public Dialogue dialogue;

        [SerializeField]
        [Tooltip("Dialogue File with text and clip to play")]
        private DialoguePath nextDialoguePath = null;

        [SerializeField]
        [Tooltip("Special")]
        private List<Dialogue> SpecialDialogues;

        [SerializeField]
        [Tooltip("Spell to play when you get a spell")]
        private AudioSource GetSpellSound;

        private DialoguePlayer dialPlayer;

        private bool gotSpell, gotAoe;

        private void Start()
        {
            interact.InteractAction += PlayAudio;
            gotSpell = 
                (GameController.scene == GameConstants.SCENE_AUTUMNROOM && GameConstants.windUnlocked) ||
                (GameController.scene == GameConstants.SCENE_SUMMERROOM && GameConstants.fireUnlocked) ||
                (GameController.scene == GameConstants.SCENE_WINTERROOM && GameConstants.waterUnlocked) ||
                (GameController.scene == GameConstants.SCENE_SPRINGROOM && GameConstants.natureUnlocked);
            gotAoe = 
                (GameController.scene == GameConstants.SCENE_AUTUMNROOM && GameConstants.waterAOEUnlocked) ||
                (GameController.scene == GameConstants.SCENE_SUMMERROOM && GameConstants.fireAOEUnlocked) ||
                (GameController.scene == GameConstants.SCENE_WINTERROOM && GameConstants.waterAOEUnlocked) ||
                (GameController.scene == GameConstants.SCENE_SPRINGROOM && GameConstants.natureAOEUnlocked);
        }

        private void CreateDialoguePlayer(Dialogue d)
        {
            //To play the audio we create a dialogue player object and give it the dialogue to play which plays on start
            dialPlayer = gameObject.AddComponent<DialoguePlayer>();
            dialPlayer.dialogue = d; //giving the player what to play
        }

        //Plays audio on interact
        private void PlayAudio()
        {
            if (!gotSpell)
            {
                // logic for obtaining the basic versions of the spells
                if (GameController.scene == GameConstants.SCENE_AUTUMNROOM && !GameConstants.windUnlocked)
                {
                    Debug.Log("Wind Spell Unlocked!");
                    GameConstants.windUnlocked = true;
                    gotSpell = true;
                    //Play audio
                    CreateDialoguePlayer(SpecialDialogues[0]);
                    StartCoroutine(DisableWhileAudioPlayingNoDialoguePathing());
                }
                if (GameController.scene == GameConstants.SCENE_SUMMERROOM && !GameConstants.fireUnlocked)
                {
                    Debug.Log("Fire Spell Unlocked!");
                    GameConstants.fireUnlocked = true;
                    gotSpell = true;
                    //Play audio
                    CreateDialoguePlayer(SpecialDialogues[1]);
                    StartCoroutine(DisableWhileAudioPlayingNoDialoguePathing());
                }
                if (GameController.scene == GameConstants.SCENE_WINTERROOM && !GameConstants.waterUnlocked)
                {
                    Debug.Log("Water Spell Unlocked!");
                    GameConstants.waterUnlocked = true;
                    gotSpell = true;
                    //Play audio
                    CreateDialoguePlayer(SpecialDialogues[2]);
                    StartCoroutine(DisableWhileAudioPlayingNoDialoguePathing());
                }
                if (GameController.scene == GameConstants.SCENE_SPRINGROOM && !GameConstants.natureUnlocked)
                {
                    Debug.Log("Nature Spell Unlocked!");
                    GameConstants.natureUnlocked = true;
                    gotSpell = true;
                    //Play audio
                    CreateDialoguePlayer(SpecialDialogues[3]);
                    StartCoroutine(DisableWhileAudioPlayingNoDialoguePathing());
                }
                if (!gotSpell) //some how we still don't have the spell play regular comment
                {
                    Debug.Log("Playing Default!");
                    CreateDialoguePlayer(dialogue);
                    //we then in this case what to disable the interactable while this is playing
                    StartCoroutine(DisableWhileAudioPlaying());
                }
            } 
            else if (!gotAoe) 
            {
                // logic for obtaining the aoe spells
                if (GameController.scene == GameConstants.SCENE_AUTUMNROOM && GameConstants.puzzleStatus["TractorPuzzle"]
                    && GameConstants.puzzleStatus["MazePuzzle"] && !GameConstants.windAOEUnlocked)
                {
                    Debug.Log("Wind AOE Spell Unlocked!");
                    GameConstants.windAOEUnlocked = true;
                    //Play audio
                    CreateDialoguePlayer(SpecialDialogues[4]);
                    StartCoroutine(DisableWhileAudioPlayingNoDialoguePathing());
                    gotAoe = true;
                }
                if (GameController.scene == GameConstants.SCENE_SUMMERROOM && GameConstants.puzzleStatus["TileSlidePuzzle"]
                    && GameConstants.puzzleStatus["WindPushPuzzle"] && GameConstants.puzzleStatus["FirePuzzle"] && !GameConstants.fireAOEUnlocked)
                {
                    Debug.Log("Fire AOE Spell Unlocked!");
                    GameConstants.fireAOEUnlocked = true;
                    //Play audio
                    CreateDialoguePlayer(SpecialDialogues[4]);
                    StartCoroutine(DisableWhileAudioPlayingNoDialoguePathing());
                    gotAoe = true;
                }
                if (GameController.scene == GameConstants.SCENE_WINTERROOM && GameConstants.puzzleStatus["MeltIcePuzzle"]
                    && GameConstants.puzzleStatus["FallingIcePuzzle"] && !GameConstants.waterAOEUnlocked)
                {
                    Debug.Log("Water AOE Spell Unlocked!");
                    GameConstants.waterAOEUnlocked = true;
                    //Play audio
                    CreateDialoguePlayer(SpecialDialogues[4]);
                    StartCoroutine(DisableWhileAudioPlayingNoDialoguePathing());
                    gotAoe = true;
                }
                if (GameController.scene == GameConstants.SCENE_SPRINGROOM && GameConstants.puzzleStatus["RunePuzzle"]
                    && GameConstants.puzzleStatus["TreeGrowPuzzle"] && !GameConstants.natureAOEUnlocked)
                {
                    Debug.Log("Nature AOE Spell Unlocked!");
                    GameConstants.natureAOEUnlocked = true;
                    //Play audio
                    CreateDialoguePlayer(SpecialDialogues[4]);
                    StartCoroutine(DisableWhileAudioPlayingNoDialoguePathing());
                    gotAoe = true;
                }
                if (!gotAoe) //some how we still don't have the spell play regular comment
                {
                    Debug.Log("Playing Default!");
                    CreateDialoguePlayer(dialogue);
                    //we then in this case what to disable the interactable while this is playing
                    StartCoroutine(DisableWhileAudioPlaying());
                }
            } 
            else
            {
                Debug.Log("Playing Default!");
                CreateDialoguePlayer(dialogue);
                //we then in this case what to disable the interactable while this is playing
                StartCoroutine(DisableWhileAudioPlaying());
            }
            

            
        }

        IEnumerator DisableWhileAudioPlaying()
        {
            interact.enabled = false;
            //Dialogue player destorys itself after it is finished playing so we can reenabel the interact after
            //the object is null
            while (dialPlayer != null)
            {
                yield return null;
            }

            //Check if we have another dialogue to play in our sequence
            if (nextDialoguePath != null) 
            {
                interact.enabled = true;
                dialogue = nextDialoguePath.curDialogue; //sets dialogue on the path as we should
                nextDialoguePath = nextDialoguePath.nextPath; //recursively gets next path (could be null or another path)
            } //do not renable if we only want to play once
        }

        IEnumerator DisableWhileAudioPlayingNoDialoguePathing()
        {
            interact.enabled = false;
            //Dialogue player destorys itself after it is finished playing so we can reenabel the interact after
            //the object is null
            while (dialPlayer != null)
            {
                yield return null;
            }

            //Check if we have another dialogue to play in our sequence
            if (nextDialoguePath != null)
            {
                interact.enabled = true;
            } //do not renable if we only want to play once

            GetSpellSound.Play();
        }

    }
}

